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
        public ProductDbContext(string connectionStringOrName)
            : base(string.Format("name={0}", connectionStringOrName))
        {
            Database.CreateIfNotExists();
        }

        public ProductDbContext()
           : base("name=SAPOCConnection")
        {
            
        }

        public DbSet<Product> Products { get; set; }

    }
}
