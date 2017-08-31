using System;
using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class OrganizationTransformer : Transformer<Organization>
    {
        protected override Organization Parse()
        {
            return new Organization
            {
                Id = ToInt(OrganizationID),
                Name = OrganizationName.ToString(),
                About = About.ToString(),
                LastUpdate = ToDateTime(LastUpdate),
                Address = CompleteAddress.ToString(),
                StreetName = StreetName.ToString(),
                StreetNumber = ToInt(StreetNo),
                Barangay = Barangay.ToString(),
                City = CityOrMunicipality.ToString(),
                State = StateOrProvince.ToString(),
                Country = Country.ToString(),
                DateEstablished = DateEstablished.ToString(),
                ParentOrganization = ParentOrganization.ToString(),
                Preacher = FeastBuilderOrPreacher.ToString(),
                Branch = BranchOrLocation.ToString(),
                ContactNumber = ContactNo.ToString(),
                Email = EmailAddress.ToString(),
                Website = Website.ToString(),
                Longitude = (float) Convert.ToDouble(Longitude),
                Latitude = (float) Convert.ToDouble(Latitude),
                RetreatSchedule = RetreatSchedule.ToString(),
                RecollectionSchedule = RecollectionSchedule.ToString(),
                TalkSchedule = TalkSchedule.ToString(),
                CampSchedule = CampSchedule.ToString(),
                VolunteerSchedule = VolunteerSchedule.ToString()
            };
        }

        #region Private members

        private object OrganizationID;
        private object OrganizationName;
        private object About;
        private object LastUpdate;
        private object CompleteAddress;
        private object StreetNo;
        private object StreetName;
        private object Barangay;
        private object CityOrMunicipality;
        private object StateOrProvince;
        private object Country;
        private object DateEstablished;
        private object ParentOrganization;
        private object FeastBuilderOrPreacher;
        private object BranchOrLocation;
        private object ContactNo;
        private object EmailAddress;
        private object Website;
        private object Latitude;
        private object Longitude;
        private object RetreatSchedule;
        private object RecollectionSchedule;
        private object TalkSchedule;
        private object CampSchedule;
        private object VolunteerSchedule;

        #endregion
    }
}