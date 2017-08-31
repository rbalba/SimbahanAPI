using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class GospelReflectionTransformer : Transformer<DailyGospelReflection>
    {
        protected override DailyGospelReflection Parse()
        {
            return new DailyGospelReflection
            {
                Id = ToInt(Id),
                UserId = ToInt(UserID),
                DailyGospelId = ToInt(DailyGospelID),
                Title = Title.ToString(),
                ReflectionContent = ReflectionContent.ToString(),
                CreatedAt = ToDateTime(CreatedAt),
                UpdatedAt = ToDateTime(UpdatedAt)
            };
        }

        #region Private Properties

        private object Id { get; set; }
        private object UserID { get; set; }
        private object DailyGospelID { get; set; }
        private object Title { get; set; }
        private object ReflectionContent { get; set; }
        private object CreatedAt { get; set; }
        private object UpdatedAt { get; set; }

        #endregion
    }
}