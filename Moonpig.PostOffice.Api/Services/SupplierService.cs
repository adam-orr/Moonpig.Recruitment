using Moonpig.PostOffice.Data;
using System.Linq;

namespace Moonpig.PostOffice.Api.Services
{
    public interface ISupplierService
    {
        Supplier GetSupplierById(int supplierId);
    }

    public class SupplierService : ISupplierService
    {
        public Supplier GetSupplierById(int supplierId)
        {
            DbContext dbContext = new DbContext();
            return dbContext.Suppliers.SingleOrDefault(x => x.SupplierId == supplierId);
        }
    }
}
