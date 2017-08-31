using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class OtherCatholicPrayerService : IBasicService<OtherCatholicPrayer>
    {
        private readonly OtherCatholicPrayerTransformer _otherCatholicPrayerTransformer;

        public OtherCatholicPrayerService()
        {
            _otherCatholicPrayerTransformer = new OtherCatholicPrayerTransformer();
        }

        public OtherCatholicPrayer Create(OtherCatholicPrayer model)
        {
            throw new NotImplementedException();
        }

        public OtherCatholicPrayer Find(int id)
        {
            var otherCatholicPrayer = new OtherCatholicPrayer();

            using (var sp = new StoredProcedure("spFindOtherCatholicPrayer"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@othercatholicprayerID", id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    otherCatholicPrayer = _otherCatholicPrayerTransformer.Transform(reader);
            }

            return otherCatholicPrayer;
        }

        public OtherCatholicPrayer Update(int id, OtherCatholicPrayer model)
        {
            throw new NotImplementedException();
        }

        public void Delete(OtherCatholicPrayer model)
        {
            throw new NotImplementedException();
        }

        public List<OtherCatholicPrayer> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0,
            int relationId4 = 0)
        {
            var otherCatholicPrayers = new List<OtherCatholicPrayer>();

            using (var sp = new StoredProcedure("spGetOtherCatholicPrayers"))
            {
                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    otherCatholicPrayers.Add(_otherCatholicPrayerTransformer.Transform(reader));
            }

            return otherCatholicPrayers;
        }

        public List<string> GetCategories()
        {
            var categories = new List<string>();

            using (var sp = new StoredProcedure("spGetOtherCatholicPrayerCategories"))
            {
                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    categories.Add(reader["Categories"].ToString());
            }

            return categories;
        }
    }
}