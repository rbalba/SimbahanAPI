using System;
using System.Data;
using Simbahan.Models;
using System.Configuration;
using System.Data.SqlClient;
using Simbahan.Transformers;
using System.Collections.Generic;
using Simbahan.Database;

namespace Simbahan.Services
{
    public class ChurchReviewService : IBasicService<ChurchReview>
    {
        private readonly ChurchReviewTransformer _churchReviewTransformer;

        private readonly ChurchTransformer _churchTransformer;

        private readonly UserTransformer _userTransformer;

        public ChurchReviewService()
        {
            _churchReviewTransformer = new ChurchReviewTransformer();
            _churchTransformer = new ChurchTransformer();
            _userTransformer = new UserTransformer();
        }

        public ChurchReview Create(ChurchReview model)
        {
            var churchReview = new ChurchReview();

            using (var sp = new StoredProcedure("spInsertChurchReview"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", model.UserId);
                sp.SqlCommand.Parameters.AddWithValue("@simbahanID", model.SimbahanId);
                sp.SqlCommand.Parameters.AddWithValue("@title", model.Title);
                sp.SqlCommand.Parameters.AddWithValue("@rate", model.StarCount);
                sp.SqlCommand.Parameters.AddWithValue("@content", model.Comment);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    churchReview = _churchReviewTransformer.Transform(reader);
                    churchReview.User = _userTransformer.Transform(reader);
                    churchReview.Simbahan = _churchTransformer.Transform(reader);
                }
            }

            return churchReview;
        }

        public ChurchReview Find(int id)
        {
            var churchReview = new ChurchReview();

            using (var sp = new StoredProcedure("spFindChurchReview"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@churchReviewID", id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    churchReview = _churchReviewTransformer.Transform(reader);
                    churchReview.User = _userTransformer.Transform(reader);
                    churchReview.Simbahan = _churchTransformer.Transform(reader);
                }
            }

            return churchReview;
        }

        public ChurchReview Update(int id, ChurchReview model)
        {
            var churchReview = new ChurchReview();

            using (var sp = new StoredProcedure("spUpdateChurchReviews"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", model.UserId);
                sp.SqlCommand.Parameters.AddWithValue("@simbahanID", model.SimbahanId);
                sp.SqlCommand.Parameters.AddWithValue("@title", model.Title);
                sp.SqlCommand.Parameters.AddWithValue("@starCount", model.StarCount);
                sp.SqlCommand.Parameters.AddWithValue("@comment", model.Comment);
                sp.SqlCommand.Parameters.AddWithValue("@churchReviewID", id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    churchReview = _churchReviewTransformer.Transform(reader);
                    churchReview.User = _userTransformer.Transform(reader);
                    churchReview.Simbahan = _churchTransformer.Transform(reader);
                }
            }

            return churchReview;
        }

        public void Delete(ChurchReview model)
        {
            using (var sp = new StoredProcedure("spRemoveChurchReview"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@churchReviewID", model.Id);

                sp.SqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="relationId">Simbahan Id</param>
        /// <param name="relationId2"></param>
        /// <param name="relationId3"></param>
        /// <param name="relationId4"></param>
        /// <returns></returns>
        public List<ChurchReview> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0, int relationId4 = 0)
        {
            var churchReviews = new List<ChurchReview>();

            using (var sp = new StoredProcedure("spGetChurchReviews"))
            {
                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var churchReview = _churchReviewTransformer.Transform(reader);
                    churchReview.User = _userTransformer.Transform(reader);
                    churchReview.Simbahan = _churchTransformer.Transform(reader);

                    churchReviews.Add(churchReview);
                }
            }

            return churchReviews;
        }
    }
}