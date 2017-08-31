using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class MassDetailTransformer : Transformer<MassDetailsModel>
    {
        protected override MassDetailsModel Parse()
        {
            return new MassDetailsModel
            {
                Id = ToInt(MassDetailID),
                ScheduleId = ToInt(ScheduleID),
                TimeStandardId = ToInt(TimeStandardID),
                Time = Time.ToString(),
                Language = Language.ToString(),
                DateCreated = ToDateTime(DateCreated)
            };
        }

        #region Private Properties

        private object MassDetailID { get; set; }
        private object ScheduleID { get; set; }
        private object TimeStandardID { get; set; }
        private object Time { get; set; }
        private object Language { get; set; }
        private object DateCreated { get; set; }

        #endregion
    }
}