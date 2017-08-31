using System;
using Simbahan.Models;

namespace Simbahan.Transformers
{
    public class SaintTransformer : Transformer<Saint>
    {
        protected override Saint Parse()
        {
            return new Saint
            {
                Id = Convert.ToInt64(SaintID),
                Name = Name.ToString(),
                Biography = Biography.ToString(),
                FeastDay = FeastDay.ToString(),
                Categories = Categories.ToString(),
                BirthDate = BirthDate.ToString(),
                DeathDate = DeathDate.ToString(),
                CanonizeDate = CanonizeDate.ToString(),
                RelatedChurch = RelatedChurch.ToString(),
                Patron = Patron.ToString(),
                ImagePath = ImagePath.ToString(),
                PublicationDate = ToDateTime(PublicationDate)
            };
        }

        #region Private Properties

        private object SaintID { get; set; }
        private object Name { get; set; }
        private object Biography { get; set; }
        private object ImagePath { get; set; }
        private object Categories { get; set; }
        private object FeastDay { get; set; }
        private object BirthDate { get; set; }
        private object DeathDate { get; set; }
        private object Patron { get; set; }
        private object CanonizeDate { get; set; }
        private object RelatedChurch { get; set; }
        private object PublicationDate { get; set; }

        #endregion
    }
}