using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class BibleVerseService : IBasicService<BibleVerse>
    {
        private readonly BibleVerseTransformer _bibleVerseTransformer;

        public BibleVerseService()
        {
            _bibleVerseTransformer = new BibleVerseTransformer();
        }

        public BibleVerse Create(BibleVerse model)
        {
            throw new NotImplementedException();
        }

        public void Delete(BibleVerse model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="relationId">Mood</param>
        /// <param name="relationId2">User Id</param>
        /// <param name="relationId3"></param>
        /// <param name="relationId4"></param>
        /// <returns></returns>
        public List<BibleVerse> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0, int relationId4 = 0)
        {
            var bibleVerses = new List<BibleVerse>();
            var mood = new Mood();

            using (var sp = new StoredProcedure("spGetBibleVerse"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@moods", mood.GetValue(relationId));
                sp.SqlCommand.Parameters.AddWithValue("@userID", relationId2);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var verse = _bibleVerseTransformer.Transform(reader);
                    verse.IsFavorite = Convert.ToBoolean(reader["isFavorite"]);

                    bibleVerses.Add(verse);
                }
            }

            return bibleVerses;
        }

        public BibleVerse Update(int id, BibleVerse model)
        {
            throw new NotImplementedException();
        }

        public BibleVerse Find(int id)
        {
            var bibleVerse = new BibleVerse();

            using (var sp = new StoredProcedure("spFindBibleVerse"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@verseID", id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    bibleVerse = _bibleVerseTransformer.Transform(reader);
            }

            return bibleVerse;
        }
    }
}