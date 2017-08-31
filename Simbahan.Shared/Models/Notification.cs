using System;
using System.Globalization;

namespace Simbahan.Models
{
    public class Notification
    {
        public Notification()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            HasRead = false;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HasRead { get; set; }
        public string Action { get; set; }

        public string TimeReadable
        {
            get
            {
                var time = DateTime.Now.Subtract(CreatedAt);

                var days = "";

                if (time.Days == 1)
                    days = "Yesterday at ";
                else if (time.Days > 1)
                    days = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(CreatedAt.Month) + " " +
                           CreatedAt.Day + " at ";
                else if (time.Hours == 1)
                    return time.Minutes + " minutes ago";
                else
                    return time.Hours + " hours ago";

                return days + time.Hours + ":" + time.Minutes;
            }
        }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}