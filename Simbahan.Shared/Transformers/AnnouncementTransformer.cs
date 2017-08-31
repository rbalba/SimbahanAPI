using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class AnnouncementTransformer : Transformer<Announcement>
    {
        protected override Announcement Parse()
        {
            return new Announcement
            {
                Id = ToInt(AnnouncementID),
                Description = AnnouncementDesc.ToString(),
                SimbahanId = ToInt(SimbahanID),
                StartDate = ToDateTime(StartDate),
                StartTime = StartTime.ToString(),
                EndDate = ToDateTime(EndDate),
                EndTime = EndTime.ToString(),
                Title = TitleContent.ToString(),
                Venue = Address.ToString(),
                ImagePath = ImagePath.ToString()
            };
        }

        #region Private Properties

        private object AnnouncementID { get; set; }
        private object AnnouncementDesc { get; set; }
        private object SimbahanID { get; set; }
        private object StartDate { get; set; }
        private object StartTime { get; set; }
        private object EndDate { get; set; }
        private object EndTime { get; set; }
        private object TitleContent { get; set; }
        private object Address { get; set; }
        private object ImagePath { get; set; }

        #endregion
    }
}