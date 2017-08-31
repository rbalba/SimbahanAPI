using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simbahan.Models
{
    public class clsRegisterModel
    {
        public Int32 regId { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String emailAddress { get; set; }
        public String Gender { get; set; }
        public String birthDay { get; set; }
        public String passWord { get; set; }
    }
}
