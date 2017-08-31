using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simbahan.Models
{
    public class clsCatholicOrganization
    {
        public Int32 OrgID { get; set; }
        public String OrgName { get; set; }
        public DateTime LastUpdate { get; set; }
        public String CompleteAddress { get; set; }
        public String StreetNo { get; set; }
        public String StreetName { get; set; }
        public String Barangay { get; set; }
        public String CityOrMunicipality { get; set; }
        public String StateOrProvince { get; set; }
        public String Country { get; set; }
        public DateTime DateEstablished { get; set; }
        public String ParentOrganization { get; set; }
        public String FeastBuilderOrPreacher { get; set; }
        public String BranchOrLocation { get; set; }
        public String ContactNo { get; set; }
        public String EmailAddress { get; set; }
        public String Website { get; set; }
        public Int32 OrgLocID { get; set; }
        public String RetreatSchedule { get; set; }
        public String RecollectSchedule { get; set; }
        public String TalkSchedule { get; set; }
        public String CampSchedule { get; set; }
        public String VolunteerSchedule { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public String About { get; set; }
    }
}
