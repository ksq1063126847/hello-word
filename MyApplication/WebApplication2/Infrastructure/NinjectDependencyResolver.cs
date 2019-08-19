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
using System.Configuration;
using WebApplication2.Infrastructure.Abstract;
using WebApplication2.Infrastructure.Concrete;

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
            kernel.Bind<IProductRepository>().To<EFProductRepository>();

            EmailSetting emailSetting = new EmailSetting()
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            kernel.Bind<IOrderProcessor>().To<EmailOderProcessor>().WithConstructorArgument("settings", emailSetting);

            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}