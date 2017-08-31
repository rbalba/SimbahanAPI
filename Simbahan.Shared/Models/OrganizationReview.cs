using System;
using System.Collections.Generic;
using Simbahan.Exceptions;
using Simbahan.Services;

namespace Simbahan.Models
{
    public class OrganizationReview : IModel<OrganizationReview>
    {
        #region Private Properties

        private readonly OrganizationReviewService _organizationReviewService;

        #endregion

        #region Constructors and Destructors

        public OrganizationReview()
        {
            _organizationReviewService = new OrganizationReviewService();
            Id = 0;
        }

        #endregion

        private bool IsPersisted()
        {
            return Id != 0;
        }

        #region Public Properties

        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public int StarCount { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }
        public string FormattedDate => DateCreated.ToString("MMMM dd, yyyy");
        public User User { get; set; }
        public Organization Organization { get; set; }

        #endregion

        #region IModel Implementation

        public OrganizationReview Create()
        {
            if (IsPersisted())
                throw new ModelAlreadyPersistedException("This model is already saved in the database.");

            return _organizationReviewService.Create(this);
        }

        public OrganizationReview Find(int id)
        {
            return _organizationReviewService.Find(id);
        }

        public OrganizationReview Update()
        {
            if (!IsPersisted())
                throw new ModelNotFoundException(
                    "Model cannot be found. Make sure the model is saved before you can update.");

            return _organizationReviewService.Update(Id, this);
        }

        public void Delete()
        {
            if (!IsPersisted())
                throw new ModelNotFoundException(
                    "Model cannot be found. Make sure the model is saved before you can delete.");

            _organizationReviewService.Delete(this);
        }

        public List<OrganizationReview> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0,
            int relationId4 = 0)
        {
            return _organizationReviewService.Get(relationId, relationId2, relationId3, relationId4);
        }

        #endregion
    }
}