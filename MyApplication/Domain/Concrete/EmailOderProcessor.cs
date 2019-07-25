using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Domain.Concrete
{
    public class EmailSetting
    {
        public string MailToAddress = "order@example.com";
        public string MailFromAddress = "sportsrore@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "stmp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\sports_store_emails";
    }
    public class EmailOderProcessor : IOrderProcessor
    {
        private EmailSetting emailSetting;
        public EmailOderProcessor(EmailSetting emailSetting)
        {
            this.emailSetting = emailSetting;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSetting.UseSsl;
                smtpClient.Host = emailSetting.ServerName;
                smtpClient.Port = emailSetting.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSetting.Username, emailSetting.Password);
                if (emailSetting.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSetting.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submited")
                    .AppendLine("---")
                    .AppendLine("Items：");
                foreach (var line in cart.Lines())
                {
                    var subTotal = line.Product.Price * line.quantity;
                    body.AppendFormat("{0}x{1}(subtotal:{2:c})", line.quantity, line.Product.Name, subTotal);
                }
                body.AppendFormat("Total order value:{0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingDetails.Line1)
                    .AppendLine(shippingDetails.Line2 ?? "")
                    .AppendLine(shippingDetails.Line3 ?? "")
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.State ?? "")
                    .AppendLine(shippingDetails.Country)
                    .AppendLine(shippingDetails.Zip)
                    .AppendLine("---")
                    .AppendFormat("Gift wrap:{0}", shippingDetails.GiftWrap ? "Yes" : "No");
                MailMessage mailMessage = new MailMessage(
                    emailSetting.MailFromAddress, //From
                    emailSetting.MailToAddress,//To
                    "New order submitted！",//Subject
                    body.ToString()//Body
                    );
                if (emailSetting.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);
            }
        }
    }
}
