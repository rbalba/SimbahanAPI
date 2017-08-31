using System;
using Simbahan.Services;
using System.Collections.Generic;

namespace Simbahan.Models
{
    public class BibleVerse : IModel<BibleVerse>
    {
        #region Public Properties

        public int Id { get; set; }
        public DateTime DateOfVerse { get; set; }
        public string EmotionsReactions { get; set; }
        public string ChapterTitle { get; set; }
        public string BibleVerseContent { get; set; }
        public string Emotion { get; set; }
        public bool IsFavorite { get; set; }

        #endregion

        #region Private Properties

        private BibleVerseService _bibleVerseService;

        #endregion

        #region Constructors and Deconstructors

        public BibleVerse()
        {
            _bibleVerseService = new BibleVerseService();
        }

        #endregion

        #region IModel Implementation

        public BibleVerse Create()
        {
            return _bibleVerseService.Create(this);
        }

        public BibleVerse Find(int id)
        {
            return _bibleVerseService.Find(id);
        }

        public BibleVerse Update()
        {
            return _bibleVerseService.Update(Id, this);
        }

        public void Delete()
        {
            _bibleVerseService.Delete(this);
        }

        public List<BibleVerse> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0, int relationId4 = 0)
        {
            return _bibleVerseService.Get(relationId, relationId2, relationId3, relationId4);
        }

        #endregion
    }
}