using SAPOC.Repository.Common;
using SAPOC.Repository.Common.Entity;
using SAPOC.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAPOC.Repository
{
    public class ProductRepository : IRepository
    {
        private readonly ProductDbContext _productDbContext;


        public ProductRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public List<Product> GetAllProduct()
        {
           
            List<Product> products = new List<Product>();
            products = _productDbContext.Products.ToList();
            //Product p1 = new Product();
            //p1.ProductId = 1;
            //p1.Name = "Apple";
            //p1.Description = "Fruit";

            //Product p2 = new Product();
            //p2.ProductId = 2;
            //p2.Name = "Orange";
            //p2.Description = "Fruit";

            //Product p3 = new Product();
            //p3.ProductId = 3;
            //p3.Name = "Grapes";
            //p3.Description = "Fruit";

            //products.Add(p1);
            //products.Add(p2);
            //products.Add(p3);

            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = _productDbContext.Products.SingleOrDefault(x => x.ProductId == id);
            return product;
        }
    }
}
