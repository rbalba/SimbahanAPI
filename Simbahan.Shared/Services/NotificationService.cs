using System;
using System.Collections.Generic;
using System.Data;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class NotificationService : IBasicService<Notification>
    {
        private readonly NotificationTransformer _notificationTransformer;
        private readonly UserTransformer _userTransformer;

        public NotificationService()
        {
            _notificationTransformer = new NotificationTransformer();
            _userTransformer = new UserTransformer();
        }

        public Notification Create(Notification notification)
        {
            var createdNotification = new Notification();

            using (var sp = new StoredProcedure("spCreateNotification"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@title", notification.Title);
                sp.SqlCommand.Parameters.AddWithValue("@comment", notification.Description);
                sp.SqlCommand.Parameters.AddWithValue("@userID", notification.UserId);
                sp.SqlCommand.Parameters.AddWithValue("@action", notification.Action);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    createdNotification = _notificationTransformer.Transform(reader);
                    createdNotification.User = _userTransformer.Transform(reader);
                }
            }

            return createdNotification;
        }

        public Notification Find(int id)
        {
            throw new NotImplementedException();
        }

        public Notification Update(int id, Notification model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Notification model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="relationId">User Id</param>
        /// <param name="relationId2"></param>
        /// <param name="relationId3"></param>
        /// <param name="relationId4"></param>
        /// <returns></returns>
        public List<Notification> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0, int relationId4 = 0)
        {
            var notifications = new List<Notification>();

            using (var sp = new StoredProcedure("spGetNotifications"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", relationId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var notification = _notificationTransformer.Transform(reader);
                    notification.User = _userTransformer.Transform(reader);

                    notifications.Add(notification);
                }
            }

            return notifications;
        }

        public void CreateUserNotification(int notificationId, int userId)
        {
            using (var sp = new StoredProcedure(""))
            {
                sp.SqlCommand.CommandText = string.Format(
                    "INSERT INTO [Notification_User] (UserId, NotificationId, HasRead) VALUES ({0}, {1}, 0)",
                    userId, notificationId);
                sp.SqlCommand.CommandType = CommandType.Text;

                sp.SqlCommand.ExecuteNonQuery();
            }
        }

        public void MarkAsRead(int notificationId, int userId)
        {
            using (var sp = new StoredProcedure("spMarkNotificationAsRead"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@notificationID", notificationId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }
        }

        public int GetNumberOfUnreadNotification(int userId)
        {
            var unreadNotificationCount = 0;

            using (var sp = new StoredProcedure("spGetNumberOfUnreadNotification"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    unreadNotificationCount = Convert.ToInt32(reader["count"]);
            }

            return unreadNotificationCount;
        }
    }
}