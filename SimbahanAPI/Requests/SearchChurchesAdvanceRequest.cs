using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimbahanAPI.Requests
{
    public class SearchChurchesAdvanceRequest
    {
        public string Keyword = "";
        public string Location = "";
        public string MassDay = "";
        public string MassTime = "";
        public string MassLanguage = "";
        public string ConfessionDay = "";
        public string ConfessionTime = "";
        public bool HasAirCon = false;
        public bool HasCeilingFan = false;
        public bool HasOrdinaryFan = false;
        public string ChurchType = "";
        public string LocationId = "";
        public bool HasStreetParking = false;
        public bool HasMallParking = false;
        public bool HasPrivateParking = false;
        public double UserLatitude = 0;
        public double UserLongitude = 0;
        public int Limit = 0;
        public int Page = 1;
    }
}