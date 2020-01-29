namespace Moonpig.PostOffice.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Moonpig.PostOffice.Api.Services;

    [Route("api/DespatchDate")]
    public class DespatchDateController : Controller
    {
        private readonly IDespatchService despatchService;

        public DespatchDateController(IDespatchService despatchService)
        {
            this.despatchService = despatchService;
        }

        [HttpGet]
        public DespatchDate Get(List<int> productIds, DateTime orderDate)
        {
            return despatchService.GetDespatchDate(productIds, orderDate);
        }
    }
}
