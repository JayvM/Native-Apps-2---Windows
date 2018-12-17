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

        [Route("api/getOndernemingen/{id}")]
        public IQueryable<Onderneming> GetOndernemingen(int id)
        {
            return serviceContext.Ondernemingen.Include("Categorie").Include("Acties").Include("Evenementen").Where(on => on.UserId == id).AsQueryable();
        }

        [ResponseType(typeof(Actie))]
        [System.Web.Http.Route("api/bewerkOnderneming")]
        public IHttpActionResult BewerkActie(Onderneming onderneming)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = serviceContext.Ondernemingen.SingleOrDefault(a => a.Id == onderneming.Id);
            if (result != null)
            {
                result.Naam = onderneming.Naam;
                result.Land = onderneming.Land;
                result.Information = onderneming.Information;
                result.Openingsuur = onderneming.Openingsuur;
                result.Sluituur = onderneming.Sluituur;
                result.Straat = onderneming.Straat;
                result.Telefooonnummer = onderneming.Telefooonnummer;
                result.Website = onderneming.Website;
                result.CategorieId = onderneming.Categorie.Id;
                result.Gemeente = onderneming.Gemeente;
              
                serviceContext.SaveChanges();
                return Ok(onderneming);
            }
            return BadRequest(ModelState);
        }


    }
}