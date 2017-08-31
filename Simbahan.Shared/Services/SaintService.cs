using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class SaintService : IBasicService<Saint>
    {
        private readonly SaintTransformer _saintTransformer;

        public SaintService()
        {
            _saintTransformer = new SaintTransformer();
        }

        public Saint Create(Saint model)
        {
            throw new NotImplementedException();
        }

        public Saint Find(int id)
        {
            var saint = new Saint();

            using (var sp = new StoredProcedure("spFindSaints"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@SaintsID", id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    saint = _saintTransformer.Transform(reader);
            }

            return saint;
        }

        public Saint Update(int id, Saint model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Saint model)
        {
            throw new NotImplementedException();
        }

        public List<Saint> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0, int relationId4 = 0)
        {
            var saints = new List<Saint>();

            using (var sp = new StoredProcedure("spGetSaints"))
            {
                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    saints.Add(_saintTransformer.Transform(reader));
            }

            return saints;
        }

        public List<string> GetPatron()
        {
            var patron = new List<string>();

            using (var sp = new StoredProcedure("spGetSaintsPatron"))
            {
                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    patron.Add(reader["Categories"].ToString());
            }

            return patron;
        }
    }
}