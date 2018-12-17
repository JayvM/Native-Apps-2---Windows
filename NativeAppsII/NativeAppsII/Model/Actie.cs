using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NativeAppsII.Model
{
   public class Actie
    {
        public int Id { get; set; }
        public String Beschrijving { get; set; }
        public String QR { get; set; }
        public DateTime GeldigTot { get; set; }
        public Onderneming onderneming { get; set; }
        public int OndernemingId { get; set; }
        public TimeSpan getHour { get { return GeldigTot.TimeOfDay; } }
        public DateTime getDate { get { return GeldigTot.Date; } }

        public Actie(string beschrijving, DateTime geldigTot, int ondernemingId)
        {
            Beschrijving = beschrijving;
            GeldigTot = geldigTot;
            OndernemingId = ondernemingId;
        }

        public Actie()
        {

        }
    }
}
