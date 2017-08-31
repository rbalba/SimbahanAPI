using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class DevotionService : IBasicService<Devotion>
    {
        private readonly DevotionTransformer _devotionTransformer;

        public DevotionService()
        {
            _devotionTransformer = new DevotionTransformer();
        }

        public Devotion Create(Devotion model)
        {
            throw new NotImplementedException();
        }

        public Devotion Find(int id)
        {
            var devotion = new Devotion();

            using (var sp = new StoredProcedure("spFindDevotion"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@devotionID", id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    devotion = _devotionTransformer.Transform(reader);
            }

            return devotion;
        }

        public Devotion Update(int id, Devotion model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Devotion model)
        {
            throw new NotImplementedException();
        }

        public List<Devotion> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0,
            int relationId4 = 0)
        {
            var devotions = new List<Devotion>();

            using (var sp = new StoredProcedure("spGetDevotions"))
            {
                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    devotions.Add(_devotionTransformer.Transform(reader));
            }

            return devotions;
        }
    }
}