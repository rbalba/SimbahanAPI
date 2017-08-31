using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simbahan.Models
{
    public class clsCatholicOrgAnnounceModel
    {
        public Int32 AnnouncementId { get; set; }
        public Int32 OrganizationId { get; set; }
        public String TitleContent { get; set; }
        public String AnnouncementDesc { get; set; }
        public String Address { get; set; }
        public String ImagePath { get; set; }
        public DateTime StartDate { get; set; }
        public String StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public String EndTime { get; set; }
    }
}