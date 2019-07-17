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

namespace WebApplication2.Repository.Tests
{
    [TestClass()]
    public class TestRepositoryTests
    {        [TestMethod()]
        public void HelperTest()
        {
            Mock<ICommon> mock = new Mock<ICommon>();
            mock.Setup(p => p.count(It.IsAny<int>())).Returns<int>(p => p);
            IHelper result = new Helper(mock.Object);
            Assert.AreEqual("2019", result.GetYear());
        }

        [TestMethod()]
        public void CanPage()
        {
            //1.模仿 IProductRepository 的实例对象, 排除干扰（专注于当前的测试对象，排除其他对象的干扰）
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new List<Product>() {
                new Product{ Name ="FootBall",Price=25},
                new Product{ Name ="Surf board",Price=179},
                new Product{ Name ="Running shoes",Price=95},
            });
            //2.实例化 此次测试的对象
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 2;
            var arr = ((IEnumerable<Product>)controller.List(2).Model).ToArray();
            //3,开始测试
            Assert.IsTrue(arr.Length == 1);
            Assert.AreEqual("Running shoes", arr[0].Name);
        }
    }
}