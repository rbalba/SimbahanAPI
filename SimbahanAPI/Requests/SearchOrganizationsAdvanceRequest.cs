using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimbahanAPI.Requests
{
    public class SearchOrganizationsAdvanceRequest
    {
        public string Keyword = "";
        public string Location = "";
        public string ParentOrganization = "";
        public string Schedule = "";
        public string Day = "";
        public string Time = "";
        public string Language = "";
        public bool ActivityWorship = false;
        public bool ActivityBibleStudy = false;
        public bool ActivityMass = false;
        public bool ActivityRetreats = false;
        public bool ActivityRecollection = false;
        public bool ActivityVolunteerWorks = false;
        public bool ActivityTalks = false;
        public bool ActivityCamp = false;
        public bool AttendeeMen = false;
        public bool AttendeeWomen = false;
        public bool AttendeeSingle = false;
        public bool AttendeeCouple = false;
        public bool AttendeeProfessional = false;
        public bool AttendeeStudent = false;
        public bool AttendeeMissionary = false;
        public bool AttendeeNonCatholic = false;
        public bool VenueChurch = false;
        public bool VenueMall = false;
        public bool VenueSchool = false;
        public bool VenuePrivate = false;
        public bool VenuePublic = false;
        public bool VentAircon = false;
        public bool VentCeilingFan = false;
        public bool VentWallFan = false;
        public bool VentStandFan = false;
        public bool ParkingStreet = false;
        public bool ParkingMall = false;
        public bool ParkingPrivate = false;
        public double Latitude = 0;
        public double Longitude = 0;
    }
}