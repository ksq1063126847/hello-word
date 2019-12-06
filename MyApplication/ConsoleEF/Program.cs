using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleEF.Context;
using ConsoleEF.Entity;

namespace ConsoleEF
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer( new InitiStrategy());
            CreatData();            
            ReadData();
            Console.Read();
        }

        static void CreatData()
        {
            using (var context = new TestContext())
            {
                context.Database.CreateIfNotExists();
                var list = new List<Donator>()
                {
                    new Donator{ Name="third", Amount =100, DonateDate = DateTime.Now},
                    new Donator{ Name ="fourth", Amount = 100,DonateDate = DateTime.Now}
                };
                context.Donator.AddRange(list);
                context.SaveChanges();
            }
            //Console.Write("DB has Created!");//提示DB创建成功
            Console.WriteLine("Creation Finished!");//提示创建完成           
        }
        static void ReadData()
        {
            using (var context = new TestContext())
            {
                foreach (var item in context.Donator)
                {
                    Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", item.DonatorId, item.Name, item.Amount, item.DonateDate.GetValueOrDefault().ToShortDateString());
                }
            }           
        }
        static void UpdateData()
        {
            using (var context = new TestContext())
            {
                if (context.Donator.Any())
                {
                    var item = context.Donator.First(p => p.Name == "one");
                    if (item != null)
                    {
                        item.Amount = 200;
                        context.SaveChanges();
                    }                   
                }
            }           
        }
        static void DeleteData()
        {
            using (var context = new TestContext())
            {
                if (context.Donator.Any())
                {
                    var item = context.Donator.Single(p => p.Name == "1");
                    if (item != null)
                    {
                        context.Donator.Remove(item);
                        context.SaveChanges();
                    }
                }
            }
            Console.Read();
        }
  