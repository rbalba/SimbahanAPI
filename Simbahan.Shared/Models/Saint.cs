using System;

namespace Simbahan.Models
{
    public class Saint
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string Categories { get; set; }
        public string ClassName => Categories.Replace(' ', '-').ToLower(); 

        private string _imagePath;

        public string ImagePath
        {
            get
            {
                if (string.IsNullOrEmpty(_imagePath))
                    return "";

                return @"/Images/Saints/" + _imagePath;
            }
            set
            {
                _imagePath = value;
            }
        }

        public string FeastDay { get; set; }
        public string BirthDate { get; set; }
        public string DeathDate { get; set; }
        public string Patron { get; set; }
        public string CanonizeDate { get; set; }
        public string RelatedChurch { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}