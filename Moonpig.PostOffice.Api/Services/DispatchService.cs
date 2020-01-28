using Moonpig.PostOffice.Api.Extensions;
using Moonpig.PostOffice.Api.Model;
using Moonpig.PostOffice.Data;
using System;
using System.Collections.Generic;

namespace Moonpig.PostOffice.Api.Services
{
    public interface IDispatchService
    {
        DispatchDate GetDispatchDate(List<int> ProductIds, DateTime orderDate);
    }

    public class DispatchService : IDispatchService
    {
        private readonly IProductService productService;
        private readonly ISupplierService supplierService;

        public DispatchService(IProductService productService, ISupplierService supplierService)
        {
            this.productService = productService;
            this.supplierService = supplierService;
        }

        public DispatchDate GetDispatchDate(List<int> productIds, DateTime orderDate)
        {
            if ((orderDate.DayOfWeek == DayOfWeek.Saturday) || (orderDate.DayOfWeek == DayOfWeek.Sunday))
            {
                orderDate = orderDate.GetNextWorkDay();
            }

            var maxLeadTime = orderDate;

            foreach (var Id in productIds)
            {
                DbContext dbContext = new DbContext();

                var supplierId = productService.GetProductById(Id).SupplierId;
                var leadTime = supplierService.GetSupplierById(supplierId).LeadTime;

                if (orderDate.AddWorkdays(leadTime) > maxLeadTime)
                {
                    maxLeadTime = orderDate.AddWorkdays(leadTime);
                }
            }

            var dispatchDate = new DispatchDate();

            if ((maxLeadTime.DayOfWeek == DayOfWeek.Saturday) || (maxLeadTime.DayOfWeek == DayOfWeek.Sunday))
            {
                dispatchDate.Date = maxLeadTime.GetNextWorkDay();
            }
            else
            {
                dispatchDate.Date = maxLeadTime;
            }

            return dispatchDate;
        }
    }
}
