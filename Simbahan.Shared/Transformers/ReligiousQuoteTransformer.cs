using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class ReligiousQuoteTransformer : Transformer<ReligiousQuote>
    {
        protected override ReligiousQuote Parse()
        {
            return new ReligiousQuote
            {
                Id = ToInt(ReligiousQuoteID),
                Author = Author.ToString(),
                Quote = Quote.ToString(),
                Emotion = EmotionsReactions.ToString(),
                DateOfQuote = ToDateTime(DateOfQuote),
                MobileDisplay = DisplayListForMobile.ToString()
            };
        }

        #region Private Properties

        private object ReligiousQuoteID { get; set; }
        private object DateOfQuote { get; set; }
        private object RQCode { get; set; }
        private object EmotionsReactions { get; set; }
        private object Author { get; set; }
        private object Quote { get; set; }
        private object DisplayListForMobile { get; set; }

        #endregion
    }
}