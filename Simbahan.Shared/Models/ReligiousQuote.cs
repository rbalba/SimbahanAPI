using System;

namespace Simbahan.Models
{
    public class ReligiousQuote
    {
        public int Id { get; set; }
        public DateTime DateOfQuote { get; set; }
        public string Emotion { get; set; }
        public string Quote { get; set; }
        public string Author { get; set; }
        public string MobileDisplay { get; set; }
        public bool IsFavorite { get; set; }
    }
}