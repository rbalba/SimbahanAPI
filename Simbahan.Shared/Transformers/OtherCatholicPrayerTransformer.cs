using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class OtherCatholicPrayerTransformer : Transformer<OtherCatholicPrayer>
    {
        protected override OtherCatholicPrayer Parse()
        {
            return new OtherCatholicPrayer
            {
                Id = ToInt(OtherCatholicPrayerID),
                ImagePath = ImagePath.ToString(),
                Prayer = Prayer.ToString(),
                Title = Title.ToString(),
                Category = Categories.ToString()
            };
        }

        #region Private Properties

        private object OtherCatholicPrayerID { get; set; }
        private object ImagePath { get; set; }
        private object Prayer { get; set; }
        private object Title { get; set; }
        private object Categories { get; set; }

        #endregion
    }
}