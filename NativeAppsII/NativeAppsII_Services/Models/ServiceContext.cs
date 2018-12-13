using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NativeAppsII_Services.Models
{
    public class ServiceContext : System.Data.Entity.DbContext
    {
        public ServiceContext() : base("name=ServiceContext")
        {
        }

        //public System.Data.Entity.DbSet<ServiceContext.Models.Evenement> Evenements { get; set; }

        //public System.Data.Entity.DbSet<ServiceContext.Models.Ondernemer> Ondernemers { get; set; }

        public System.Data.Entity.DbSet<NativeAppsII_Services.Models.Onderneming> Ondernemingen { get; set; }


        //public System.Data.Entity.DbSet<ServiceContext.Models.Promotie> Promoties { get; set; }
    }
}