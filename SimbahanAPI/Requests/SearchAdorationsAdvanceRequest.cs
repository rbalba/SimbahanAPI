using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimbahanAPI.Requests
{
    public class SearchAdorationsAdvanceRequest
    {
        public string Keyword = "";
        public string Location = "";
        public string Day = "";
        public string Time = "";
        public string Adorationlocation = "";
        public bool HasAircondition = false;
        public bool HasStreetParking = false;
        public bool HasMallParking = false;
        public bool HasPrivateParking = false;
        public bool HasElectricFan = false;
        public double Longitude = 0;
        public double Latitude = 0;
    }
}