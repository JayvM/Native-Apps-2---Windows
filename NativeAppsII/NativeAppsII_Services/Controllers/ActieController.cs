using NativeAppsII_Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace NativeAppsII_Services.Controllers
{
    public class ActieController : ApiController
    {
        private ServiceContext serviceContext = new ServiceContext();
        


        [ResponseType(typeof(Actie))]
        [System.Web.Http.Route("api/postActie")]
        public IHttpActionResult PostActie(Actie actie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            serviceContext.Acties.Add(actie);
            serviceContext.SaveChanges();

            return Ok(actie);
        }
    }
}