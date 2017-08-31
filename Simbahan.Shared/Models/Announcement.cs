using System;
using System.Collections.Generic;
using Simbahan.Exceptions;
using Simbahan.Broadcast;
using Simbahan.Services;

namespace Simbahan.Models
{
    public class Announcement : IModel<Announcement>
    {
        #region Private Properties

        /// <summary>
        ///     Service implementation that talks to the database.
        /// </summary>
        private readonly AnnouncementService _announcementService;

        #endregion

        #region Constructors and Deconstructors

        public Announcement()
        {
            _announcementService = new AnnouncementService();
            NotificationManager.GetInstance().RegisterOnChurchAnnouncementPublished(this);

            Id = 0;
        }

        #endregion

        protected virtual void OnChurchAnnouncementPublished(NotificationEventArgs e)
        {
            var churchAnnouncementPublished = ChurchAnnouncementPublished;

            churchAnnouncementPublished?.Invoke(this, e);
        }

        private bool IsPersisted()
        {
            return Id != 0;
        }

        #region Public Properties

        /// <summary>
        ///     Unique identifier for announcement
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Unique identifier for its relation to a <seealso cref="Church">Church</seealso>
        /// </summary>
        public int SimbahanId { get; set; }

        /// <summary>
        ///     The date this announcement begins.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     Formatted string used to display on a plain string.
        /// </summary>
        public string FormattedStartDate => StartDate.ToString("MMM d, yyyy");

        /// <summary>
        ///     Until when this announcement is due.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        ///     Formmated string used to display on a plain string.
        /// </summary>
        public string FormattedEndDate => EndDate.ToString("MMM d, yyyy");

        /// <summary>
        ///     Time this announcement will start.
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        ///     Time until this announcement is due.
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        ///     Announcement heading.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Announcement details.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Path to the default image.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        ///     Place where this announcement will happen.
        /// </summary>
        public string Venue { get; set; }

        /// <summary>
        ///     An event that will be raised whenever an announcement is saved.
        /// </summary>
        public event EventHandler<NotificationEventArgs> ChurchAnnouncementPublished;

        #endregion

        #region IModel Implementation

        public Announcement Create()
        {
            if (IsPersisted())
                throw new ModelAlreadyPersistedException("This model is already saved in the database.");

            var announcement = _announcementService.Create(this);

            var notification = new Notification
            {
                UserId = Auth.user().Id,
                Title = "Church announcement was published",
                Description = Auth.user().FullName + " has published a new announcement for church",
                User = Auth.user(),
                Action = NotificationAction.OnChurchAnnouncementPublished + SimbahanId
            };

            OnChurchAnnouncementPublished(new NotificationEventArgs(notification, SimbahanId));

            return announcement;
        }

        public Announcement Find(int id)
        {
            return _announcementService.Find(id);
        }

        public Announcement Update()
        {
            if (!IsPersisted())
                throw new ModelNotFoundException(
                    "Model cannot be found. Make sure the model is saved before you can update.");

            return _announcementService.Update(Id, this);
        }

        public void Delete()
        {
            if (!IsPersisted())
                throw new ModelNotFoundException(
                    "Model cannot be found. Make sure the model is saved before you can delete.");

            _announcementService.Delete(this);
        }

        public List<Announcement> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0, int relationId4 = 0)
        {
            return _announcementService.Get(relationId, relationId2, relationId3, relationId4);
        }

        #endregion
    }
}