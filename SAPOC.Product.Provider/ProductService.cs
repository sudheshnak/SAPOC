using SAPOC.Contract;
using SAPOC.Repository.Common;
using System;
using System.Collections.Generic;

namespace SAPOC.Product.Provider
{
    public class ProductService : IProductService
    {
        public IRepository repository
        {
            get;
            set;
        }

        public List<Contract.Entity.Product> GetAllProduct()
        {
            List<Contract.Entity.Product> products = new List<Contract.Entity.Product>();
            List<Repository.Common.Entity.Product> productsDto = repository.GetAllProduct();

            //Do proper mapping over here
            foreach(Repository.Common.Entity.Product p in productsDto)
            {
                Contract.Entity.Product p1 = new Contract.Entity.Product();
                p1.Name = p.Name;
                p1.Description = p.Description;
                p1.ProductId = p.ProductId;

                products.Add(p1);
            }
            return products;
        }

        public Contract.Entity.Product GetProductById(int id)
        {
            Repository.Common.Entity.Product productsDto = repository.GetProductById(id);

            Contract.Entity.Product product = new Contract.Entity.Product();
            product.Name = productsDto.Name;
            product.Description = productsDto.Description;
            product.ProductId = productsDto.ProductId;

            return product;
        }
        
    }
}
