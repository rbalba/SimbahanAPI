using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class FavoritesService
    {
        public List<Tuple<Church, User>> GetChurchFollowers(int churchId)
        {
            var churchTransformer = new ChurchTransformer();
            var userTransformer = new UserTransformer();
            var followers = new List<Tuple<Church, User>>();

            using (var sp = new StoredProcedure(""))
            {
                sp.SqlCommand.Parameters.AddWithValue("@churchID", churchId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var church = churchTransformer.Transform(reader);
                    var user = userTransformer.Transform(reader);
                    var follower = new Tuple<Church, User>(church, user);

                    followers.Add(follower);
                }
            }

            return followers;
        }

        public List<Organization> GetFavoriteOrganizations(int userId)
        {
            var organizationTransformer = new OrganizationTransformer();
            var organizations = new List<Organization>();

            using (var sp = new StoredProcedure("spGetFavoriteOrganization"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);


                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var organization = organizationTransformer.Transform(reader);


                    //char[] separator = { ',' };
                    //var photos = reader["ChurchPhotos"].ToString().Split(separator, StringSplitOptions.None);
                    //church.ChurchPhotos = new List<ChurchPhotosModel>();
                    //foreach (var strPix in photos)
                    //    church.ChurchPhotos.Add(new ChurchPhotosModel
                    //    {
                    //        ChurchPhotos = strPix == string.Empty ? "" : @"Images\Photos\" + strPix
                    //    });

                    organizations.Add(organization);
                }
            }

            return organizations;
        }

        public List<Church> GetFavoriteChurches(int userId)
        {
            var churchTransformer = new ChurchTransformer();
            var churches = new List<Church>();

            using (var sp = new StoredProcedure("spGetFavoriteChurch"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var church = churchTransformer.Transform(reader);


                    char[] separator = {','};
                    var photos = reader["ChurchPhotos"].ToString().Split(separator, StringSplitOptions.None);
                    church.ChurchPhotos = new List<string>();
                    foreach (var strPix in photos)
                        church.ChurchPhotos.Add(strPix == string.Empty ? "" : @"Images\Photos\" + strPix);

                    churches.Add(church);
                }
            }

            return churches;
        }

        public List<BibleVerse> GetFavoriteBibleVerses(int userId)
        {
            var verses = new List<BibleVerse>();
            var bibleVerseTransformer = new BibleVerseTransformer();

            using (var sp = new StoredProcedure("spGetFavoriteBibleVerse"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    verses.Add(bibleVerseTransformer.Transform(reader));
            }

            return verses;
        }

        public List<ReligiousQuote> GetFavoriteReligiousQuotes(int userId)
        {
            var religiousQuotes = new List<ReligiousQuote>();
            var religiousQuoteTransformer = new ReligiousQuoteTransformer();

            using (var sp = new StoredProcedure("spGetFavoriteReligiousQuote"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    religiousQuotes.Add(religiousQuoteTransformer.Transform(reader));
            }

            return religiousQuotes;
        }

        public List<MusicalInspiration> GetFavoriteInspirationalMusic(int userId)
        {
            var musicalInspirations = new List<MusicalInspiration>();
            var inspirationalMusicTransformer = new InspirationalMusicTransformer();

            using (var sp = new StoredProcedure("spGetFavoriteInspirationalMusic"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    musicalInspirations.Add(inspirationalMusicTransformer.Transform(reader));
            }

            return musicalInspirations;
        }

        public bool AddChurch(int userId, int simbahanId)
        {
            if (IsChurchAlreadyInFavorites(userId, simbahanId)) return false;

            using (var sp = new StoredProcedure("spInsertSimbahanFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@simbahanID", simbahanId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool AddOrganization(int userId, int organizationId)
        {
            if (IsOrganizationAlreadyInFavorites(userId, organizationId)) return false;

            using (var sp = new StoredProcedure("spInsertOrganizationFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@organizationID", organizationId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool AddBibleVerse(int userId, int bibleVerseId)
        {
            if (IsBibleVerseAlreadyInFavorites(userId, bibleVerseId)) return false;

            using (var sp = new StoredProcedure("spInsertBibleVerseFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@bibleVerseID", bibleVerseId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool AddReligiousQuote(int userId, int religiousQuoteId)
        {
            if (IsReligiousQuoteAlreadyInFavorites(userId, religiousQuoteId)) return false;

            using (var sp = new StoredProcedure("spInsertFavoriteReligiousQuote"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@religiousQuoteID", religiousQuoteId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool AddInspirationalMusic(int userId, int inspirationalMusicId)
        {
            if (IsInspirationalMusicAlreadyInFavorites(userId, inspirationalMusicId)) return false;

            using (var sp = new StoredProcedure("spInsertFavoriteInspirationalMusic"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@inspirationalMusicID", inspirationalMusicId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool IsBibleVerseAlreadyInFavorites(int userId, int bibleVerseId)
        {
            using (var sp = new StoredProcedure("spIsBibleVerseInFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@bibleVerseID", bibleVerseId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    return Convert.ToInt32(reader["result"]) == 1;
            }

            return true;
        }

        public bool IsReligiousQuoteAlreadyInFavorites(int userId, int religiousQuoteId)
        {
            using (var sp = new StoredProcedure("spIsReligiousQuoteInFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@religiousQuoteID", religiousQuoteId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    return Convert.ToInt32(reader["result"]) == 1;
            }

            return true;
        }

        public bool IsInspirationalMusicAlreadyInFavorites(int userId, int inspirationalMusicId)
        {
            using (var sp = new StoredProcedure("spIsInspirationalMusicInFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@inspirationalMusicID", inspirationalMusicId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    return Convert.ToInt32(reader["result"]) == 1;
            }

            return true;
        }

        public bool RemoveBibleVerse(int userId, int bibleVerseId)
        {
            if (!IsBibleVerseAlreadyInFavorites(userId, bibleVerseId)) return false;

            using (var sp = new StoredProcedure("spRemoveBibleVerseFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@bibleVerseID", bibleVerseId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool RemoveReligiousQuote(int userId, int religiousQuoteId)
        {
            if (!IsReligiousQuoteAlreadyInFavorites(userId, religiousQuoteId)) return false;

            using (var sp = new StoredProcedure("spRemoveReligiousQuoteFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@religiousQuoteID", religiousQuoteId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool RemoveInspirationalMusic(int userId, int inspirationalMusicId)
        {
            if (!IsInspirationalMusicAlreadyInFavorites(userId, inspirationalMusicId)) return false;

            using (var sp = new StoredProcedure("spRemoveInspirationalMusicFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@inspirationalMusicID", inspirationalMusicId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool RemoveChurch(int userId, int simbahanId)
        {
            if (!IsChurchAlreadyInFavorites(userId, simbahanId)) return false;

            using (var sp = new StoredProcedure("spRemoveChurchFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@simbahanID", simbahanId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool RemoveOrganization(int userId, int organizationId)
        {
            if (!IsOrganizationAlreadyInFavorites(userId, organizationId)) return false;

            using (var sp = new StoredProcedure("spRemoveOrganizationFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@organizationID", organizationId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool IsChurchAlreadyInFavorites(int userId, int simbahanId)
        {
            using (var sp = new StoredProcedure("spIsChurchInFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@simbahanID", simbahanId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    return Convert.ToInt32(reader["result"]) == 1;
            }

            return true;
        }

        public bool IsOrganizationAlreadyInFavorites(int userId, int organizationId)
        {
            using (var sp = new StoredProcedure("spIsOrganizationInFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@organizationID", organizationId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    return Convert.ToInt32(reader["result"]) == 1;
            }

            return true;
        }

        public List<BasicCatholicPrayer> GetFavoriteBasicCatholicPrayer(int userId)
        {
            var basicCatholicPrayer = new List<BasicCatholicPrayer>();
            var basicCatholicPrayerTransformer = new BasicCatholicPrayerTransformer();

            using (var sp = new StoredProcedure("spGetFavoriteBasicCatholicPrayer"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    basicCatholicPrayer.Add(basicCatholicPrayerTransformer.Transform(reader));
            }

            return basicCatholicPrayer;
        }

        public List<Devotion> GetFavoriteDevotion(int userId)
        {
            var devotion = new List<Devotion>();
            var devotionTransformer = new DevotionTransformer();

            using (var sp = new StoredProcedure("spGetFavoriteDevotion"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    devotion.Add(devotionTransformer.Transform(reader));
            }

            return devotion;
        }

        public List<OtherCatholicPrayer> GetFavoriteOtherCatholicPrayer(int userId)
        {
            var otherCatholicPrayers = new List<OtherCatholicPrayer>();
            var otherCatholicPrayerTransformer = new OtherCatholicPrayerTransformer();

            using (var sp = new StoredProcedure("spGetFavoriteOtherCatholicPrayer"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    otherCatholicPrayers.Add(otherCatholicPrayerTransformer.Transform(reader));
            }

            return otherCatholicPrayers;
        }

        public bool AddBasicCatholicPrayer(int userId, int basicCatholicPrayerId)
        {
            if (IsBasicCatholicPrayerAlreadyInFavorites(userId, basicCatholicPrayerId)) return false;

            using (var sp = new StoredProcedure("spInsertFavoriteBasicCatholicPrayer"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@basiccatholicprayerID", basicCatholicPrayerId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool AddDevotion(int userId, int devotionId)
        {
            if (IsDevotionAlreadyInFavorites(userId, devotionId)) return false;

            using (var sp = new StoredProcedure("spInsertFavoriteDevotion"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@devotionID", devotionId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool AddOtherCatholicPrayer(int userId, int othercatholicprayerId)
        {
            if (IsOtherCatholicPrayerAlreadyInFavorites(userId, othercatholicprayerId)) return false;

            using (var sp = new StoredProcedure("spInsertFavoriteOtherCatholicPrayer"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@othercatholicprayerID", othercatholicprayerId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool IsBasicCatholicPrayerAlreadyInFavorites(int userId, int basicCatholicPrayerId)
        {
            using (var sp = new StoredProcedure("spIsBasicCatholicPrayerInFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@basiccatholicprayerID", basicCatholicPrayerId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    return Convert.ToInt32(reader["result"]) == 1;
            }

            return true;
        }

        public bool IsDevotionAlreadyInFavorites(int userId, int devotionId)
        {
            using (var sp = new StoredProcedure("spIsDevotionInFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@devotionID", devotionId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    return Convert.ToInt32(reader["result"]) == 1;
            }

            return true;
        }

        public bool IsOtherCatholicPrayerAlreadyInFavorites(int userId, int othecatholicprayerId)
        {
            using (var sp = new StoredProcedure("spIsOtherCatholicPrayerInFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@othercatholicprayerID", othecatholicprayerId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    return Convert.ToInt32(reader["result"]) == 1;
            }

            return true;
        }

        public bool RemoveBasicCatholicPrayer(int userId, int basicCatholicPrayerId)
        {
            if (!IsBasicCatholicPrayerAlreadyInFavorites(userId, basicCatholicPrayerId)) return false;

            using (var sp = new StoredProcedure("spRemoveBasicCatholicPrayerFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@basiccatholicprayerID", basicCatholicPrayerId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool RemoveDevotion(int userId, int devotionId)
        {
            if (!IsDevotionAlreadyInFavorites(userId, devotionId)) return false;

            using (var sp = new StoredProcedure("spRemoveDevotionFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@devotionID", devotionId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }

        public bool RemoveOtherCatholicPrayer(int userId, int othercatholicprayerId)
        {
            if (!IsDevotionAlreadyInFavorites(userId, othercatholicprayerId)) return false;

            using (var sp = new StoredProcedure("spRemoveOtherCatholicPrayerFavorite"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@othercatholicprayerID", othercatholicprayerId);
                sp.SqlCommand.Parameters.AddWithValue("@userID", userId);

                sp.SqlCommand.ExecuteNonQuery();
            }

            return true;
        }
    }
}