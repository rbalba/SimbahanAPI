using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simbahan.Models
{
    public class clsConfessionModel
    {
        public Int32 ConfessionID { get; set; }
        public Int32 SimbahanID { get; set; }
        public Int32 ScheduleID { get; set; }
        public String Time { get; set; }
        public String Text { get; set; }
        public Int32 TimeStandardID { get; set; }
    }
}
