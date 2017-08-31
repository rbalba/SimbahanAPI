using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class BasicCatholicPrayerService : IBasicService<BasicCatholicPrayer>
    {
        private readonly BasicCatholicPrayerTransformer _basicCatholicPrayerTransformer;

        public BasicCatholicPrayerService()
        {
            _basicCatholicPrayerTransformer = new BasicCatholicPrayerTransformer();
        }

        public BasicCatholicPrayer Create(BasicCatholicPrayer model)
        {
            throw new NotImplementedException();
        }

        public BasicCatholicPrayer Find(int id)
        {
            var basicCatholicPrayer = new BasicCatholicPrayer();

            using (var sp = new StoredProcedure("spFindBasicCatholicPrayer"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@BasicCatholicPrayerID", id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    basicCatholicPrayer = _basicCatholicPrayerTransformer.Transform(reader);
            }

            return basicCatholicPrayer;
        }

        public BasicCatholicPrayer Update(int id, BasicCatholicPrayer model)
        {
            throw new NotImplementedException();
        }

        public void Delete(BasicCatholicPrayer model)
        {
            throw new NotImplementedException();
        }

        public List<BasicCatholicPrayer> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0,
            int relationId4 = 0)
        {
            var basicCatholicPrayers = new List<BasicCatholicPrayer>();

            using (var sp = new StoredProcedure("spGetBasicCatholicPrayers"))
            {
                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    basicCatholicPrayers.Add(_basicCatholicPrayerTransformer.Transform(reader));
            }

            return basicCatholicPrayers;
        }
    }
}