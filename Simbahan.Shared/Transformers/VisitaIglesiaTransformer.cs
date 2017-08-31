using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class VisitaIglesiaTransformer : Transformer<VisitaIglesia>
    {
        protected override VisitaIglesia Parse()
        {
            return new VisitaIglesia
            {
                UserId = ToInt(UserID),
                StatusId = ToInt(StatusID),
                SimbahanId = ToInt(SimbahanID),
                Status = Name.ToString(),
                CreatedAt = ToDateTime(DateCreated),
                UpdatedAt = ToDateTime(DateUpdated)
            };
        }

        #region Private Properties

        /// <summary>
        ///     Property that represents the column UserId from data layer.
        /// </summary>
        private object UserID { get; set; }

        /// <summary>
        ///     Property that represents the column StatusId from data layer.
        /// </summary>
        private object StatusID { get; set; }

        public object SimbahanID { get; set; }

        /// <summary>
        ///     Property that represents the column Name from data layer.
        /// </summary>
        private object Name { get; set; }

        /// <summary>
        ///     Property that represents the column DateCreated from data layer.
        /// </summary>
        private object DateCreated { get; set; }

        /// <summary>
        ///     Property that represents the column DateUpdated from data layer.
        /// </summary>
        private object DateUpdated { get; set; }

        #endregion
    }
}