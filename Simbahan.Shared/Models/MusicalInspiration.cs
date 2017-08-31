namespace Simbahan.Models
{
    public class MusicalInspiration
    {
        public int Id { get; set; }
        public string EmotionsReactions { get; set; }
        public string Artist { get; set; }
        public string SongTitle { get; set; }
        public string Path { get; set; }
        public string Source { get; set; }
        public string Album { get; set; }
        public bool IsFavorite { get; set; }
        public string Lyrics { get; set; }
    }
}