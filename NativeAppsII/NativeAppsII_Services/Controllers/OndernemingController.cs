using NativeAppsII_Services.Models;
using System.Linq;
using System.Runtime.Serialization;
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
            return serviceContext.Ondernemingen.Include("Categorie").Include("Acties").Include("Evenementen").AsQueryable();
        }


    }
}