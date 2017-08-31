using System;

namespace Simbahan.Models
{
    public class DailyReflectionReflection
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int DailyReflectionId { get; set; }
        public DailyReflection DailyReflection { get; set; }
        public string Title { get; set; }
        public string ReflectionContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ReflectionDate { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FormattedReflectionDate => ReflectionDate.ToString("MMMM dd, yyyy");
        public string FormattedDate => CreatedAt.ToString("MMMM dd, yyyy");
    }
}