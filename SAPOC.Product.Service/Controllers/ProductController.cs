using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
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


        #endregion

        #region Private methods
        private void Initialize()
        {
            var unityContainer = new UnityContainer();

            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");

            section.Configure(unityContainer);

            productService = unityContainer.Resolve<IProductService>();
        }


        #endregion
    }
}
