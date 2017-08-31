using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simbahan.Models
{
    public class clsBibleSchedulesModel
    {
        public Int32 OrgBibleSchedId { get; set; }
        public Int32 OrgnizationId { get; set; }
        public Int32 ScheduleId { get; set; }
        public Int32 TimeStandardId { get; set; }
        public String Time { get; set; }
    }
}