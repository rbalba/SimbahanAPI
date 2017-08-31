namespace Simbahan.Models
{
    public class TimeStandard
    {
        public TimeStandard()
        {
            Time = new string[24];

            Time[0] = "12:00 - 1:00 AM";
            Time[1] = "1:00 - 2:00 AM";
            Time[2] = "2:00 - 3:00 AM";
            Time[3] = "3:00 - 4:00 AM";
            Time[4] = "4:00 - 5:00 AM";
            Time[5] = "5:00 - 6:00 AM";
            Time[6] = "6:00 - 7:00 AM";
            Time[7] = "7:00 - 8:00 AM";
            Time[8] = "8:00 - 9:00 AM";
            Time[9] = "9:00 - 10:00 AM";
            Time[10] = "10:00 - 11:00 AM";
            Time[11] = "11:00 - 12:00 PM";
            Time[12] = "12:00 - 1:00 PM";
            Time[13] = "1:00 - 2:00 PM";
            Time[14] = "2:00 - 3:00 PM";
            Time[15] = "3:00 - 4:00 PM";
            Time[16] = "4:00 - 5:00 PM";
            Time[17] = "5:00 - 6:00 PM";
            Time[18] = "6:00 - 7:00 PM";
            Time[19] = "7:00 - 8:00 PM";
            Time[20] = "8:00 - 9:00 PM";
            Time[21] = "9:00 - 10:00 PM";
            Time[22] = "10:00 - 11:00 PM";
            Time[23] = "11:00 - 12:00 PM";
        }

        public string[] Time { get; }
    }
}