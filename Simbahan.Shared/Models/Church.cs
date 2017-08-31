using System;
using System.Collections.Generic;

namespace Simbahan.Models
{
    public class Church
    {
        public Church()
        {
            SundayMassSchedule = new List<MassDetailsModel>();
            MondayMassSchedule = new List<MassDetailsModel>();
            TuesdayMassSchedule = new List<MassDetailsModel>();
            WednesdayMassSchedule = new List<MassDetailsModel>();
            ThursdayMassSchedule = new List<MassDetailsModel>();
            FridayMassSchedule = new List<MassDetailsModel>();
            SaturdayMassSchedule = new List<MassDetailsModel>();

            AdorationPhotos = new List<string>();
            ChurchParking = new List<string>();
            ChurchReviews = new List<ChurchReview>();
            AdorationVentilations = new List<string>();
            Announcements = new List<Announcement>();
            ChurchPhotos = new List<string>();
            ChurchThumbnails = new List<string>();
            ChurchSchedules = new List<string>();
            AdorationChapelSchedule = new List<AdorationChapelSchedule>();
            ConfessionDetails = new List<ConfessionSchedule>();
            Ventilations = new List<string>();
        }

        public int Id { get; set; }
        public int? StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string ZipCode { get; set; }
        public string CompleteAddress { get; set; }
        public string Diocese { get; set; }
        public string Parish { get; set; }
        public string Priest { get; set; }
        public string Vicariate { get; set; }
        public string DateEstablished { get; set; }
        public string FeastDay { get; set; }
        public string ContactNo { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool HasAdorationChapel { get; set; }
        public string ChurchHistory { get; set; }
        public string OfficeHours { get; set; }
        public int? ChurchTypeId { get; set; }
        public string ChurchType { get; set; }
        public string Website { get; set; }
        public string EmailAddress { get; set; }
        public string DevotionSchedule { get; set; }
        public int? LocationId { get; set; }
        public string LocationType { get; set; }
        public string ChurchCode { get; set; }
        public string BaptismDetails { get; set; }
        public List<ConfessionSchedule> ConfessionDetails { get; set; }
        public string WeddingDetails { get; set; }
        public string Country { get; set; }
        public List<string> Ventilations { get; set; }
        public List<MassDetailsModel> MassSchedules { get; set; }
        public List<string> ChurchPhotos { get; set; }
        public List<string> ChurchThumbnails { get; set; }
        public int? CountryId { get; set; }
        public List<string> AdorationPhotos { get; set; }
        public string AdorationDisplayText { get; set; }
        public List<string> ChurchParking { get; set; }
        public List<ChurchReview> ChurchReviews { get; set; }
        public List<string> ChurchSchedules { get; set; }
        public List<Announcement> Announcements { get; set; }
        public List<AdorationChapelSchedule> AdorationChapelSchedule { get; set; }
        public List<MassDetailsModel> SundayMassSchedule { get; set; }
        public List<MassDetailsModel> MondayMassSchedule { get; set; }
        public List<MassDetailsModel> TuesdayMassSchedule { get; set; }
        public List<MassDetailsModel> WednesdayMassSchedule { get; set; }
        public List<MassDetailsModel> ThursdayMassSchedule { get; set; }
        public List<MassDetailsModel> FridayMassSchedule { get; set; }
        public List<MassDetailsModel> SaturdayMassSchedule { get; set; }
        public List<string> AdorationVentilations { get; set; }
    }
}