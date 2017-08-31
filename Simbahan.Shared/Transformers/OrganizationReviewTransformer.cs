using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class OrganizationReviewTransformer : Transformer<OrganizationReview>
    {
        protected override OrganizationReview Parse()
        {
            return new OrganizationReview
            {
                Id = ToInt(OrganizationReviewID),
                OrganizationId = ToInt(OrganizationID),
                UserId = ToInt(UserID),
                StarCount = ToInt(StarCount),
                Title = Title.ToString(),
                Comment = Comment.ToString(),
                DateCreated = ToDateTime(DateCreated)
            };
        }

        #region Private Properties

        public object OrganizationReviewID { get; set; }
        public object UserID { get; set; }
        public object OrganizationID { get; set; }
        public object StarCount { get; set; }
        public object Comment { get; set; }
        public object Title { get; set; }
        public object DateCreated { get; set; }

        #endregion
    }
}