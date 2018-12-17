using NativeAppsII_Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace NativeAppsII_Services.Controllers
{
    public class EvenementController : ApiController
    {
        private ServiceContext serviceContext = new ServiceContext();



    [ResponseType(typeof(Evenement))]
    [Route("api/postEvenement")]
    public IHttpActionResult PostEvenement(Evenement evenement)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        serviceContext.Evenementen.Add(evenement);
        serviceContext.SaveChanges();

        return Ok(evenement);
    }

        [ResponseType(typeof(Evenement))]
        [Route("api/bewerkEvenement")]
        public IHttpActionResult bewerkEvenement(Evenement evenement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = serviceContext.Evenementen.SingleOrDefault(a => a.Id == evenement.Id);
            if (result != null)
            {
                serviceContext.Evenementen.Add(evenement);
                result.Datum = evenement.Datum;
                result.Beschrijving = evenement.Beschrijving;
                result.Plaats = evenement.Plaats;
                serviceContext.SaveChanges();
                return Ok(evenement);
            }
            return Ok(evenement);
        }


    }
}