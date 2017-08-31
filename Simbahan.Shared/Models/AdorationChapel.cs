namespace Simbahan.Models
{
    public class AdorationChapel
    {
        public int Id { get; set; }
        public int SimbahanId { get; set; }
        public bool IsOpen24By7 { get; set; }
        public int ScheduleId { get; set; }
        public string Time { get; set; }
    }
}