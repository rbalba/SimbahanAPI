using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class ChurchService
    {
        #region Constructors and Deconstructors

        public ChurchService()
        {
            _churchTransformer = new ChurchTransformer();
            _massDetailTransformer = new MassDetailTransformer();
            _userTransformer = new UserTransformer();
            _churchReviewTransformer = new ChurchReviewTransformer();
            _announcementTransformer = new AnnouncementTransformer();
        }

        #endregion

        public Church Find(int churchId)
        {
            var church = new Church {Id = churchId};

            var schedulesList = new List<string>();

            using (var sp = new StoredProcedure("spGetChurches"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@simbahanID", church.Id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    church = _churchTransformer.Transform(reader);

                char[] separator = {','};

                // Ventilation Processing...
                reader.NextResult();
                while (reader.Read())
                {
                    var ventilations = reader["VentType"].ToString().Split(separator, StringSplitOptions.None);

                    foreach (var ventilation in ventilations)
                        church.Ventilations.Add(ventilation);
                }

                // Mass Schedules Processing...
                reader.NextResult();
                while (reader.Read())
                {
                    var massDetail = _massDetailTransformer.Transform(reader);

                    switch (Convert.ToInt32(reader["ScheduleId"]))
                    {
                        case 1:
                            massDetail.Days = "Sunday";
                            church.SundayMassSchedule.Add(massDetail);
                            break;

                        case 2:
                            massDetail.Days = "Monday";
                            church.MondayMassSchedule.Add(massDetail);
                            break;

                        case 3:
                            massDetail.Days = "Tuesday";
                            church.TuesdayMassSchedule.Add(massDetail);
                            break;

                        case 4:
                            massDetail.Days = "Wednesday";
                            church.WednesdayMassSchedule.Add(massDetail);
                            break;

                        case 5:
                            massDetail.Days = "Thursday";
                            church.ThursdayMassSchedule.Add(massDetail);
                            break;

                        case 6:
                            massDetail.Days = "Friday";
                            church.FridayMassSchedule.Add(massDetail);
                            break;

                        case 7:
                            massDetail.Days = "Saturday";
                            church.SaturdayMassSchedule.Add(massDetail);
                            break;
                    }
                }
                church.ChurchSchedules = schedulesList;

                //// Church Reviews Processing...
                reader.NextResult();
                while (reader.Read())
                {
                    var review = _churchReviewTransformer.Transform(reader);

//                    review.User = _userTransformer.Transform(reader);

                    church.ChurchReviews.Add(review);
                }

                // Church Parking Processing...
                reader.NextResult();
                while (reader.Read())
                {
                    var parkings = reader["ParkingType"].ToString().Split(separator, StringSplitOptions.None);

                    foreach (var parking in parkings)
                        church.ChurchParking.Add(parking);
                }

                // Church Photos Processing...
                reader.NextResult();
                while (reader.Read())
                {
                    var photos = reader["ImagePath"].ToString().Split(separator, StringSplitOptions.None);

                    foreach (var photo in photos)
                        church.ChurchPhotos.Add(photo);
                }

                // Church Announcement Processing...
                reader.NextResult();
                while (reader.Read())
                    church.Announcements.Add(_announcementTransformer.Transform(reader));

                // Adoration Chapel Schedule Processing...
                reader.NextResult();
                while (reader.Read())
                {
                    church.AdorationDisplayText = Convert.IsDBNull(reader["DisplayText"].ToString()) ? "" : reader["DisplayText"].ToString();


                    bool isOpen24Hours;
                    try
                    {
                        isOpen24Hours = Convert.ToBoolean(reader["Open24Hours"]);
                    }
                    catch (Exception e)
                    {
                        isOpen24Hours = reader["Open24Hours"].ToString() != "No";
                    }

                    church.AdorationChapelSchedule.Add(new AdorationChapelSchedule
                    {
                        Day = reader["Days"].ToString(),
                        Time = reader["Time"].ToString(),
                        IsOpen24Hours = isOpen24Hours // this should be a valid boolean like 1 or 0 - by: Ronald Alba 08/01/2017
                    });
                }

                // Church Type Processing...
                reader.NextResult();
                while (reader.Read())
                    church.ChurchType = reader["ChurchType"].ToString();

                // Location Type Processing...
                reader.NextResult();
                while (reader.Read())
                    church.LocationType = reader["LocationType"].ToString();

                // Baptism Details Processing...
                reader.NextResult();
                while (reader.Read())
                    church.BaptismDetails = reader["Text"].ToString();

                // Confession Details Processing...
                reader.NextResult();
                while (reader.Read())
                    church.ConfessionDetails.Add(new ConfessionSchedule
                    {
                        Day = reader["ConfessDay"].ToString(),
                        Time = reader["ConfessTime"].ToString(),
                        Text = reader["ConfessText"].ToString()
                    });

                // Wedding Details Processing...
                reader.NextResult();
                while (reader.Read())
                    church.WeddingDetails = reader["Text"].ToString();

                // Country Details Processing...
                reader.NextResult();
                while (reader.Read())
                    church.Country = reader["Country"].ToString();

                // Adoration Ventilation Processing...
                reader.NextResult();
                while (reader.Read())
                    church.AdorationVentilations.Add(reader["VentType"].ToString());

                // Adoration Photos Processing...
                reader.NextResult();
                while (reader.Read())
                    church.AdorationPhotos.Add(@"Images\Photos\" + reader["ImagePath"]);
            }

            return church;
        }

        public List<Church> Search(string parish, string stateProvince, string churchTypeId,
            string churchLocationId,
            string hasAdorationChapel, string massScheduleId, string massTime, string massLanguage,
            string confessionTime, string confessionScheduleId, string ventilation, string parking,
            string countryId)
        {
            var churches = new List<Church>();

            using (var sp = new StoredProcedure("spSearchChurches"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@parish", parish);
                sp.SqlCommand.Parameters.AddWithValue("@stateOrProvince", stateProvince);
                sp.SqlCommand.Parameters.AddWithValue("@churchTypeID", churchTypeId);
                sp.SqlCommand.Parameters.AddWithValue("@churchLocationID", churchLocationId);
                sp.SqlCommand.Parameters.AddWithValue("@hasAdorationChapel", hasAdorationChapel);
                sp.SqlCommand.Parameters.AddWithValue("@massScheduleID", massScheduleId);
                sp.SqlCommand.Parameters.AddWithValue("@massTime", massTime);
                sp.SqlCommand.Parameters.AddWithValue("@massLanguage", massLanguage);
                sp.SqlCommand.Parameters.AddWithValue("@confessionScheduleID", confessionScheduleId);
                sp.SqlCommand.Parameters.AddWithValue("@confessionTime", confessionTime);
                sp.SqlCommand.Parameters.AddWithValue("@ventilation", ventilation);
                sp.SqlCommand.Parameters.AddWithValue("@parking", parking);
                sp.SqlCommand.Parameters.AddWithValue("@countries", countryId);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var church = _churchTransformer.Transform(reader);

                    // Ventilation Processing...
                    char[] separator = {','};
                    var ventilations = reader["Ventilations"]
                        .ToString()
                        .Split(separator, StringSplitOptions.None);
                    foreach (var ventilationItem in ventilations)
                        church.Ventilations.Add(ventilationItem);

                    // Mass Schedules Processing...
                    if (!Convert.IsDBNull(reader["MassSchedules"]))
                    {
                        var schedules = reader["MassSchedules"]
                            .ToString()
                            .Split(separator, StringSplitOptions.None);
                        foreach (var schedule in schedules)
                        {
                            char[] pipeSeparator = {'|'};
                            var massSchedule = schedule.Split(pipeSeparator, StringSplitOptions.None);

                            var massDetail =
                                new MassDetailsModel
                                {
                                    Id = massSchedule[0] == "" ? 0 : Convert.ToInt32(massSchedule[0]),
                                    Language = massSchedule[2],
                                    Time = massSchedule[3],
                                    TimeStandardId =
                                        massSchedule[4] == "" ? 0 : Convert.ToInt32(massSchedule[4]),
                                    DateCreated = massSchedule[5] == ""
                                        ? DateTime.Now
                                        : Convert.ToDateTime(massSchedule[5])
                                };

                            switch (Convert.ToInt32(massSchedule[1]))
                            {
                                case 1:
                                    massDetail.Days = "Sunday";
                                    massDetail.ScheduleId = 1;
                                    church.SundayMassSchedule.Add(massDetail);
                                    break;

                                case 2:
                                    massDetail.Days = "Monday";
                                    massDetail.ScheduleId = 2;
                                    church.MondayMassSchedule.Add(massDetail);
                                    break;

                                case 3:
                                    massDetail.Days = "Tuesday";
                                    massDetail.ScheduleId = 3;
                                    church.TuesdayMassSchedule.Add(massDetail);
                                    break;

                                case 4:
                                    massDetail.Days = "Wednesday";
                                    massDetail.ScheduleId = 4;
                                    church.WednesdayMassSchedule.Add(massDetail);
                                    break;

                                case 5:
                                    massDetail.Days = "Thursday";
                                    massDetail.ScheduleId = 5;
                                    church.ThursdayMassSchedule.Add(massDetail);
                                    break;

                                case 6:
                                    massDetail.Days = "Friday";
                                    massDetail.ScheduleId = 6;
                                    church.FridayMassSchedule.Add(massDetail);
                                    break;

                                case 7:
                                    massDetail.Days = "Saturday";
                                    massDetail.ScheduleId = 7;
                                    church.SaturdayMassSchedule.Add(massDetail);
                                    break;
                            }
                        }
                    }

                    switch (DateTime.Now.DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            church.MassSchedules = church.SundayMassSchedule;
                            break;
                        case DayOfWeek.Monday:
                            church.MassSchedules = church.MondayMassSchedule;
                            break;
                        case DayOfWeek.Tuesday:
                            church.MassSchedules = church.TuesdayMassSchedule;
                            break;
                        case DayOfWeek.Wednesday:
                            church.MassSchedules = church.WednesdayMassSchedule;
                            break;
                        case DayOfWeek.Thursday:
                            church.MassSchedules = church.ThursdayMassSchedule;
                            break;
                        case DayOfWeek.Friday:
                            church.MassSchedules = church.FridayMassSchedule;
                            break;
                        case DayOfWeek.Saturday:
                            church.MassSchedules = church.SaturdayMassSchedule;
                            break;
                    }


                    // Church Reviews Processing...
                    var reviews = reader["Reviews"].ToString().Split(separator, StringSplitOptions.None);
                    foreach (var review in reviews)
                        church.ChurchReviews.Add(new ChurchReview
                        {
                            Comment = review
                        });

                    // Church Photos Processing...
                    var photos = reader["ChurchPhotos"].ToString().Split(separator, StringSplitOptions.None);
                    foreach (var photo in photos)
                    {
                        church.ChurchThumbnails.Add(
                            photo == string.Empty ? "" : @"Images\Photos\Thumbnails\" + photo
                        );

                        church.ChurchPhotos.Add(
                            photo == string.Empty ? "" : @"Images\Photos\" + photo
                        );
                    }

                    // Baptism
                    church.BaptismDetails = reader["BaptismSched"].ToString();

                    // Confession
                    char[] splitter = {',', '|'};
                    var confession = reader["ConfessionSched"]
                        .ToString()
                        .Split(splitter, StringSplitOptions.None);

                    if (confession.Length > 1)
                    {
                        int i = 0, j = 1;
                        for (; i < confession.Length;)
                        {
                            church.ConfessionDetails.Add(new ConfessionSchedule
                            {
                                Day = confession[i],
                                Time = confession[j]
                            });
                            i = i + 2;
                            j = j + 2;
                        }
                    }

                    // Wedding
                    church.WeddingDetails = reader["WeddingSched"].ToString();

                    // Parking
                    var parkings = reader["ParkingSlot"].ToString().Split(separator, StringSplitOptions.None);
                    foreach (var parkingItem in parkings)
                        church.ChurchParking.Add(parkingItem);

                    churches.Add(church);
                }
            }

            return churches;
        }

        public List<Church> GetCoordinates(Coordinate coordinate)
        {
            var churches = new List<Church>();

            using (var sp = new StoredProcedure("spGetChurchesNearby"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@latitude", coordinate.Latitude);
                sp.SqlCommand.Parameters.AddWithValue("@longitude", coordinate.Longitude);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var church = _churchTransformer.Transform(reader);

                    // Ventilation Processing...
                    char[] separator = {','};

                    var ventilations = reader["Ventilations"]
                        .ToString()
                        .Split(separator, StringSplitOptions.None);
                    foreach (var ventilation in ventilations)
                        church.Ventilations.Add(ventilation);

                    // Mass Schedules Processing...
                    if (!Convert.IsDBNull(reader["MassSchedules"]))
                    {
                        var schedules = reader["MassSchedules"]
                            .ToString()
                            .Split(separator, StringSplitOptions.None);
                        foreach (var schedule in schedules)
                        {
                            char[] pipeSeparator = {'|'};
                            var massSchedule = schedule.Split(pipeSeparator, StringSplitOptions.None);

                            var massDetail =
                                new MassDetailsModel
                                {
                                    Id = massSchedule[0] == "" ? 0 : Convert.ToInt32(massSchedule[0]),
                                    Language = massSchedule[2],
                                    Time = massSchedule[3],
                                    TimeStandardId =
                                        massSchedule[4] == "" ? 0 : Convert.ToInt32(massSchedule[4]),
                                    DateCreated = massSchedule[5] == ""
                                        ? DateTime.Now
                                        : Convert.ToDateTime(massSchedule[5])
                                };

                            switch (Convert.ToInt32(massSchedule[1]))
                            {
                                case 1:
                                    massDetail.Days = "Sunday";
                                    massDetail.ScheduleId = 1;
                                    church.SundayMassSchedule.Add(massDetail);
                                    break;

                                case 2:
                                    massDetail.Days = "Monday";
                                    massDetail.ScheduleId = 2;
                                    church.MondayMassSchedule.Add(massDetail);
                                    break;

                                case 3:
                                    massDetail.Days = "Tuesday";
                                    massDetail.ScheduleId = 3;
                                    church.TuesdayMassSchedule.Add(massDetail);
                                    break;

                                case 4:
                                    massDetail.Days = "Wednesday";
                                    massDetail.ScheduleId = 4;
                                    church.WednesdayMassSchedule.Add(massDetail);
                                    break;

                                case 5:
                                    massDetail.Days = "Thursday";
                                    massDetail.ScheduleId = 5;
                                    church.ThursdayMassSchedule.Add(massDetail);
                                    break;

                                case 6:
                                    massDetail.Days = "Friday";
                                    massDetail.ScheduleId = 6;
                                    church.FridayMassSchedule.Add(massDetail);
                                    break;

                                case 7:
                                    massDetail.Days = "Saturday";
                                    massDetail.ScheduleId = 7;
                                    church.SaturdayMassSchedule.Add(massDetail);
                                    break;
                            }
                        }
                    }

                    switch (DateTime.Now.DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            church.MassSchedules = church.SundayMassSchedule;
                            break;
                        case DayOfWeek.Monday:
                            church.MassSchedules = church.MondayMassSchedule;
                            break;
                        case DayOfWeek.Tuesday:
                            church.MassSchedules = church.TuesdayMassSchedule;
                            break;
                        case DayOfWeek.Wednesday:
                            church.MassSchedules = church.WednesdayMassSchedule;
                            break;
                        case DayOfWeek.Thursday:
                            church.MassSchedules = church.ThursdayMassSchedule;
                            break;
                        case DayOfWeek.Friday:
                            church.MassSchedules = church.FridayMassSchedule;
                            break;
                        case DayOfWeek.Saturday:
                            church.MassSchedules = church.SaturdayMassSchedule;
                            break;
                    }


                    // Church Reviews Processing...
                    var reviews = reader["Reviews"].ToString().Split(separator, StringSplitOptions.None);
                    foreach (var review in reviews)
                        church.ChurchReviews.Add(new ChurchReview
                        {
                            Comment = review
                        });

                    // Church Photos Processing...
                    var photos = reader["ChurchPhotos"].ToString().Split(separator, StringSplitOptions.None);
                    foreach (var photo in photos)
                        church.ChurchPhotos.Add(
                            photo == string.Empty ? "" : @"Images\Photos\" + photo
                        );

                    // Baptism
                    church.BaptismDetails = reader["BaptismSched"].ToString();

                    // Confession
                    char[] splitter = {',', '|'};
                    var confession = reader["ConfessionSched"]
                        .ToString()
                        .Split(splitter, StringSplitOptions.None);

                    if (confession.Length > 1)
                    {
                        int i = 0, j = 1;
                        for (; i < confession.Length;)
                        {
                            church.ConfessionDetails.Add(new ConfessionSchedule
                            {
                                Day = confession[i],
                                Time = confession[j]
                            });
                            i = i + 2;
                            j = j + 2;
                        }
                    }

                    // Wedding
                    church.WeddingDetails = reader["WeddingSched"].ToString();

                    // Parking
                    var parkings = reader["ParkingSlot"].ToString().Split(separator, StringSplitOptions.None);
                    foreach (var parking in parkings)
                        church.ChurchParking.Add(parking);

                    churches.Add(church);
                }
            }

            return churches;
        }

        #region Private Properties

        private readonly ChurchTransformer _churchTransformer;
        private readonly MassDetailTransformer _massDetailTransformer;
        private readonly UserTransformer _userTransformer;
        private readonly ChurchReviewTransformer _churchReviewTransformer;
        private readonly AnnouncementTransformer _announcementTransformer;

        #endregion
    }
}