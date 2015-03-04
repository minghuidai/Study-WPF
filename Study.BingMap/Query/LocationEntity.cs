using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.BingMap.Query
{
    public class LocationEntity
    {
        public string EntityTypeID { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string Distance { get; set; }
    }
}
