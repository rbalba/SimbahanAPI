using System;
using System.Collections.Generic;
using System.Data;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class DailyReflectionService : IBasicService<DailyReflection>
    {
        private readonly DailyReflectionTransformer _dailyReflectionTransformer;

        public DailyReflectionService()
        {
            _dailyReflectionTransformer = new DailyReflectionTransformer();
        }

        public DailyReflection Create(DailyReflection model)
        {
            throw new NotImplementedException();
        }

        public DailyReflection Find(int id)
        {
            var dailyReflection = new DailyReflection();

            using (var sp = new StoredProcedure("spFindDailyReflection"))
            {
                sp.SqlCommand.CommandType = CommandType.StoredProcedure;
                sp.SqlCommand.Parameters.AddWithValue("@id", id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    dailyReflection = _dailyReflectionTransformer.Transform(reader);
            }

            return dailyReflection;
        }

        public DailyReflection Update(int id, DailyReflection model)
        {
            throw new NotImplementedException();
        }

        public void Delete(DailyReflection model)
        {
            throw new NotImplementedException();
        }

        public List<DailyReflection> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0,
            int relationId4 = 0)
        {
            throw new NotImplementedException();
        }

        public DailyReflection FindByDate(DateTime date)
        {
            var reflection = new DailyReflection();

            using (var sp = new StoredProcedure("spGetDailyReflection"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@dateofReflection", date.ToString("MM/dd/yyyy"));

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    reflection = _dailyReflectionTransformer.Transform(reader);
            }

            return reflection;
        }
    }
}