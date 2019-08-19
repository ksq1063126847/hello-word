using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.ServicesClass;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using WebApplication2.Controllers;
using System.Text.RegularExpressions;
using WebApplication2.Models;
using System.Web.Mvc;

namespace WebApplication2.Repository.Tests
{
    [TestClass()]
    public class TestRepositoryTests
    {
        [TestMethod]
        public void HelperTest()
        {
            Mock<ICommon> mock = new Mock<ICommon>();
            mock.Setup(p => p.count(It.IsAny<int>())).Returns<int>(p => p);
            IHelper result = new Helper(mock.Object);
            Assert.AreEqual("2019", result.GetYear());
        }
        [TestMethod]
        public void Can_Paginate()
        {
            //1.模仿 IProductRepository 的实例对象, 排除干扰（专注于当前的测试对象，排除其他对象的干扰）
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new List<Product>()
            {
                new Product{ ProductID=1,Name ="P1"},
                new Product{ ProductID=2,Name ="P2"},
                new Product{ ProductID=3,Name ="P3"},
            });
            //2.实例化 此次测试的对象
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 2;
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;
            //3,开始测试
            Assert.IsTrue(result.PagingInfo.CurrentPage == 2);
            Assert.IsTrue(result.PagingInfo.TotalPages == 2);
            Assert.IsTrue(result.PagingInfo.TotalItems == 3);
            Assert.IsTrue(result.Products.Count() == 1);
            Assert.AreEqual("P3", result.Products.ToList()[0].Name);
        }
        [TestMethod]
        public void Can_Create_Categories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new List<Product>()
            {
                new Product{ ProductID=1,Name ="P1",Category="Apples"},
                new Product{ ProductID=2,Name ="P2",Category="Apples"},
                new Product{ ProductID=3,Name ="P3",Category="Oranges"},
            });
            NavController target = new NavController(mock.Object);
            var result = (IEnumerable<string>)target.Menu().Model;
            Assert.IsTrue(result.ToArray().Length == 2);
            Assert.AreEqual("Apples", result.ToArray()[0]);
            Assert.AreEqual("Oranges", result.ToArray()[1]);
        }
        [TestMethod]
        public void TestStr()
        {
            Assert.IsTrue("10年1个月" == ChangeYearStr("10年1个月"));
            Assert.IsTrue("1个月" == ChangeYearStr("0年1个月"));
            Assert.IsTrue("1个月" == ChangeYearStr("00年1个月"));
            Assert.IsTrue("12个月" == ChangeYearStr("零年12个月"));
        }
        private string ChangeYearStr(string yearStr)
        {
            string result = yearStr.ToString();
            string str0 = @"(\d{1,2})年(\d{1,2})";
            string str1 = @"零年(\d{1,2})";
            List<string> list = new List<string>() { str0, str1 };
            foreach (string pattern in list)
            {
                foreach (System.Text.RegularExpressions.Match match in Regex.Matches(yearStr, pattern))
                {
                    if (pattern.Equals(str0))
                    {
                        string d0 = match.Groups[0].Value;
                        string d1 = match.Groups[1].Value;
                        string d2 = match.Groups[2].Value;
                        if (d1.Equals("0") || d1.Equals("00"))
                        {
                            result = result.Replace(d0, d2);
                        }
                    }
                    else if (pattern.Equals(str1))
                    {
                        string d0 = match.Groups[0].Value;
                        result = result.Replace(d0, d0.Substring(2));
                    }
                }
            }
            return result;
        }
        [TestMethod]
        public void Can_Add_NewCartLine() //测试购物车功能
        {
            var list = new List<Product>() {
                new Product{ ProductID=1,Name ="P1",Price =10},
                new Product{ ProductID=2,Name ="P2",Price =20},
                new Product{ ProductID=3,Name ="P3",Price =30} };
            var target = new Cart();
            foreach (var item in list)
            {
                target.AddItem(item, 1);
            }
            Assert.IsTrue(target.Lines().ToList().Count() == 3);
            Assert.IsTrue(target.ComputeTotalValue() == 60m);           
        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new List<Product>()
            {
                new Product{ ProductID=1,Name ="P1",Category="Apples"},              
            });
            CartController target = new CartController(mock.Object,null);
            Cart cart = new Cart();
            RedirectToRouteResult result = target.AddToCard(cart, 1, "urlTest");
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("urlTest", result.RouteValues["returnUrl"]);
        }
        [TestMethod]
        public void Can_View_Cart_Context()
        {
            CartController target = new CartController(null,null);
            Cart cart = new Cart();
            CartIndexViewModel result =(CartIndexViewModel) target.Index(cart, "urlTest").ViewData.Model;
            Assert.AreSame(cart, result.Cart);
            Assert.AreEqual("urlTest", result.ReturnUrl);
        }

        [TestMethod]
        public void Can_Not_Add_Empty_Cart()
        {
            //1.模仿订单处理器
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            //2.准备空的购物车 和 订单详情
            Cart cart = new Cart();            
            ShippingDetails shipping = new ShippingDetails();
            //3.控制器实例
            CartController target = new CartController(null, mock.Object);
            //4.动作
            var result = target.Checkout(cart, shipping);
            //断言-检查，订单尚未传给处理器
            mock.Verify(p => p.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never);

            Assert.AreEqual("", result.ViewName);//返回的是默认视图
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);//传递的是非法模型
        }
    }
}