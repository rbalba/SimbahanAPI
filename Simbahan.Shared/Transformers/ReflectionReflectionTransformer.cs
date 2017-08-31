using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class ReflectionReflectionTransformer : Transformer<DailyReflectionReflection>
    {
        protected override DailyReflectionReflection Parse()
        {
            return new DailyReflectionReflection
            {
                Id = ToInt(ID),
                UserId = ToInt(UserID),
                DailyReflectionId = ToInt(DailyReflectionID),
                Title = Title.ToString(),
                ReflectionContent = ReflectionContent.ToString(),
                CreatedAt = ToDateTime(CreatedAt),
                UpdatedAt = ToDateTime(UpdatedAt)
            };
        }

        #region Private Properties

        private object ID { get; set; }
        private object DailyReflectionID { get; set; }
        private object UserID { get; set; }
        private object Title { get; set; }
        private object ReflectionContent { get; set; }
        private object CreatedAt { get; set; }
        private object UpdatedAt { get; set; }

        #endregion
    }
}