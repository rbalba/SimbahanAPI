using System;

namespace Simbahan.Models
{
    public class DailyGospel
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public DateTime DateOfGospel { get; set; }
        public string FirstReadingTitle { get; set; }
        public string FirstReadingContent { get; set; }
        public string ResponsorialPsalmTitle { get; set; }
        public string ResponsorialPsalmContent { get; set; }
        public string SecondReadingTitle { get; set; }
        public string SecondReadingContent { get; set; }
        public string VerseBeforeGospelTitle { get; set; }
        public string VerseBeforeGospelContent { get; set; }
        public string GospelTitle { get; set; }
        public string GospelContent { get; set; }
        public string FormattedDate => DateOfGospel.DayOfWeek + ", " + DateOfGospel.ToString("MMMM dd, yyyy");
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}