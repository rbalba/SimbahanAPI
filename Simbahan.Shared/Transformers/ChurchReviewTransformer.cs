using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class ChurchReviewTransformer : Transformer<ChurchReview>
    {
        protected override ChurchReview Parse()
        {
            return new ChurchReview
            {
                Id = ToInt(ChurchReviewID),
                UserId = ToInt(UserID),
                SimbahanId = ToInt(SimbahanID),
                StarCount = ToInt(StarCount),
                Comment = Comment.ToString(),
                Title = Title.ToString(),
                DateCreated = ToDateTime(DateCreated)
            };
        }

        #region Private Properties

        private object ChurchReviewID { get; set; }
        private object UserID { get; set; }
        private object StarCount { get; set; }
        private object Comment { get; set; }
        private object SimbahanID { get; set; }
        private object Title { get; set; }
        private object DateCreated { get; set; }

        #endregion
    }
}