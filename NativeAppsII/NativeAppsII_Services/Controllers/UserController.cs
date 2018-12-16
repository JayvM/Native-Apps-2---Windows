using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Owin;
using NativeAppsII_Services.Models;
using Owin;

namespace NativeAppsII_Services.Controllers
{
    public class UserController : ApiController
    {
        private ServiceContext serviceContext = new ServiceContext();


        [ResponseType(typeof(Evenement))]
        [Route("api/Register")]
        public IHttpActionResult Register(User user)
        {
            if (serviceContext.users.FirstOrDefault(userRegistered => userRegistered.Gebruikersnaam == user.Gebruikersnaam) != null)
            {
                return BadRequest(ModelState);
            }
            user.Salt = HashPassword.GenerateSalt();
            user.Wachtwoord = HashPassword.Hashpassword(user.Wachtwoord + user.Salt);
            serviceContext.users.Add(user);
            serviceContext.SaveChanges();
            return Ok();
        }

        public User LogIn(string usernameVal, string passwordVal)
        {
            var userRegistered =  serviceContext.users.FirstOrDefault(user => user.Gebruikersnaam == usernameVal);
            var ww = HashPassword.Hashpassword(passwordVal + userRegistered.Salt);
            if(userRegistered.Wachtwoord == ww)
            {
                return userRegistered;
            }else
            {
                return null;
            }
        }
    }
}
