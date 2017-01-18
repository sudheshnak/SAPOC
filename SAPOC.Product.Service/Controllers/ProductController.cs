using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using SAPOC.Cache;
using SAPOC.Contract;
using SAPOC.Contract.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SAPOC.Product.Service.Controllers
{
    public class ProductController : ApiController
    {
        public IProductService productService
        {
            get;
            set;
        }
        public ICache cache
        {
            get;
            set;
        }


        public ProductController()
        {
            this.Initialize();
        }

        #region Interface Methods

        [HttpGet]
        [Route("api/product")]
        public List<SAPOC.Contract.Entity.Product> GetAllProduct()
        {
            return this.productService.GetAllProduct();
        }

        [HttpGet]
        [Route("api/product/{id}")]
        public SAPOC.Contract.Entity.Product GetAllProductById(int id)
        {
            var cacheKey = string.Format("product_{0}", id);
            SAPOC.Contract.Entity.Product product = new Contract.Entity.Product();
            product = this.cache.Get<SAPOC.Contract.Entity.Product>(cacheKey);
            if (product == null)
            {
                product = this.productService.GetProductById(id);
                this.cache.Set(cacheKey, product);
            }
            return product;
        }


        #endregion

        #region Private methods
        private void Initialize()
        {
            var unityContainer = new UnityContainer();

            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");

            section.Configure(unityContainer);

            this.productService = unityContainer.Resolve<IProductService>();

            // Initialize Redis cache
            unityContainer.RegisterType<RedisCache>(new ContainerControlledLifetimeManager()
                                                            , new InjectionConstructor(
                                                                    Convert.ToBoolean(ConfigurationManager.AppSettings["IsCacheEnabled"])
                                                            )); //Singleton ( RedisCache use thread-safe code)
            unityContainer.RegisterType<ICache, RedisCache>(); //Re-use the singleton above
            unityContainer.RegisterType<ICacheStatus, RedisCache>(); //Re-use the singleton above

            this.cache = unityContainer.Resolve<ICache>();
        }


        #endregion
    }
}
