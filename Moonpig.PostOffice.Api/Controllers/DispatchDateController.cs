namespace Moonpig.PostOffice.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Moonpig.PostOffice.Api.Services;

    [Route("api/DespatchDate")]
    public class DispatchDateController : Controller
    {
        private readonly IDispatchService dispatchService;

        public DispatchDateController(IDispatchService dispatchService)
        {
            this.dispatchService = dispatchService;
        }

        [HttpGet]
        public DispatchDate Get(List<int> productIds, DateTime orderDate)
        {
            return dispatchService.GetDispatchDate(productIds, orderDate);
        }
    }
}
