using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class ReligiousQuoteService : IBasicService<ReligiousQuote>
    {
        private readonly ReligiousQuoteTransformer _religiousQuoteTransformer;

        public ReligiousQuoteService()
        {
            _religiousQuoteTransformer = new ReligiousQuoteTransformer();
        }

        public ReligiousQuote Create(ReligiousQuote model)
        {
            throw new NotImplementedException();
        }

        public ReligiousQuote Find(int id)
        {
            var religiousQuote = new ReligiousQuote();

            using (var sp = new StoredProcedure("spFindReligiousQuote"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@religiousQuoteID", id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    religiousQuote = _religiousQuoteTransformer.Transform(reader);
            }

            return religiousQuote;
        }

        public ReligiousQuote Update(int id, ReligiousQuote model)
        {
            throw new NotImplementedException();
        }

        public void Delete(ReligiousQuote model)
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
        public List<ReligiousQuote> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0,
            int relationId4 = 0)
        {
            var mood = new Mood();
            var religiousQuotes = new List<ReligiousQuote>();

            using (var sp = new StoredProcedure("spGetReligiousQuote"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@moods", mood.GetValue(relationId));
                sp.SqlCommand.Parameters.AddWithValue("@userID", relationId2);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var religiousQuote = _religiousQuoteTransformer.Transform(reader);
                    religiousQuote.IsFavorite = Convert.ToBoolean(reader["isFavorite"]);

                    religiousQuotes.Add(religiousQuote);
                }
            }

            return religiousQuotes;
        }
    }
}