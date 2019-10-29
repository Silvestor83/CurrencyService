using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CurrencyService.Common;

namespace CurrencyService.Controllers
{
    public class BaseApiController : ApiController
    {
        public ServiceProvider Services { get; set; }
        public BaseApiController()
        {
            Services = new ServiceProvider();
        }
    }
}