using SAPOC.Contract.Entity;
using System.Collections.Generic;

namespace SAPOC.Contract
{
    public interface IProductService
    {
        List<Product> GetAllProduct();
        Product GetProductById(int id);
    }
}
