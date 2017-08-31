using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class DailyReflectionReflectionService : IBasicService<DailyReflectionReflection>
    {
        public DailyReflectionReflectionService()
        {
            _reflectionReflectionTransformer = new ReflectionReflectionTransformer();
            _dailyReflectionTransformer = new DailyReflectionTransformer();
            _userTransformer = new UserTransformer();
        }

        public DailyReflectionReflection Create(DailyReflectionReflection model)
        {
            var dailyReflectionReflection = new DailyReflectionReflection();

            using (var sp = new StoredProcedure("spInsertUserDailyReflectionReflection"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", model.UserId);
                sp.SqlCommand.Parameters.AddWithValue("@reflectionID", model.DailyReflectionId);
                sp.SqlCommand.Parameters.AddWithValue("@title", model.Title);
                sp.SqlCommand.Parameters.AddWithValue("@content", model.ReflectionContent);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    dailyReflectionReflection = _reflectionReflectionTransformer.Transform(reader);
                    dailyReflectionReflection.DailyReflection = _dailyReflectionTransformer.Transform(reader);
                    dailyReflectionReflection.User = _userTransformer.Transform(reader);
                }
            }

            return dailyReflectionReflection;
        }

        public DailyReflectionReflection Find(int id)
        {
            throw new NotImplementedException();
        }

        public DailyReflectionReflection Update(int id, DailyReflectionReflection model)
        {
            var dailyReflectionReflection = new DailyReflectionReflection();

            using (var sp = new StoredProcedure("spUpdateUserDailyReflectionReflection"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", model.UserId);
                sp.SqlCommand.Parameters.AddWithValue("@reflectionID", model.DailyReflectionId);
                sp.SqlCommand.Parameters.AddWithValue("@title", model.Title);
                sp.SqlCommand.Parameters.AddWithValue("@content", model.ReflectionContent);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    dailyReflectionReflection = _reflectionReflectionTransformer.Transform(reader);
                    dailyReflectionReflection.DailyReflection = _dailyReflectionTransformer.Transform(reader);
                    dailyReflectionReflection.User = _userTransformer.Transform(reader);
                }
            }

            return dailyReflectionReflection;
        }

        public void Delete(DailyReflectionReflection model)
        {
            using (var sp = new StoredProcedure("spRemoveUserDailyReflectionReflection"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", model.UserId);
                sp.SqlCommand.Parameters.AddWithValue("@reflectionID", model.DailyReflectionId);


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
        public List<DailyReflectionReflection> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0,
            int relationId4 = 0)
        {
            var dailyRefletionReflections = new List<DailyReflectionReflection>();

            using (var sp = new StoredProcedure("spGetUserDailyReflectionReflection"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", relationId);
                sp.SqlCommand.Parameters.AddWithValue("@reflectionID", 0);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var reflection = _reflectionReflectionTransformer.Transform(reader);
                    reflection.DailyReflection = _dailyReflectionTransformer.Transform(reader);
                    reflection.User = _userTransformer.Transform(reader);
                    reflection.ReflectionDate = Convert.ToDateTime(reader["DateOfReflection"]);

                    dailyRefletionReflections.Add(reflection);
                }
            }

            return dailyRefletionReflections;
        }

        public bool UserHasReflection(int userId, int reflectionId)
        {
            using (var sp = new StoredProcedure("spCheckUserHasReflectionReflection"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);
                sp.SqlCommand.Parameters.AddWithValue("@reflectionID", reflectionId);

                var dr = sp.SqlCommand.ExecuteReader();

                while (dr.Read())
                    if (Convert.ToInt32(dr["result"]) == 1)
                        return true;
            }

            return false;
        }


        public DailyReflectionReflection GetUserReflection(int userId, int reflectionId)
        {
            var reflectionReflectionTransformer = new ReflectionReflectionTransformer();
            var reflection = new DailyReflectionReflection();

            using (var sp = new StoredProcedure("spGetUserDailyReflectionReflection"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);
                sp.SqlCommand.Parameters.AddWithValue("@reflectionID", reflectionId);

                var dr = sp.SqlCommand.ExecuteReader();

                while (dr.Read())
                {
                    reflection = reflectionReflectionTransformer.Transform(dr);
                    reflection.ReflectionDate = Convert.ToDateTime(dr["DateOfReflection"]);
                }
            }

            return reflection;
        }

        #region Private Properties

        private readonly DailyReflectionTransformer _dailyReflectionTransformer;
        private readonly ReflectionReflectionTransformer _reflectionReflectionTransformer;
        private readonly UserTransformer _userTransformer;

        #endregion
    }
}