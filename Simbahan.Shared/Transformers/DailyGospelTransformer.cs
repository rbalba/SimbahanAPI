using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class DailyGospelTransformer : Transformer<DailyGospel>
    {
        protected override DailyGospel Parse()
        {
            return new DailyGospel
            {
                Id = ToInt(DailyReadingID),
                Source = Source.ToString(),
                DateOfGospel = ToDateTime(DateOfGospel),
                FirstReadingTitle = FirstReadingTitle.ToString(),
                FirstReadingContent = FirstReadingContent.ToString(),
                ResponsorialPsalmTitle = ResponsorialPsalmTitle.ToString(),
                ResponsorialPsalmContent = ResponsorialPsalmContent.ToString(),
                SecondReadingTitle = SecondReadingTitle.ToString(),
                SecondReadingContent = SecondReadingContent.ToString(),
                VerseBeforeGospelTitle = VerseBeforeGospelTitle.ToString(),
                VerseBeforeGospelContent = VerseBeforeGospelContent.ToString(),
                GospelTitle = GospelTitle.ToString(),
                GospelContent = GospelContent.ToString(),
                CreatedBy = CreatedBy.ToString(),
                DateCreated = ToDateTime(DateCreated)
            };
        }

        #region Private Properties

        private object DailyReadingID { get; set; }
        private object Source { get; set; }
        private object DateOfGospel { get; set; }
        private object FirstReadingTitle { get; set; }
        private object FirstReadingContent { get; set; }
        private object ResponsorialPsalmTitle { get; set; }
        private object ResponsorialPsalmContent { get; set; }
        private object SecondReadingTitle { get; set; }
        private object SecondReadingContent { get; set; }
        private object VerseBeforeGospelTitle { get; set; }
        private object VerseBeforeGospelContent { get; set; }
        private object GospelTitle { get; set; }
        private object GospelContent { get; set; }
        private object CreatedBy { get; set; }
        private object DateCreated { get; set; }

        #endregion
    }
}