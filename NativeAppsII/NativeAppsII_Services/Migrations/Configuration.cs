namespace NativeAppsII_Services.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NativeAppsII_Services.Models.ServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NativeAppsII_Services.Models.ServiceContext context)
        {
            context.Ondernemingen.AddOrUpdate(x => x.Id,
        new Models.Onderneming() { Id = 1,Categorie="Informatica",Gemeente="Gent",Land="Belgie",naam="Orbid",Openingsuur="9",Sluituur="10",Straat="Merbekestraart 5",Website="orbid.be"}
        );

            
        }
    }
}
