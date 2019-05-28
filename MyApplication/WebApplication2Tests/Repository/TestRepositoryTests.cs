using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Repository.Tests
{
    [TestClass()]
    public class TestRepositoryTests
    {
        [TestMethod()]
        public void GetHelloMsgTest()
        {
            TestRepository repository = new TestRepository();
            Assert.AreEqual(repository.GetHelloMsg(), "Hello World!");          
        }
    }
}