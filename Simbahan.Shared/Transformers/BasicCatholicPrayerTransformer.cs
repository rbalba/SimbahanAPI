using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class BasicCatholicPrayerTransformer : Transformer<BasicCatholicPrayer>
    {
        protected override BasicCatholicPrayer Parse()
        {
            return new BasicCatholicPrayer
            {
                Id = ToInt(BasicCatholicPrayerID),
                ImagePath = ImagePath.ToString(),
                Prayer = Prayer.ToString(),
                Title = Title.ToString()
            };
        }

        #region Private Properties

        private object BasicCatholicPrayerID { get; set; }
        private object ImagePath { get; set; }
        private object Prayer { get; set; }
        private object Title { get; set; }

        #endregion
    }
}