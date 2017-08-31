using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class DailyGospelService : IBasicService<DailyGospel>
    {
        private readonly DailyGospelTransformer _dailyGospelTransformer;

        public DailyGospelService()
        {
            _dailyGospelTransformer = new DailyGospelTransformer();
        }

        public DailyGospel Create(DailyGospel model)
        {
            throw new NotImplementedException();
        }

        public DailyGospel Find(int id)
        {
            var dailyGospel = new DailyGospel();

            using (var sp = new StoredProcedure("spFindDailyGospel"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@id", id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    dailyGospel = _dailyGospelTransformer.Transform(reader);
                    dailyGospel.DateOfGospel = Convert.ToDateTime(reader["DateOfGospel"]);
                }
            }

            return dailyGospel;
        }

        public DailyGospel Update(int id, DailyGospel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(DailyGospel model)
        {
            throw new NotImplementedException();
        }

        public List<DailyGospel> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0,
            int relationId4 = 0)
        {
            throw new NotImplementedException();
        }

        public DailyGospel FindByDate(DateTime date)
        {
            var gospel = new DailyGospel();

            using (var sp = new StoredProcedure("spGetDailyGospel"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@dateofGospel", date.ToString("MM/dd/yyyy"));

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    gospel = _dailyGospelTransformer.Transform(reader);
            }

            return gospel;
        }
    }
}