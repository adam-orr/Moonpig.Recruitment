using Moonpig.PostOffice.Api.Extensions;
using Moonpig.PostOffice.Api.Model;
using Moonpig.PostOffice.Data;
using System;
using System.Collections.Generic;

namespace Moonpig.PostOffice.Api.Services
{
    public interface IDespatchService
    {
        DespatchDate GetDespatchDate(List<int> ProductIds, DateTime orderDate);
    }

    public class DespatchService : IDespatchService
    {
        private readonly IProductService productService;
        private readonly ISupplierService supplierService;

        public DespatchService(IProductService productService, ISupplierService supplierService)
        {
            this.productService = productService;
            this.supplierService = supplierService;
        }

        public DespatchDate GetDespatchDate(List<int> productIds, DateTime orderDate)
        {
            if ((orderDate.DayOfWeek == DayOfWeek.Saturday) || (orderDate.DayOfWeek == DayOfWeek.Sunday))
            {
                orderDate = orderDate.GetNextWorkDay();
            }

            var maxLeadTime = orderDate;

            foreach (var Id in productIds)
            {
                DbContext dbContext = new DbContext();

                var product = productService.GetProductById(Id);

                if(product == null)
                {
                    return null;
                }

                var supplierId = product.SupplierId;
                var leadTime = supplierService.GetSupplierById(supplierId).LeadTime;

                if (orderDate.AddWorkdays(leadTime) > maxLeadTime)
                {
                    maxLeadTime = orderDate.AddWorkdays(leadTime);
                }
            }

            var despatchDate = new DespatchDate();

            if ((maxLeadTime.DayOfWeek == DayOfWeek.Saturday) || (maxLeadTime.DayOfWeek == DayOfWeek.Sunday))
            {
                despatchDate.Date = maxLeadTime.GetNextWorkDay();
            }
            else
            {
                despatchDate.Date = maxLeadTime;
            }

            return despatchDate;
        }
    }
}
