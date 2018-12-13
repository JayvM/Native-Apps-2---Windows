using NativeAppsII_Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NativeAppsII_Services.Controllers
{
    public class OndernemingController : ApiController
    {
        private ServiceContext serviceContext = new ServiceContext();


        //GET api/Companies
        [Route("api/getOndernemingen")]
        public IQueryable<Onderneming> GetOndernemingen()
        {
            return serviceContext.Ondernemingen.AsQueryable();
        }
    }
}