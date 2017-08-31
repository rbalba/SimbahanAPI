using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class VisitaIglesiaService : IBasicService<VisitaIglesia>
    {
        private readonly ChurchTransformer _churchTransformer;
        private readonly UserTransformer _userTransformer;

        private readonly VisitaIglesiaTransformer _visitaIglesiaTransformer;
        // TODO: CREATE A TRANSFORMER FOR STATUS.

        public VisitaIglesiaService()
        {
            _visitaIglesiaTransformer = new VisitaIglesiaTransformer();
            _churchTransformer = new ChurchTransformer();
            _userTransformer = new UserTransformer();
        }

        public VisitaIglesia Create(VisitaIglesia model)
        {
            var visitaIglesia = new VisitaIglesia();

            using (var sp = new StoredProcedure("spInsertVisitaIglesia"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", model.UserId);
                sp.SqlCommand.Parameters.AddWithValue("@simbahanID", model.SimbahanId);
                sp.SqlCommand.Parameters.AddWithValue("@statusID", model.StatusId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    visitaIglesia = _visitaIglesiaTransformer.Transform(reader);
                    visitaIglesia.Church = _churchTransformer.Transform(reader);
                    visitaIglesia.User = _userTransformer.Transform(reader);
                }
            }

            return visitaIglesia;
        }

        public VisitaIglesia Find(int id)
        {
            throw new NotImplementedException();
        }

        public VisitaIglesia Update(int id, VisitaIglesia model)
        {
            var visitaIglesia = new VisitaIglesia();

            using (var sp = new StoredProcedure("spUpdateVisitaIglesiaStatus"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", model.UserId);
                sp.SqlCommand.Parameters.AddWithValue("@simbahanID", model.SimbahanId);
                sp.SqlCommand.Parameters.AddWithValue("@statusID", model.StatusId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    visitaIglesia = _visitaIglesiaTransformer.Transform(reader);
                    visitaIglesia.Church = _churchTransformer.Transform(reader);
                    visitaIglesia.User = _userTransformer.Transform(reader);
                }
            }

            return visitaIglesia;
        }

        public void Delete(VisitaIglesia model)
        {
            using (var sp = new StoredProcedure("spResetUserVisitaIglesia"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", model.UserId);

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
        public List<VisitaIglesia> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0,
            int relationId4 = 0)
        {
            var visitaIglesias = new List<VisitaIglesia>();

            using (var sp = new StoredProcedure("spGetVisitaIglesia"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", relationId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var visitaIglesia = _visitaIglesiaTransformer.Transform(reader);
                    visitaIglesia.Church = _churchTransformer.Transform(reader);
                    visitaIglesia.User = _userTransformer.Transform(reader);

                    char[] separator = {','};
                    var photos = reader["ChurchPhotos"].ToString().Split(separator, StringSplitOptions.None);

                    foreach (var photo in photos)
                        visitaIglesia.Church.ChurchPhotos.Add(
                            photo == string.Empty ? "" : @"Images\Photos\Thumbnails\" + photo
                        );

                    visitaIglesias.Add(visitaIglesia);
                }
            }
            return visitaIglesias;
        }

        public bool HasExistingData(int id)
        {
            using (var sp = new StoredProcedure("spCheckUserHasExistingVisitaIglesia"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", id);

                var dr = sp.SqlCommand.ExecuteReader();

                while (dr.Read())
                    if (Convert.ToInt32(dr["result"]) == 1)
                        return true;
            }

            return false;
        }

        public void ResetCurrentlyHere(int userId)
        {
            using (var sp = new StoredProcedure("spResetCurrentlyHere"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }
        }
    }
}