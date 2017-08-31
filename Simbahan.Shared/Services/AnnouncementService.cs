using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class AnnouncementService : IBasicService<Announcement>
    {
        private readonly AnnouncementTransformer _announcementTransformer;

        public AnnouncementService()
        {
            _announcementTransformer = new AnnouncementTransformer();
        }

        public Announcement Create(Announcement model)
        {
            using (var sp = new StoredProcedure("spInsertAnnouncement"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@simbahanID", model.SimbahanId);
                sp.SqlCommand.Parameters.AddWithValue("@title", model.Title);
                sp.SqlCommand.Parameters.AddWithValue("@description", model.Description);
                sp.SqlCommand.Parameters.AddWithValue("@address", model.Venue);
                sp.SqlCommand.Parameters.AddWithValue("@startDate", model.StartDate);
                sp.SqlCommand.Parameters.AddWithValue("@startTime", model.StartTime);
                sp.SqlCommand.Parameters.AddWithValue("@endDate", model.EndDate);
                sp.SqlCommand.Parameters.AddWithValue("@endTime", model.EndTime);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    model = _announcementTransformer.Transform(reader);
            }

            return model;
        }

        public Announcement Find(int id)
        {
            var model = new Announcement();

            using (var sp = new StoredProcedure("spFindAnnouncement"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@announcementID", id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    model = _announcementTransformer.Transform(reader);
            }

            return model;
        }

        public Announcement Update(int id, Announcement model)
        {
            var announcement = new Announcement();

            using (var sp = new StoredProcedure("spUpdateAnnouncement"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@announcementID", id);
                sp.SqlCommand.Parameters.AddWithValue("@title", model.Title);
                sp.SqlCommand.Parameters.AddWithValue("@description", model.Description);
                sp.SqlCommand.Parameters.AddWithValue("@address", model.Venue);
                sp.SqlCommand.Parameters.AddWithValue("@imagePath", model.ImagePath);
                sp.SqlCommand.Parameters.AddWithValue("@simbahanID", model.SimbahanId);
                sp.SqlCommand.Parameters.AddWithValue("@startDate", model.StartDate);
                sp.SqlCommand.Parameters.AddWithValue("@startTime", model.StartTime);
                sp.SqlCommand.Parameters.AddWithValue("@endDate", model.EndDate);
                sp.SqlCommand.Parameters.AddWithValue("@endTime", model.EndTime);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    announcement = _announcementTransformer.Transform(reader);
            }

            return announcement;
        }

        public void Delete(Announcement model)
        {
            using (var sp = new StoredProcedure("spRemoveAnnouncement"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@announcementID", model.Id);

                sp.SqlCommand.ExecuteNonQuery();
            }
        }

        public List<Announcement> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0, int relationId4 = 0)
        {
            var announcements = new List<Announcement>();

            using (var sp = new StoredProcedure("spRemoveAnnouncement"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@simbahanID", relationId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    announcements.Add(_announcementTransformer.Transform(reader));
            }

            return announcements;
        }
    }
}