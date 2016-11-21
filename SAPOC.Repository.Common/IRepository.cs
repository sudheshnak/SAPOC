using SAPOC.Repository.Common.Entity;
using System.Collections.Generic;


namespace SAPOC.Repository.Common
{
    public interface IRepository
    {
        List<Product> GetAllProduct();
        Product GetProductById(int id);
    }
}
