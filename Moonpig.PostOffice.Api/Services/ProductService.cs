using Moonpig.PostOffice.Data;
using System.Linq;

namespace Moonpig.PostOffice.Api.Services
{
    public interface IProductService
    {
        Product GetProductById(int supplierId);
    }

    public class ProductService : IProductService
    {
        public Product GetProductById(int productId)
        {
            DbContext dbContext = new DbContext();
            return dbContext.Products.SingleOrDefault(x => x.ProductId == productId);
        }
    }
}
