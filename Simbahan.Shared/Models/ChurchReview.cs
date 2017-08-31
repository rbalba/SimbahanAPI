using System;
using System.Collections.Generic;
using Simbahan.Exceptions;
using Simbahan.Broadcast;
using Simbahan.Services;

namespace Simbahan.Models
{
    public class ChurchReview : IModel<ChurchReview>
    {
        #region Private Properties

        private readonly ChurchReviewService _churchReviewService;

        #endregion

        #region Constructors and Destructors

        public ChurchReview()
        {
            NotificationManager.GetInstance().RegisterOnChurchReviewPublished(this);
            _churchReviewService = new ChurchReviewService();

            Id = 0;
        }

        #endregion

        protected virtual void OnChurchReviewPublished(NotificationEventArgs e)
        {
            var churchReviewPublished = ChurchReviewPublished;

            churchReviewPublished?.Invoke(this, e);
        }

        private bool IsPersisted()
        {
            return Id != 0;
        }

        #region Public Properties

        public int Id { get; set; }
        public int UserId { get; set; }
        public int StarCount { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public int SimbahanId { get; set; }
        public Church Simbahan { get; set; }
        public User User { get; set; }
        public DateTime DateCreated { get; set; }
        public string FormattedDate => DateCreated.ToString("MMMM dd, yyyy");

        public event EventHandler<NotificationEventArgs> ChurchReviewPublished;

        #endregion

        #region IModel Implementation

        public ChurchReview Create()
        {
            if (IsPersisted())
                throw new ModelAlreadyPersistedException("This model is already saved in the database.");

            var churchReview = _churchReviewService.Create(this);

            var notification = new Notification
            {
                UserId = churchReview.UserId,
                Title = "New review was published",
                Description =
                    churchReview.User.FullName + " published a new review for " + churchReview.Simbahan.Parish,
                User = churchReview.User,
                Action = NotificationAction.OnChurchReviewPublished + churchReview.SimbahanId
            };

            OnChurchReviewPublished(new NotificationEventArgs(notification, churchReview.SimbahanId));

            return churchReview;
        }

        public ChurchReview Find(int id)
        {
            return _churchReviewService.Find(id);
        }

        public ChurchReview Update()
        {
            if (!IsPersisted())
                throw new ModelNotFoundException(
                    "Model cannot be found. Make sure the model is saved before you can update.");

            return _churchReviewService.Update(Id, this);
        }

        public void Delete()
        {
            if (!IsPersisted())
                throw new ModelNotFoundException(
                    "Model cannot be found. Make sure the model is saved before you can delete.");

            _churchReviewService.Delete(this);
        }

        public List<ChurchReview> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0, int relationId4 = 0)
        {
            return _churchReviewService.Get(relationId, relationId2, relationId3, relationId4);
        }

        #endregion
    }
}