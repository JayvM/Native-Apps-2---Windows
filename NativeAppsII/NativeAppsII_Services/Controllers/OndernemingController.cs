using NativeAppsII_Services.Models;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Description;

namespace NativeAppsII_Services.Controllers
{
    public class OndernemingController : ApiController
    {
        private ServiceContext serviceContext = new ServiceContext();


        //GET api/Companies
        [Route("api/getOndernemingen")]
        public IQueryable<Onderneming> GetOndernemingen()
        {
            return serviceContext.Ondernemingen.Include("Categorie").Include("Acties").Include("Evenementen").AsQueryable();
        }

        [ResponseType(typeof(Onderneming))]
        [Route("api/postOnderneming")]
        public IHttpActionResult PostOnderneming(Onderneming onderneming)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            serviceContext.Ondernemingen.Add(onderneming);
            serviceContext.SaveChanges();

            return Ok(onderneming);
        }


    }
}