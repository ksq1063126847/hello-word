using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using WebApplication2.ServicesClass;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using Domain.Concrete;

namespace WebApplication2.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBinding();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBinding()
        {
            kernel.Bind<IHelper>().To<Helper>();
            kernel.Bind<ICommon>().To<Common>().WithConstructorArgument("param",true);

            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(p => p.Products).Returns(new List<Product>() {
            //    new Product{ Name ="FootBall",Price=25},
            //    new Product{ Name ="Surf board",Price=179},
            //    new Product{ Name ="Running shoes",Price=95},
            //});
            //kernel.Bind<IProductRepository>().ToConstant(mock.Object); //ToConstant限定作用域
            kernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
    }
}