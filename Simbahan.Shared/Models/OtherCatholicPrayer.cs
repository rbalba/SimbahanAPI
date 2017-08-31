namespace Simbahan.Models
{
    public class OtherCatholicPrayer
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Prayer { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string ClassName => Category.Replace(' ', '-').ToLower();
    }
}