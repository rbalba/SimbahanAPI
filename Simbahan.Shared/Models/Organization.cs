using System;
using System.Collections.Generic;
using System.Linq;

namespace Simbahan.Models
{
    public class Organization
    {
        public Organization()
        {
            Locations = new List<string>();
            Ventilations = new List<string>();
            Parkings = new List<string>();
            Attendees = new List<string>();
            Activities = new List<string>();
            Photos = new List<string>();
            Masses = new List<OrganizationMass>();
            BibleStudySchedules = new List<OrganizationMass>();
            WorshipSchedules = new List<OrganizationMass>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Address { get; set; }
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string DateEstablished { get; set; }
        public string ParentOrganization { get; set; }
        public string Preacher { get; set; }
        public string Branch { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string RetreatSchedule { get; set; }
        public string RecollectionSchedule { get; set; }
        public string TalkSchedule { get; set; }
        public string CampSchedule { get; set; }
        public string VolunteerSchedule { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public List<string> Locations { get; set; }
        public List<string> Ventilations { get; set; }
        public List<string> Parkings { get; set; }
        public List<string> Attendees { get; set; }
        public List<string> Activities { get; set; }
        public List<string> Photos { get; set; }
        public List<OrganizationMass> Masses { get; set; }
        public List<OrganizationMass> BibleStudySchedules { get; set; }
        public List<OrganizationMass> WorshipSchedules { get; set; }

        public List<OrganizationMass> TodayMass => Masses
            .Where(mass => mass.ScheduleId == (int) DateTime.Now.DayOfWeek + 1)
            .ToList();
    }
}