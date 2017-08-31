using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class OrganizationReviewService : IBasicService<OrganizationReview>
    {
        public OrganizationReviewService()
        {
            _organizationReviewTransformer = new OrganizationReviewTransformer();
            _organizationTransformer = new OrganizationTransformer();
            _userTransformer = new UserTransformer();
        }

        public OrganizationReview Create(OrganizationReview model)
        {
            var organizationReview = new OrganizationReview();

            using (var sp = new StoredProcedure("spInsertOrganizationReview"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", model.UserId);
                sp.SqlCommand.Parameters.AddWithValue("@organizationID", model.OrganizationId);
                sp.SqlCommand.Parameters.AddWithValue("@title", model.Title);
                sp.SqlCommand.Parameters.AddWithValue("@rate", model.StarCount);
                sp.SqlCommand.Parameters.AddWithValue("@content", model.Comment);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    organizationReview = _organizationReviewTransformer.Transform(reader);
                    organizationReview.User = _userTransformer.Transform(reader);
                    organizationReview.Organization = _organizationTransformer.Transform(reader);
                }
            }

            return organizationReview;
        }

        public OrganizationReview Find(int id)
        {
            var organizationReview = new OrganizationReview();

            using (var sp = new StoredProcedure("spFindOrganizationReview"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@organizationReviewID", id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    organizationReview = _organizationReviewTransformer.Transform(reader);
                    organizationReview.User = _userTransformer.Transform(reader);
                    organizationReview.Organization = _organizationTransformer.Transform(reader);
                }
            }

            return organizationReview;
        }

        public OrganizationReview Update(int id, OrganizationReview model)
        {
            throw new NotImplementedException();
        }

        public void Delete(OrganizationReview model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="relationId">Organization Id</param>
        /// <param name="relationId2"></param>
        /// <param name="relationId3"></param>
        /// <param name="relationId4"></param>
        /// <returns></returns>
        public List<OrganizationReview> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0,
            int relationId4 = 0)
        {
            var organizationReviews = new List<OrganizationReview>();

            using (var sp = new StoredProcedure("spGetOrganizationReviews"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@organizationID", relationId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var organizationReview = _organizationReviewTransformer.Transform(reader);
                    organizationReview.User = _userTransformer.Transform(reader);
                    organizationReview.Organization = _organizationTransformer.Transform(reader);

                    organizationReviews.Add(organizationReview);
                }
            }

            return organizationReviews;
        }

        #region Private Properties

        private readonly OrganizationReviewTransformer _organizationReviewTransformer;
        private readonly OrganizationTransformer _organizationTransformer;
        private readonly UserTransformer _userTransformer;

        #endregion
    }
}