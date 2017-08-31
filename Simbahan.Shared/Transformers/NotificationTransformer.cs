using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class NotificationTransformer : Transformer<Notification>
    {
        protected override Notification Parse()
        {
            return new Notification
            {
                Id = ToInt(NotificationID),
                UserId = ToInt(UserID),
                Title = Title.ToString(),
                Description = Description.ToString(),
                CreatedAt = ToDateTime(CreatedAt),
                UpdatedAt = ToDateTime(UpdatedAt),
                HasRead = ToBoolean(HasRead),
                Action = Action.ToString()
            };
        }

        #region Private Properties

        private object NotificationID { get; set; }
        private object UserID { get; set; }
        private object Title { get; set; }
        private object Description { get; set; }
        private object CreatedAt { get; set; }
        private object UpdatedAt { get; set; }
        private object HasRead { get; set; }
        private object Action { get; set; }

        #endregion
    }
}