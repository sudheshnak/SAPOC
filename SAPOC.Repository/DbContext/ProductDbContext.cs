using SAPOC.Repository.Common.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPOC.Repository.Context
{
    public class ProductDbContext : DbContext
    {
//jj

        public ProductDbContext(string connectionStringOrName)
            : base(connectionStringOrName)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ProductDbContext>());
        }


        public DbSet<Product> Products { get; set; }

    }
}
