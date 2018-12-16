﻿using NativeAppsII_Services.Models;
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
    public class CategorieController : ApiController
    {
        private ServiceContext serviceContext = new ServiceContext();

        //GET api/Companies
        [Route("api/getCategorieën")]
        public IQueryable<Categorie> GetCategorieën()
        {
            return serviceContext.Categorieën.AsQueryable();
        }

        [ResponseType(typeof(Categorie))]
        [Route("api/getCategorie/{naam}")]
        public IHttpActionResult GetCategorie(string naam)
        {
            Categorie categorie = serviceContext.Categorieën.Where(e => e.Naam == naam).FirstOrDefault();
            if (categorie == null)
            {
                return NotFound();
            }

            return Ok(categorie);
        }
    }
}