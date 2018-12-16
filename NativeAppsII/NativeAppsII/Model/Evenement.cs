using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeAppsII.Model
{
    public class Evenement
    {
        public int Id { get; set; }
        public string Beschrijving { get; set; }
        public string Plaats { get; set; }
        public DateTime Datum { get; set; }
        public Onderneming Onderneming { get; set; }
        public int OndernemingId { get; set; }
        public TimeSpan getHour{ get{ return Datum.TimeOfDay; } }
        public DateTime getDate { get { return Datum.Date; } }

        public Evenement(string beschrijving, string plaats, DateTime datum, int ondernemingId)
        {
            Beschrijving = beschrijving;
            Plaats = plaats;
            Datum = datum;
            OndernemingId = ondernemingId;
        }

        public Evenement()
        {

        }
    }
}
