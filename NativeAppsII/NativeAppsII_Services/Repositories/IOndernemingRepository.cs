using NativeAppsII_Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeAppsII_Services.Repositories
{
    interface IOndernemingRepository
    {
        Onderneming CreateOnderneming(Onderneming onderneming);
        IEnumerable<Onderneming> GetOndernemingen();
        Onderneming GetOnderneming(int id);

    }
}
