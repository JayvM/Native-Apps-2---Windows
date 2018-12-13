using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NativeAppsII_Services.Models
{
    public class ServiceContext : DbContext
    {
        public DbSet<Onderneming> Ondernemingen { get; set; }
        public DbSet<Categorie> Categorieën { get; set; }
        public ServiceContext() : base("name=ServiceContext")
        {
            Database.SetInitializer<ServiceContext>(new DropCreateDatabaseIfModelChanges<ServiceContext>());

        }

    }
}