using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simbahan.Models
{
    public class clsAdorationModel
    {
        public Int32 adChapelId { get; set; }
        public Int32 adorationId { get; set; }
        public Int32 scheduleId { get; set; }
        public String timeSched { get; set; }
        public Int32 timeStandardId { get; set; }
    }
}
