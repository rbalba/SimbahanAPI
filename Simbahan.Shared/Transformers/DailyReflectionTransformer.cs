using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class DailyReflectionTransformer : Transformer<DailyReflection>
    {
        protected override DailyReflection Parse()
        {
            return new DailyReflection
            {
                Id = ToInt(DailyReflectionID),
                Source = Source.ToString(),
                DateOfReflection = ToDateTime(DateOfReflection),
                FirstContentTitle = FirstContentTitle.ToString(),
                FirstContent = FirstContent.ToString(),
                SecondContentTitle = SecondContentTitle.ToString(),
                SecondContent = SecondContent.ToString(),
                ThirdContentTitle = ThirdContentTitle.ToString(),
                ThirdContent = ThirdContent.ToString(),
                FourthContentTitle = FourthContentTitle.ToString(),
                FourthContent = FourthContent.ToString(),
                FifthContentTitle = FifthContentTitle.ToString(),
                FifthContent = FifthContent.ToString(),
                SixthContentTitle = SixthContentTitle.ToString(),
                SixthContent = SixthContent.ToString(),
                Prayer = Prayer.ToString(),
                CreatedBy = CreatedBy.ToString(),
                DateCreated = ToDateTime(DateCreated)
            };
        }

        #region Private Properties

        private object DailyReflectionID { get; set; }
        private object Source { get; set; }
        private object DateOfReflection { get; set; }
        private object FirstContentTitle { get; set; }
        private object FirstContent { get; set; }
        private object SecondContentTitle { get; set; }
        private object SecondContent { get; set; }
        private object ThirdContentTitle { get; set; }
        private object ThirdContent { get; set; }
        private object FourthContentTitle { get; set; }
        private object FourthContent { get; set; }
        private object FifthContentTitle { get; set; }
        private object FifthContent { get; set; }
        private object SixthContentTitle { get; set; }
        private object SixthContent { get; set; }
        private object Prayer { get; set; }
        private object CreatedBy { get; set; }
        private object DateCreated { get; set; }

        #endregion
    }
}