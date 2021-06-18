using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JYOTIRGAMAYA.Models
{
    public class MapViewModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public double CityLatitude { get; set; }
        public double CityLongitude { get; set; }
        public string CityDescription { get; set; }
        public string Icon { get; set; }
        public string Photo { get; set; }
    }
}