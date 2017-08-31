using System;
using Simbahan.Models;
using Simbahan.Services;

namespace Simbahan.Broadcast
{
    public class NotificationManager
    {
        private readonly NotificationService notificationService;

        #region Singleton Implementation

        private static NotificationManager _notificationManager;

        private NotificationManager()
        {
            notificationService = new NotificationService();
        }

        public static NotificationManager GetInstance()
        {
            if (_notificationManager == null)
                _notificationManager = new NotificationManager();

            return _notificationManager;
        }

        #endregion

        #region Event Registration Methods

        public void RegisterOnChurchReviewPublished(ChurchReview review)
        {
            review.ChurchReviewPublished += Review_ChurchReviewPublished;
        }

        public void RegisterOnChurchAnnouncementPublished(Announcement announcement)
        {
            announcement.ChurchAnnouncementPublished += Announcement_ChurchAnnouncementPublished;
        }

        #endregion

        #region Event Handlers

        private void Review_ChurchReviewPublished(object sender, NotificationEventArgs e)
        {
            var notification = notificationService.Create(e.Notification);

            var favoritesService = new FavoritesService();

            // Get the list of users who 'subscribe' to the church that this review was published on.
            var followers = favoritesService.GetChurchFollowers(e.Id);

            // Create a notifacation instance for every one of the 'subscribers'
            foreach (Tuple<Church, User> follower in followers)
                notificationService.CreateUserNotification(notification.Id, follower.Item2.Id);
        }

        private void Announcement_ChurchAnnouncementPublished(object sender, NotificationEventArgs e)
        {
            var notification = notificationService.Create(e.Notification);

            var favoritesService = new FavoritesService();

            var followers = favoritesService.GetChurchFollowers(e.Id);

            foreach (Tuple<Church, User> follower in followers)
                notificationService.CreateUserNotification(notification.Id, follower.Item2.Id);
        }

        #endregion
    }
}