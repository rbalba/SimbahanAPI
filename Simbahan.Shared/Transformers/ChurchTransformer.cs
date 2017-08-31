using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class ChurchTransformer : Transformer<Church>
    {
        protected override Church Parse()
        {
            return new Church
            {
                Id = ToInt(SimbahanID),
                StreetNumber = ToInt(StreetNo),
                StreetName = StreetName.ToString(),
                Barangay = Barangay.ToString(),
                StateProvince = StateOrProvince.ToString(),
                City = City.ToString(),
                ZipCode = ZipCode.ToString(),
                CompleteAddress = CompleteAddress.ToString(),
                Diocese = Diocese.ToString(),
                Parish = Parish.ToString(),
                Priest = ParishPriest.ToString(),
                Vicariate = Vicariate.ToString(),
                DateEstablished = DateEstablished.ToString(),
                LastUpdate = ToDateTime(LastUpdate),
                FeastDay = FeastDay.ToString(),
                ContactNo = ContactNo.ToString(),
                Latitude = ToDouble(Latitude),
                Longitude = ToDouble(Longitude),
                HasAdorationChapel = ToBoolean(HasAdorationChapel),
                ChurchHistory = ChurchHistory.ToString(),
                OfficeHours = OfficeHours.ToString(),
                ChurchTypeId = ToInt(ChurchTypeID),
                Website = Website.ToString(),
                EmailAddress = EmailAddress.ToString(),
                DevotionSchedule = DevotionSchedule.ToString(),
                LocationId = ToInt(LocationID),
                ChurchCode = ChurchCode.ToString()
            };
        }

        #region Private Properties

        /// <summary>
        ///     Property that represents the column SimbahaID from data layer.
        /// </summary>
        private object SimbahanID { get; set; }

        /// <summary>
        ///     Property that represents the column StreetNo from data layer.
        /// </summary>
        private object StreetNo { get; set; }

        /// <summary>
        ///     Property that represents the column StreetName from data layer.
        /// </summary>
        private object StreetName { get; set; }

        /// <summary>
        ///     Property that represents the column Barangay from data layer.
        /// </summary>
        private object Barangay { get; set; }

        /// <summary>
        ///     Property that represents the column City from data layer.
        /// </summary>
        private object City { get; set; }

        /// <summary>
        ///     Property that represents the column StateOrProvince from data layer.
        /// </summary>
        private object StateOrProvince { get; set; }

        /// <summary>
        ///     Property that represents the column ZipCode from data layer.
        /// </summary>
        private object ZipCode { get; set; }

        /// <summary>
        ///     Property that represents the column CompleteAddress from data layer.
        /// </summary>
        private object CompleteAddress { get; set; }

        /// <summary>
        ///     Property that represents the column Diocese from data layer.
        /// </summary>
        private object Diocese { get; set; }

        /// <summary>
        ///     Property that represents the column Parish from data layer.
        /// </summary>
        private object Parish { get; set; }

        /// <summary>
        ///     Property that represents the column ParishPriest from data layer.
        /// </summary>
        private object ParishPriest { get; set; }

        /// <summary>
        ///     Property that represents the column Vicariate from data layer.
        /// </summary>
        private object Vicariate { get; set; }

        /// <summary>
        ///     Property that represents the column DateEstablished from data layer.
        /// </summary>
        private object DateEstablished { get; set; }

        /// <summary>
        ///     Property that represents the column DateCreated from data layer.
        /// </summary>
        private object DateCreated { get; set; }

        /// <summary>
        ///     Property that represents the column LastUpdate from data layer.
        /// </summary>
        private object LastUpdate { get; set; }

        /// <summary>
        ///     Property that represents the column FeastDay from data layer.
        /// </summary>
        private object FeastDay { get; set; }

        /// <summary>
        ///     Property that represents the column ContactNo from data layer.
        /// </summary>
        private object ContactNo { get; set; }

        /// <summary>
        ///     Property that represents the column Latitude from data layer.
        /// </summary>
        private object Latitude { get; set; }

        /// <summary>
        ///     Property that represents the column Longitude from data layer.
        /// </summary>
        private object Longitude { get; set; }

        /// <summary>
        ///     Property that represents the column HasAdorationChapel from data layer.
        /// </summary>
        private object HasAdorationChapel { get; set; }

        /// <summary>
        ///     Property that represents the column ChurchHistory from data layer.
        /// </summary>
        private object ChurchHistory { get; set; }

        /// <summary>
        ///     Property that represents the column OfficeHours from data layer.
        /// </summary>
        private object OfficeHours { get; set; }

        /// <summary>
        ///     Property that represents the column ChurchTypeID from data layer.
        /// </summary>
        private object ChurchTypeID { get; set; }

        //private object ChurchType { get; set; }

        /// <summary>
        ///     Property that represents the column Website from data layer.
        /// </summary>
        private object Website { get; set; }

        /// <summary>
        ///     Property that represents the column EmailAddress from data layer.
        /// </summary>
        private object EmailAddress { get; set; }

        /// <summary>
        ///     Property that represents the column DevotionSchedule from data layer.
        /// </summary>
        private object DevotionSchedule { get; set; }

        /// <summary>
        ///     Property that represents the column LocationID from data layer.
        /// </summary>
        private object LocationID { get; set; }

        /// <summary>
        ///     Property that represents the column ChurchCode from data layer.
        /// </summary>
        private object ChurchCode { get; set; }

        #endregion
    }
}