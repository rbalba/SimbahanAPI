using System.Collections.Generic;

namespace Simbahan.Models
{
    public class Adoration
    {
        public Adoration()
        {
            Ventilations = new List<string>();
            Reviews = new List<ChurchReview>();
            Masses = new List<MassDetailsModel>();
            SundaySchedule = new List<MassDetailsModel>();
            MondaySchedule = new List<MassDetailsModel>();
            TuesdaySchedule = new List<MassDetailsModel>();
            WednesdaySchedule = new List<MassDetailsModel>();
            ThursdaySchedule = new List<MassDetailsModel>();
            FridaySchedule = new List<MassDetailsModel>();
            SaturdaySchedule = new List<MassDetailsModel>();
            Images = new List<string>();
        }

        public List<string> Images { get; set; }
        public List<MassDetailsModel> Masses { get; set; }
        public List<ChurchReview> Reviews { get; set; }
        public bool Is247 { get; set; }
        public int ChurchId { get; set; }
        public int AdorationId { get; set; }
        public string DisplayText { get; set; }
        public Church Church { get; set; }
        public List<string> Ventilations { get; set; }
        public List<MassDetailsModel> SundaySchedule { get; set; }
        public List<MassDetailsModel> MondaySchedule { get; set; }
        public List<MassDetailsModel> TuesdaySchedule { get; set; }
        public List<MassDetailsModel> WednesdaySchedule { get; set; }
        public List<MassDetailsModel> ThursdaySchedule { get; set; }
        public List<MassDetailsModel> FridaySchedule { get; set; }
        public List<MassDetailsModel> SaturdaySchedule { get; set; }
    }
}