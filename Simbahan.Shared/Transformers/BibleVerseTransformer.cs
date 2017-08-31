using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class BibleVerseTransformer : Transformer<BibleVerse>
    {
        protected override BibleVerse Parse()
        {
            return new BibleVerse
            {
                Id = ToInt(BibleVerseID),
                ChapterTitle = ChapterTitle.ToString(),
                BibleVerseContent = BibleVerseContent.ToString(),
                DateOfVerse = ToDateTime(DateOfVerse),
                Emotion = EmotionsReactions.ToString()
            };
        }

        #region Private Properties

        private object BibleVerseID { get; set; }
        private object ChapterTitle { get; set; }
        private object BibleVerseContent { get; set; }
        private object DateOfVerse { get; set; }
        private object EmotionsReactions { get; set; }

        #endregion
    }
}