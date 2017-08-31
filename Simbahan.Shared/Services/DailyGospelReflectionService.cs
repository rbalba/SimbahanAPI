using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class DailyGospelReflectionService : IBasicService<DailyGospelReflection>
    {
        public DailyGospelReflectionService()
        {
            _dailyGospelReflectionTransformer = new GospelReflectionTransformer();
            _dailyGospelTransformer = new DailyGospelTransformer();
            _userTransformer = new UserTransformer();
        }

        public DailyGospelReflection Create(DailyGospelReflection model)
        {
            var dailyGospelReflection = new DailyGospelReflection();

            using (var sp = new StoredProcedure("spInsertUserDailyGospelReflection"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", model.UserId);
                sp.SqlCommand.Parameters.AddWithValue("@gospelID", model.DailyGospelId);
                sp.SqlCommand.Parameters.AddWithValue("@title", model.Title);
                sp.SqlCommand.Parameters.AddWithValue("@content", model.ReflectionContent);


                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    dailyGospelReflection = _dailyGospelReflectionTransformer.Transform(reader);
                    dailyGospelReflection.DailyGospel = _dailyGospelTransformer.Transform(reader);
                    dailyGospelReflection.User = _userTransformer.Transform(reader);
                }
            }

            return dailyGospelReflection;
        }

        public DailyGospelReflection Find(int id)
        {
            throw new NotImplementedException();
        }

        public DailyGospelReflection Update(int id, DailyGospelReflection model)
        {
            var dailyGospelReflection = new DailyGospelReflection();

            using (var sp = new StoredProcedure("spUpdateUserDailyGospelReflection"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", model.UserId);
                sp.SqlCommand.Parameters.AddWithValue("@gospelID", model.DailyGospelId);
                sp.SqlCommand.Parameters.AddWithValue("@title", model.Title);
                sp.SqlCommand.Parameters.AddWithValue("@content", model.ReflectionContent);


                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    dailyGospelReflection = _dailyGospelReflectionTransformer.Transform(reader);
                    dailyGospelReflection.DailyGospel = _dailyGospelTransformer.Transform(reader);
                    dailyGospelReflection.User = _userTransformer.Transform(reader);
                }
            }

            return dailyGospelReflection;
        }

        public void Delete(DailyGospelReflection model)
        {
            using (var sp = new StoredProcedure("spRemoveUserDailyGospelReflection"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", model.UserId);
                sp.SqlCommand.Parameters.AddWithValue("@gospelID", model.DailyGospelId);

                sp.SqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="relationId">User Id</param>
        /// <param name="relationId2"></param>
        /// <param name="relationId3"></param>
        /// <param name="relationId4"></param>
        /// <returns></returns>
        public List<DailyGospelReflection> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0,
            int relationId4 = 0)
        {
            var dailyGospelReflections = new List<DailyGospelReflection>();

            using (var sp = new StoredProcedure("spGetUserDailyGospelReflection"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", relationId);
                sp.SqlCommand.Parameters.AddWithValue("@gospelID", 0);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var dailyGospelReflection = _dailyGospelReflectionTransformer.Transform(reader);
                    dailyGospelReflection.DailyGospel = _dailyGospelTransformer.Transform(reader);
                    dailyGospelReflection.User = _userTransformer.Transform(reader);
                    dailyGospelReflection.GospelDate = Convert.ToDateTime(reader["DateOfGospel"]);

                    dailyGospelReflections.Add(dailyGospelReflection);
                }
            }

            return dailyGospelReflections;
        }

        public bool UserHasReflection(int userId, int gospelId)
        {
            using (var sp = new StoredProcedure("spCheckUserHasGospelReflection"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);
                sp.SqlCommand.Parameters.AddWithValue("@gospelID", gospelId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    if (Convert.ToInt32(reader["result"]) == 1)
                        return true;
            }

            return false;
        }

        public DailyGospelReflection GetUserReflection(int userId, int gospelId)
        {
            var dailyGospelReflection = new DailyGospelReflection();

            using (var sp = new StoredProcedure("spGetUserDailyGospelReflection"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);
                sp.SqlCommand.Parameters.AddWithValue("@gospelID", gospelId);


                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    dailyGospelReflection = _dailyGospelReflectionTransformer.Transform(reader);
                    dailyGospelReflection.DailyGospel = _dailyGospelTransformer.Transform(reader);
                    dailyGospelReflection.User = _userTransformer.Transform(reader);
                    dailyGospelReflection.GospelDate = Convert.ToDateTime(reader["DateOfGospel"]);
                }
            }

            return dailyGospelReflection;
        }

        #region Private Properties

        private readonly GospelReflectionTransformer _dailyGospelReflectionTransformer;
        private readonly DailyGospelTransformer _dailyGospelTransformer;
        private readonly UserTransformer _userTransformer;

        #endregion
    }
}