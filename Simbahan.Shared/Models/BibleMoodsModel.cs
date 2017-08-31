namespace Simbahan.Models
{
    public class BibleMoodsModel
    {
        public int Id { get; set; }
        public bool IsFavorite { get; set; }
        public string ChapterTitle { get; set; }
        public string BibleVerseContent { get; set; }
        public string Author { get; set; }
        public string Quote { get; set; }
    }
}