using System.Collections.Generic;
using Simbahan.Services;

namespace Simbahan.Models
{
    public class BasicCatholicPrayer : IModel<BasicCatholicPrayer>
    {
        #region Private Properties

        private readonly BasicCatholicPrayerService _basicCatholicPrayerService;

        #endregion

        #region Public Properties

        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Prayer { get; set; }
        public string Title { get; set; }

        #endregion

        #region Constructors and Deconstructors

        public BasicCatholicPrayer()
        {
            _basicCatholicPrayerService = new BasicCatholicPrayerService();
        }

        #endregion

        #region IModel Implementation

        public BasicCatholicPrayer Create()
        {
            return _basicCatholicPrayerService.Create(this);
        }

        public void Delete()
        {
            _basicCatholicPrayerService.Delete(this);
        }

        public BasicCatholicPrayer Find(int id)
        {
            return _basicCatholicPrayerService.Find(id);
        }

        public List<BasicCatholicPrayer> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0, int relationId4 = 0)
        {
            return _basicCatholicPrayerService.Get(relationId, relationId2, relationId3, relationId4);
        }

        public BasicCatholicPrayer Update()
        {
            return _basicCatholicPrayerService.Update(Id, this);
        }

        #endregion
    }
}