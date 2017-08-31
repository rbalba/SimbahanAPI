using System;

namespace Simbahan.Models
{
    public class DailyGospelReflection
    {
        public int Id { get; set; }
        public int DailyGospelId { get; set; }
        public DailyGospel DailyGospel { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string ReflectionContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime GospelDate { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FormattedGospelDate => GospelDate.ToString("MMMM dd, yyyy");
        public string FormattedDate => CreatedAt.ToString("MMMM dd, yyyy");
    }
}