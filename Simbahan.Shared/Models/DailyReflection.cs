using System;

namespace Simbahan.Models
{
    public class DailyReflection
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public DateTime DateOfReflection { get; set; }
        public string FirstContentTitle { get; set; }
        public string FirstContent { get; set; }
        public string SecondContentTitle { get; set; }
        public string SecondContent { get; set; }
        public string ThirdContentTitle { get; set; }
        public string ThirdContent { get; set; }
        public string FourthContentTitle { get; set; }
        public string FourthContent { get; set; }
        public string FifthContentTitle { get; set; }
        public string FifthContent { get; set; }
        public string SixthContentTitle { get; set; }
        public string SixthContent { get; set; }
        public string Prayer { get; set; }
        public string FormattedDate => DateOfReflection.DayOfWeek + ", " + DateOfReflection.ToString("MMMM dd, yyyy");
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}