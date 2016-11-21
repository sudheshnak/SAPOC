using Microsoft.Practices.Unity;
using SAPOC.Contract;
using SAPOC.Product.Provider;
using SAPOC.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SAPOC.UI.App_Start
{
    public static class UnityConfig
    {
        public static void ConfigureIocUnityContainer()
        {
            IUnityContainer container = new UnityContainer();
            RegisterServices(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IProductService, ProductService>();
        }
    }
}