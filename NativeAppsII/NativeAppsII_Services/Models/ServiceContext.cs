using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;

namespace NativeAppsII_Services.Models
{
    public class ServiceContext : DbContext
    {
        public DbSet<Onderneming> Ondernemingen { get; set; }
        public DbSet<Categorie> Categorieën { get; set; }
        public DbSet<Actie> Acties { get; set; }
        public DbSet<Evenement> Evenementen { get; set; }
        public DbSet<User> users { get; set; }
        public ServiceContext() : base("name=ServiceContext")
        {
            //Database.SetInitializer<ServiceContext>(new DropCreateDatabaseIfModelChanges<ServiceContext>());

        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
        }

    }
}