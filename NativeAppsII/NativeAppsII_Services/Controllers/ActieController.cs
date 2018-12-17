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

        [ResponseType(typeof(Actie))]
        [System.Web.Http.Route("api/bewerkActie")]
        public IHttpActionResult BewerkActie(Actie actie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = serviceContext.Acties.SingleOrDefault(a => a.Id == actie.Id);
            if(result != null)
            {
                result.GeldigTot = actie.GeldigTot;
                result.Beschrijving = actie.Beschrijving;
                serviceContext.SaveChanges();
                return Ok(actie);
            }
            return BadRequest(ModelState);
        }
    }
}