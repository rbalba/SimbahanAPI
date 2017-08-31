using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class InspirationalMusicTransformer : Transformer<MusicalInspiration>
    {
        protected override MusicalInspiration Parse()
        {
            return new MusicalInspiration
            {
                Id = ToInt(InspirationalMusicID),
                EmotionsReactions = EmotionsReactions.ToString(),
                Artist = Artist.ToString(),
                SongTitle = SongTitle.ToString(),
                Source = Source.ToString(),
                Lyrics = Lyrics.ToString(),
                Album = Album.ToString(),
                IsFavorite = ToBoolean(isFavorite),
                Path = FilePath.ToString()
            };
        }

        #region Private Properties

        private object InspirationalMusicID { get; set; }
        private object IMCode { get; set; }
        private object EmotionsReactions { get; set; }
        private object Artist { get; set; }
        private object SongTitle { get; set; }
        private object Album { get; set; }
        private object AlbumCover { get; set; }
        private object FilePath { get; set; }
        private object Source { get; set; }
        private object Lyrics { get; set; }
        private object isFavorite { get; set; }

        #endregion
    }
}