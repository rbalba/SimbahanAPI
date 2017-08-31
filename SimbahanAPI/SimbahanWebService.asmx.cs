using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using Simbahan.Models;
using Simbahan.Services;
using SimbahanAPI.Requests;

namespace SimbahanAPI
{
    /// <summary>
    ///     Summary description for SimbahanWebService
    /// </summary>
    [WebService(Namespace = "http://simbahan-api.azurewebsites.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class ChurchWebService : WebService
    {
        private readonly ChurchService churchService;
        private readonly AdorationService adorationService;
        private readonly OrganizationService organizationService;
        private readonly DailyGospelService dailyGospelService;
        private readonly DailyGospelReflectionService dailyGospelReflectionService;
        private readonly DailyReflectionService dailyReflectionService;
        private readonly DailyReflectionReflectionService dailyReflectionReflectionService;
        private readonly SaintService saintService;
        private readonly BasicCatholicPrayerService basicCatholicPrayerService;
        private readonly OtherCatholicPrayerService otherCatholicPrayerService;
        private readonly DevotionService devotionService;
        private readonly BibleVerseService bibleVerseService;
        private readonly ReligiousQuoteService religiousQuoteService;
        private readonly VisitaIglesiaService visitaIglesiaService;

        public ChurchWebService()
        {
            churchService = new ChurchService();
            adorationService = new AdorationService();
            organizationService = new OrganizationService();
            dailyGospelService = new DailyGospelService();
            dailyGospelReflectionService = new DailyGospelReflectionService();
            dailyReflectionService = new DailyReflectionService();
            dailyReflectionReflectionService = new DailyReflectionReflectionService();
            saintService = new SaintService();
            basicCatholicPrayerService = new BasicCatholicPrayerService();
            otherCatholicPrayerService = new OtherCatholicPrayerService();
            devotionService = new DevotionService();
            bibleVerseService = new BibleVerseService();
            religiousQuoteService = new ReligiousQuoteService();
            visitaIglesiaService = new VisitaIglesiaService();
        }

        // TODO: Create a new Simbahan and Update an existing Simbahan
        #region Simbahan

        #region GET


        [WebMethod(EnableSession = true, Description = "Basic church search.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<Church> SearchChurches(string keyword, string location)
        {
            var request = new SearchChurchesAdvanceRequest()
            {
                Keyword = keyword,
                Location = location
            };

            return SearchChurchesAdvance(request);
        }

        [WebMethod(EnableSession = true, Description = "Finds churches with advance filter.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<Church> SearchChurchesAdvance(SearchChurchesAdvanceRequest request)
        {
            var ventilationList = new List<string>();
            var parkingList = new List<string>();

            if (request.HasAirCon)
                ventilationList.Add("1");

            if (request.HasCeilingFan)
                ventilationList.Add("2");

            if (request.HasOrdinaryFan)
                ventilationList.Add("3");

            var ventilations = string.Join(",", ventilationList);

            if (request.HasPrivateParking)
                parkingList.Add("1");

            if (request.HasMallParking)
                parkingList.Add("2");

            if (request.HasStreetParking)
                parkingList.Add("3");

            var parkings = string.Join(",", parkingList);

            return churchService.Search(
                request.Keyword, request.Location, request.ChurchType,
                request.LocationId, "", request.MassDay,
                request.MassTime, request.MassLanguage, request.ConfessionTime,
                request.ConfessionDay, ventilations, parkings, ""
            );
        }

        [WebMethod(EnableSession = true, Description = "Gets all the data about a certain church.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public Church FindChurch(int id)
        {
            return churchService.Find(id);
        }


        #endregion


        #endregion

        // TODO: Create a new Adoration and Update an existing Adoration
        #region Adoration


        #region POST

        #endregion

        #region GET


        [WebMethod(EnableSession = true, Description = "Basic adoration search.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<Adoration> SearchAdorations(string keyword, string location)
        {
            var request = new SearchAdorationsAdvanceRequest()
            {
                Keyword = keyword,
                Location = location
            };

            return SearchAdorationsAdvance(request);
        }

        [WebMethod(EnableSession = true, Description = "Finds adorations with advance filter.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<Adoration> SearchAdorationsAdvance(SearchAdorationsAdvanceRequest request)
        {
            var ventilationsList = new List<string>();

            if (request.HasAircondition)
                ventilationsList.Add("1");

            if (request.HasElectricFan)
                ventilationsList.Add("3");

            var ventilations = string.Join(",", ventilationsList);

            var parkingList = new List<string>();

            if (request.HasPrivateParking)
                parkingList.Add("1");

            if (request.HasMallParking)
                parkingList.Add("2");

            if (request.HasStreetParking)
                parkingList.Add("3");

            var parkings = string.Join(",", parkingList);

            return adorationService.Search(request.Keyword, request.Location, request.Day, request.Time, request.Adorationlocation, ventilations, parkings);
        }

        [WebMethod(EnableSession = true, Description = "Finds adoration details")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public Adoration FindAdoration(int id)
        {
            return adorationService.Find(id);
        }


        #endregion


        #endregion

        // TODO: Create a new Organization and Update an existing Organization
        #region Organization


        #region POST

        #endregion

        #region GET


        [WebMethod(EnableSession = true, Description = "Basic organization search.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<Organization> SearchOrganizations(string keyword, string location)
        {
            var request = new SearchOrganizationsAdvanceRequest()
            {
                Keyword = keyword,
                Location = location
            };

            return SearchOrganizationsAdvance(request);
        }

        [WebMethod(EnableSession = true, Description = "Finds organizations with advance filter.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<Organization> SearchOrganizationsAdvance(SearchOrganizationsAdvanceRequest request)
        {
            var activityList = new List<string>();
            var attendeeList = new List<string>();
            var venueList = new List<string>();
            var ventList = new List<string>();
            var parkingList = new List<string>();

            if (request.ActivityWorship)
                activityList.Add("2");
            if (request.ActivityBibleStudy)
                activityList.Add("3");
            if (request.ActivityMass)
                activityList.Add("4");
            if (request.ActivityRetreats)
                activityList.Add("5");
            if (request.ActivityRecollection)
                activityList.Add("6");
            if (request.ActivityVolunteerWorks)
                activityList.Add("7");
            if (request.ActivityTalks)
                activityList.Add("8");
            if (request.ActivityCamp)
                activityList.Add("9");

            var activities = string.Join(",", activityList);

            if (request.AttendeeMen)
                attendeeList.Add("1");
            if (request.AttendeeSingle)
                attendeeList.Add("2");
            if (request.AttendeeProfessional)
                attendeeList.Add("3");
            if (request.AttendeeStudent)
                attendeeList.Add("4");
            if (request.AttendeeCouple)
                attendeeList.Add("5");
            if (request.AttendeeWomen)
                attendeeList.Add("6");
            if (request.AttendeeMissionary)
                attendeeList.Add("7");
            if (request.AttendeeNonCatholic)
                attendeeList.Add("8");

            var attendees = string.Join(",", attendeeList);

            if (request.VenueChurch)
                venueList.Add("1");
            if (request.VenueSchool)
                venueList.Add("2");
            if (request.VenueMall)
                venueList.Add("3");
            if (request.VenuePublic)
                venueList.Add("4");
            if (request.VenuePrivate)
                venueList.Add("5");

            var venues = string.Join(",", venueList);

            if (request.VentAircon)
                ventList.Add("1");
            if (request.VentCeilingFan)
                ventList.Add("2");
            if (request.VentWallFan)
                ventList.Add("3");
            if (request.VentStandFan)
                ventList.Add("4");

            var ventilations = string.Join(",", ventList);

            if (request.ParkingStreet)
                parkingList.Add("1");
            if (request.ParkingMall)
                parkingList.Add("2");
            if (request.ParkingPrivate)
                parkingList.Add("3");

            var parkings = string.Join(",", parkingList);

            return organizationService.Search(request.Keyword, request.Location, request.ParentOrganization,
                request.Schedule, request.Time, request.Language, request.Day, activities,
                attendees, ventilations, parkings, venues);
        }

        [WebMethod(EnableSession = true, Description = "Finds organization details")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public Organization FindOrganization(int id)
        {
            return organizationService.Find(id);
        }


        #endregion


        #endregion


        #region Daily Gospel


        #region POST

        #endregion

        #region GET


        [WebMethod(EnableSession = true, Description = "Get today's Gospel.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public DailyGospel GetDailyGospel()
        {
            return dailyGospelService.FindByDate(DateTime.Now);
        }

        [WebMethod(EnableSession = true, Description = "Find a Gospel by date.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public DailyGospel GetGospelByDate(DateTime date)
        {
            return dailyGospelService.FindByDate(date);
        }

        [WebMethod(EnableSession = true, Description = "Find a Gospel by id.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public DailyGospel FindGospel(int id)
        {
            return dailyGospelService.Find(id);
        }


        #endregion


        #endregion


        #region Daily Gospel Reflection


        #region POST

        #endregion

        #region GET


        [WebMethod(EnableSession = true, Description = "Get user's gospel reflection.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public DailyGospelReflection GetUserGospelReflection(int userId, int gospelId)
        {
            return dailyGospelReflectionService.GetUserReflection(userId, gospelId);
        }

        [WebMethod(EnableSession = true, Description = "Check if user has gospel reflection.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool UserHasGospelReflection(int userId, int gospelId)
        {
            return dailyGospelReflectionService.UserHasReflection(userId, gospelId);
        }


        #endregion


        #endregion


        #region Daily Reflection


        #region POST

        #endregion

        #region GET


        [WebMethod(EnableSession = true, Description = "Get today's Reflection")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public DailyReflection GetDailyReflection()
        {
            return dailyReflectionService.FindByDate(DateTime.Now);
        }

        [WebMethod(EnableSession = true, Description = "Find Reflection by date.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public DailyReflection GetReflectionByDate(DateTime date)
        {
            return dailyReflectionService.FindByDate(date);
        }

        [WebMethod(EnableSession = true, Description = "Find Reflection by id.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public DailyReflection FindReflection(int id)
        {
            return dailyReflectionService.Find(id);
        }

        #endregion


        #endregion


        #region Daily Gospel Reflection


        #region POST

        #endregion

        #region GET


        [WebMethod(EnableSession = true, Description = "Get user's reflection reflection.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public DailyReflectionReflection GetUserReflectionReflection(int userId, int gospelId)
        {
            return dailyReflectionReflectionService.GetUserReflection(userId, gospelId);
        }

        [WebMethod(EnableSession = true, Description = "Check if user has reflection reflection.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool UserHasReflectionReflection(int userId, int gospelId)
        {
            return dailyReflectionReflectionService.UserHasReflection(userId, gospelId);
        }


        #endregion


        #endregion


        #region Saint of the Day


        #region POST

        #endregion

        #region GET


        [WebMethod(EnableSession = true, Description = "Find saint by id.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public Saint FindSaint(int id)
        {
            return saintService.Find(id);
        }

        [WebMethod(EnableSession = true, Description = "Get a list of all saints.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<Saint> GetSaints()
        {
            return saintService.Get();
        }

        [WebMethod(EnableSession = true, Description = "Get a list of all patron.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<string> GetPatron()
        {
            return saintService.GetPatron();
        }


        #endregion


        #endregion


        #region Basic Catholic Prayer


        #region POST

        #endregion

        #region GET


        [WebMethod(EnableSession = true, Description = "Find basic catholic prayer.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public BasicCatholicPrayer FindBasicCatholicPrayer(int id)
        {
            return basicCatholicPrayerService.Find(id);
        }

        [WebMethod(EnableSession = true, Description = "Get basic catholic prayers.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<BasicCatholicPrayer> GetBasicCatholicPrayer()
        {
            return basicCatholicPrayerService.Get();
        }


        #endregion


        #endregion


        #region Other Catholic Prayer


        #region POST

        #endregion

        #region GET


        [WebMethod(EnableSession = true, Description = "Find Other Catholic Prayer by id.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public OtherCatholicPrayer FindOtherCatholicPrayer(int id)
        {
            return otherCatholicPrayerService.Find(id);
        }

        [WebMethod(EnableSession = true, Description = "Get Other Catholic Prayers.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<OtherCatholicPrayer> GetOtherCatholicPrayers()
        {
            return otherCatholicPrayerService.Get();
        }


        #endregion


        #endregion


        #region Devotion


        #region POST

        #endregion

        #region GET


        [WebMethod(EnableSession = true, Description = "Find Devotion by id.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public Devotion FindDevotion(int id)
        {
            return devotionService.Find(id);
        }

        [WebMethod(EnableSession = true, Description = "Get all devotions.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<Devotion> GetDevotions()
        {
            return devotionService.Get();
        }


        #endregion


        #endregion


        #region Bible Verse


        #region POST

        #endregion

        #region GET


        [WebMethod(EnableSession = true, Description = "Find Bible Verse by id.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public BibleVerse FindBibleVerse(int id)
        {
            return bibleVerseService.Find(id);
        }

        [WebMethod(EnableSession = true, Description = "Get Bible Verses by mood. userId is the authenticated user and is optional.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<BibleVerse> GetBibleVerse(Int32 mood, Int32 userId)
        {
            return bibleVerseService.Get(mood, userId);
        }


        #endregion


        #endregion


        #region Religious Quote


        #region POST

        #endregion

        #region GET


        [WebMethod(EnableSession = true, Description = "Find Religious Quote by id.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public ReligiousQuote FindReligiousQuote(int id)
        {
            return religiousQuoteService.Find(id);
        }

        [WebMethod(EnableSession = true, Description = "Get Religious Quotes by mood. userId is the authenticated user and is optional.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<ReligiousQuote> GetReligiousQuotes(int mood, int userId = 0)
        {
            return religiousQuoteService.Get(mood, userId);
        }


        #endregion


        #endregion


        #region Musical Inspiration


        #region POST

        #endregion

        #region GEt

        #endregion


        #endregion


        #region Visita Iglesia


        #region POST

        #endregion

        #region GET


        [WebMethod(EnableSession = true, Description = "Get user's Visita Iglesia progress.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public List<VisitaIglesia> GetVisitaIglesia(int userId)
        {
            return visitaIglesiaService.Get(userId);
        }

        [WebMethod(EnableSession = true, Description = "Get user's reflection reflection.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool HasExistingVisitaIglesia(int userId)
        {
            return visitaIglesiaService.HasExistingData(userId);
        }


        #endregion


        #endregion


        #region Legacy Code

        [WebMethod(EnableSession = true, Description = "Store church details into the database.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public Church Add_Simbahan_Details(Church Simbahan)
        {
            Church SimbahanDetails = new Church
            {
                StreetNumber = Simbahan.StreetNumber,
                StreetName = Simbahan.StreetName,
                Barangay = Simbahan.Barangay,
                City = Simbahan.City,
                StateProvince = Simbahan.StateProvince,
                ZipCode = Simbahan.ZipCode,
                Parish = Simbahan.Parish,
                Diocese = Simbahan.Diocese,
                Priest = Simbahan.Priest,
                Vicariate = Simbahan.Vicariate,
                DateCreated = DateTime.Now,
                LastUpdate = DateTime.Now,
                HasAdorationChapel = Simbahan.HasAdorationChapel,
                ChurchHistory = Simbahan.ChurchHistory,
                CompleteAddress = Simbahan.CompleteAddress,
                ChurchTypeId = Simbahan.ChurchTypeId,
                Latitude = Simbahan.Latitude,
                Longitude = Simbahan.Longitude,
                ContactNo = Simbahan.ContactNo,
                Website = Simbahan.Website,
                EmailAddress = Simbahan.EmailAddress,
                DateEstablished = Simbahan.DateEstablished,
                FeastDay = Simbahan.FeastDay,
                LocationId = Simbahan.LocationId,
                OfficeHours = Simbahan.OfficeHours,
                DevotionSchedule = Simbahan.DevotionSchedule,
                ChurchCode = Simbahan.ChurchCode
            };

            ////// INSERT DATA INTO TABLE ////// By: Ronald Alba
            using (var dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                dbconn.Open();

                using (var cmd = new SqlCommand("spInsertSimbahanDetails", dbconn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@streetNo", SimbahanDetails.StreetNumber);
                        cmd.Parameters.AddWithValue("@streetName", SimbahanDetails.StreetName);
                        cmd.Parameters.AddWithValue("@Barangay", SimbahanDetails.Barangay);
                        cmd.Parameters.AddWithValue("@City", SimbahanDetails.City);
                        cmd.Parameters.AddWithValue("@stateProvince", SimbahanDetails.StateProvince);
                        cmd.Parameters.AddWithValue("@zipCode", SimbahanDetails.ZipCode);
                        cmd.Parameters.AddWithValue("@Parish", SimbahanDetails.Parish);
                        cmd.Parameters.AddWithValue("@Diocese", SimbahanDetails.Diocese);
                        cmd.Parameters.AddWithValue("@parishPriest", SimbahanDetails.Priest);
                        cmd.Parameters.AddWithValue("@Vicariate", SimbahanDetails.Vicariate);
                        cmd.Parameters.AddWithValue("@dateCreated", SimbahanDetails.DateCreated);
                        cmd.Parameters.AddWithValue("@lastUpdate", SimbahanDetails.LastUpdate);
                        cmd.Parameters.AddWithValue("@hasAdorationChapel", SimbahanDetails.HasAdorationChapel);
                        cmd.Parameters.AddWithValue("@churchHistory", SimbahanDetails.ChurchHistory);
                        cmd.Parameters.AddWithValue("@churchTypeId", SimbahanDetails.ChurchTypeId);
                        cmd.Parameters.AddWithValue("@Latitude", SimbahanDetails.Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", SimbahanDetails.Longitude);
                        cmd.Parameters.AddWithValue("@contactNo", SimbahanDetails.ContactNo);
                        cmd.Parameters.AddWithValue("@webSite", SimbahanDetails.Website);
                        cmd.Parameters.AddWithValue("@emailAddress", SimbahanDetails.EmailAddress);
                        cmd.Parameters.AddWithValue("@dateEstablished", SimbahanDetails.DateEstablished);
                        cmd.Parameters.AddWithValue("@FeastDay", SimbahanDetails.FeastDay);
                        cmd.Parameters.AddWithValue("@locationId", SimbahanDetails.LocationId);
                        cmd.Parameters.AddWithValue("@officeHours", SimbahanDetails.OfficeHours);
                        cmd.Parameters.AddWithValue("@devotionSched", SimbahanDetails.DevotionSchedule);
                        cmd.Parameters.AddWithValue("@churchCode", SimbahanDetails.ChurchCode);
                        cmd.Parameters.Add("@SimbahanAutoID", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        //Get the last inserted ID number
                        SimbahanDetails.Id = Convert.ToInt32(cmd.Parameters["@SimbahanAutoID"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        dbconn.Close();
                        dbconn.Dispose();
                    }
                }
            }
            return SimbahanDetails;
        }

        [WebMethod(EnableSession = true, Description = "Store mass details into the database.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public MassDetailsModel Add_Mass_Details(MassDetailsModel massDetails)
        {
            MassDetailsModel MassDetails = new MassDetailsModel();
            MassDetails.SimbahanId = massDetails.SimbahanId;
            MassDetails.Language = massDetails.Language;
            MassDetails.ScheduleId = massDetails.ScheduleId;
            MassDetails.Time = massDetails.Time;
            MassDetails.TimeStandardId = massDetails.TimeStandardId;

            ////// INSERT DATA INTO TABLE ////// By: Ronald Alba
            using (var dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                dbconn.Open();

                using (var cmd = new SqlCommand("spInsertSimbahanMassDetails", dbconn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SimbahanID", massDetails.SimbahanId);
                        cmd.Parameters.AddWithValue("@Language", massDetails.Language);
                        cmd.Parameters.AddWithValue("@ScheduleId", massDetails.ScheduleId);
                        cmd.Parameters.AddWithValue("@Time", massDetails.Time);
                        cmd.Parameters.AddWithValue("@TimeStandardId", massDetails.TimeStandardId);
                        cmd.Parameters.Add("@SimbahanMassAutoID", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        MassDetails.Id = Convert.ToInt32(cmd.Parameters["@SimbahanMassAutoID"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        dbconn.Close();
                        dbconn.Dispose();
                    }
                }
            }
            return MassDetails;
        }


        [WebMethod(EnableSession = true, Description = "Store adoration chapel details into the database.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public clsAdorationModel Add_AdorationChapel_Details(int AdorationID, int ScheduleID, string Time, int TimeStandardID)
        {
            clsAdorationModel AdorationChapelDetails = new clsAdorationModel();
            AdorationChapelDetails.adorationId = AdorationID;
            AdorationChapelDetails.scheduleId = ScheduleID;
            AdorationChapelDetails.timeSched = Time;
            AdorationChapelDetails.timeStandardId = TimeStandardID;

            ////// INSERT DATA INTO TABLE ////// By: Ronald Alba
            using (var dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                dbconn.Open();

                using (var cmd = new SqlCommand("spInsertAdoChapelDetails", dbconn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@adorationId", AdorationChapelDetails.adorationId);
                        cmd.Parameters.AddWithValue("@scheduleId", AdorationChapelDetails.scheduleId);
                        cmd.Parameters.AddWithValue("@timeSchedule", AdorationChapelDetails.timeSched);
                        cmd.Parameters.AddWithValue("@timeStandardId", AdorationChapelDetails.timeStandardId);
                        cmd.Parameters.Add("@adoChapelAutoId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        AdorationChapelDetails.adChapelId = Convert.ToInt32(cmd.Parameters["@adoChapelAutoId"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        dbconn.Close();
                        dbconn.Dispose();
                    }
                }
            }
            return AdorationChapelDetails;
        }


        [WebMethod(EnableSession = true, Description = "Store user regitration details into the database.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public clsRegisterModel Add_UserRegistration_Details(clsRegisterModel RegisterUser)
        {
            clsRegisterModel register = new clsRegisterModel();
            register.firstName = RegisterUser.firstName;
            register.lastName = RegisterUser.lastName;
            register.birthDay = RegisterUser.birthDay;
            register.Gender = RegisterUser.Gender;
            register.emailAddress = RegisterUser.emailAddress;
            register.passWord = RegisterUser.passWord;

            using (SqlConnection dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                dbconn.Open();

                using (SqlCommand cmd = new SqlCommand("spInsertRegistrationDetails", dbconn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FirstName", register.firstName);
                        cmd.Parameters.AddWithValue("@LastName", register.lastName);
                        cmd.Parameters.AddWithValue("@Birthday", register.birthDay);
                        cmd.Parameters.AddWithValue("@Gender", register.Gender);
                        cmd.Parameters.AddWithValue("@EmailAddress", register.emailAddress);
                        cmd.Parameters.AddWithValue("@Password", register.passWord);
                        cmd.Parameters.Add("@RegAutoID", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        register.regId = Convert.ToInt32(cmd.Parameters["@RegAutoID"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        dbconn.Close();
                        dbconn.Dispose();
                    }
                }
            }
            return register;
        }

        [WebMethod(EnableSession = true, Description = "Store baptism information into database.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public clsBaptismModel Add_Baptism_Details(string Description, int SimbahanID)
        {
            clsBaptismModel Baptism = new clsBaptismModel();
            Baptism.Text = Description;
            Baptism.SimbahanId = SimbahanID;

            ////// INSERT DATA INTO TABLE ////// By: Ronald Alba
            using (SqlConnection dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                dbconn.Open();

                using (SqlCommand cmd = new SqlCommand("spInsertBaptismDetails", dbconn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Details", Baptism.Text);
                        cmd.Parameters.AddWithValue("@SimbahanID", Baptism.SimbahanId);
                        cmd.Parameters.Add("@BaptismAutoID", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        Baptism.BaptismId = Convert.ToInt32(cmd.Parameters["@BaptismAutoID"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        dbconn.Close();
                        dbconn.Dispose();
                    }
                }
            }
            return Baptism;
        }

        [WebMethod(EnableSession = true, Description = "Store wedding information into database.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public clsWeddingModel Add_Wedding_Details(string Description, int SimbahanID)
        {
            clsWeddingModel Wedding = new clsWeddingModel();
            Wedding.Text = Description;
            Wedding.SimbahanID = SimbahanID;

            ////// INSERT DATA INTO TABLE ////// By: Ronald Alba
            using (SqlConnection dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                dbconn.Open();

                using (var cmd = new SqlCommand("spInsertWeddingDetails", dbconn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Details", Wedding.Text);
                        cmd.Parameters.AddWithValue("@SimbahanID", Wedding.SimbahanID);
                        cmd.Parameters.Add("@WeddingAutoID", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        Wedding.WeddingID = Convert.ToInt32(cmd.Parameters["@WeddingAutoID"].Value.ToString());
                    }
                    catch(Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        dbconn.Close();
                        dbconn.Dispose();
                    }
                }
            }
            return Wedding;
        }

        [WebMethod(EnableSession = true, Description = "Store confession information into database.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public clsConfessionModel Add_Confession_Details(clsConfessionModel confess)
        {
            clsConfessionModel Confession = new clsConfessionModel();
            Confession.Time = confess.Time;
            Confession.SimbahanID = confess.SimbahanID;
            Confession.ScheduleID = confess.ScheduleID;
            Confession.Text = confess.Text;
            Confession.TimeStandardID = confess.TimeStandardID;

            ////// INSERT DATA INTO TABLE ////// By: Ronald Alba
            using (var dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                dbconn.Open();

                using (var cmd = new SqlCommand("spInsertConfessionDetails", dbconn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Time", Confession.Time);
                        cmd.Parameters.AddWithValue("@SimbahanID", Confession.SimbahanID);
                        cmd.Parameters.AddWithValue("@ScheduleID", Confession.ScheduleID);
                        cmd.Parameters.AddWithValue("@Text", Confession.Text);
                        cmd.Parameters.AddWithValue("@TimeStandardID", Confession.TimeStandardID);
                        cmd.Parameters.Add("@ConfessionAutoID", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        Confession.ConfessionID = Convert.ToInt32(cmd.Parameters["@ConfessionAutoID"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        dbconn.Close();
                        dbconn.Dispose();
                    }
                }
            }
            return Confession;
        }


        // DECRYPTION TYPE  : AES
        // Added By         : Ronald Alba
        [WebMethod(EnableSession = true, Description = "Encryptor Method.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string Encrypt(string Your_Text)
        {
            var EncryptionKey = "MAKV2SPBNI99212";
            var clearBytes = Encoding.Unicode.GetBytes(Your_Text);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    Your_Text = Convert.ToBase64String(ms.ToArray());
                }
            }
            return Your_Text;
        }

        // DECRYPTION TYPE  : AES
        // Added By         : Ronald Alba
        [WebMethod(EnableSession = true, Description = "Decryptor Method.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string Decrypt(string Your_CipherText)
        {
            var EncryptionKey = "MAKV2SPBNI99212";
            var cipherBytes = Convert.FromBase64String(Your_CipherText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    Your_CipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return Your_CipherText;
        }


        //[WebMethod(EnableSession = true, Description = "Get Daily Gospel.")]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public DailyGospelModel Get_Daily_Gospel(DateTime dateOfGospel)
        //{
        //    var dailyGospel = new DailyGospelModel();

        //    ////// INSERT DATA INTO TABLE ////// By: Ronald Alba
        //    using (var dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
        //    {
        //        if (dbconn.State == ConnectionState.Open)
        //            dbconn.Close();
        //        dbconn.Open();

        //        using (var cmd = new SqlCommand("spGetDailyGospel", dbconn))
        //        {
        //            try
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@dateofGospel", dateOfGospel);

        //                var reader = cmd.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    dailyGospel = new DailyGospelModel();

        //                    dailyGospel.Source = reader["Source"].ToString();
        //                    dailyGospel.DateOfGospel = Convert.ToDateTime(reader["DateOfGospel"].ToString());
        //                    dailyGospel.FirstReadingTitle = reader["FirstReadingTitle"].ToString();
        //                    dailyGospel.FirstReadingContent = reader["FirstReadingContent"].ToString();
        //                    dailyGospel.ResponsorialPsalmTitle = reader["ResponsorialPsalmTitle"].ToString();
        //                    dailyGospel.ResponsorialPsalmContent = reader["ResponsorialPsalmContent"].ToString();
        //                    dailyGospel.SecondReadingTitle = reader["SecondReadingTitle"].ToString();
        //                    dailyGospel.SecondReadingContent = reader["SecondReadingContent"].ToString();
        //                    dailyGospel.VerseBeforeGospelTitle = reader["VerseBeforeGospelTitle"].ToString();
        //                    dailyGospel.VerseBeforeGospelContent = reader["VerseBeforeGospelContent"].ToString();
        //                    dailyGospel.GospelTitle = reader["GospelTitle"].ToString();
        //                    dailyGospel.GospelContent = reader["GospelContent"].ToString();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //            finally
        //            {
        //                dbconn.Close();
        //                dbconn.Dispose();
        //            }
        //        }
        //    }
        //    return dailyGospel;
        //}

        //[WebMethod(EnableSession = true, Description = "Get Daily Reflection.")]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public DailyReflectionModel Get_Daily_Reflection(DateTime dateOfReflection)
        //{
        //    var dailyReflection = new DailyReflectionModel();

        //    ////// INSERT DATA INTO TABLE ////// By: Ronald Alba
        //    using (var dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
        //    {
        //        if (dbconn.State == ConnectionState.Open)
        //            dbconn.Close();
        //        dbconn.Open();

        //        using (var cmd = new SqlCommand("spGetDailyReflection", dbconn))
        //        {
        //            try
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@dateofReflection", dateOfReflection);

        //                var reader = cmd.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    dailyReflection = new DailyReflectionModel();

        //                    dailyReflection.Source = reader["Source"].ToString();
        //                    dailyReflection.DateOfReflection =
        //                        Convert.ToDateTime(reader["DateOfReflection"].ToString());
        //                    dailyReflection.FirstContentTitle = reader["FirstContentTitle"].ToString();
        //                    dailyReflection.FirstContent = reader["FirstContent"].ToString();
        //                    dailyReflection.SecondContentTitle = reader["SecondContentTitle"].ToString();
        //                    dailyReflection.SecondContent = reader["SecondContent"].ToString();
        //                    dailyReflection.ThirdContentTitle = reader["ThirdContentTitle"].ToString();
        //                    dailyReflection.ThirdContent = reader["ThirdContent"].ToString();
        //                    dailyReflection.FourthContentTitle = reader["FourthContentTitle"].ToString();
        //                    dailyReflection.FourthContent = reader["FourthContent"].ToString();
        //                    dailyReflection.FifthContentTitle = reader["FifthContentTitle"].ToString();
        //                    dailyReflection.FifthContent = reader["FifthContent"].ToString();
        //                    dailyReflection.SixthContentTitle = reader["SixthContentTitle"].ToString();
        //                    dailyReflection.SixthContent = reader["SixthContent"].ToString();
        //                    dailyReflection.Prayer = reader["Prayer"].ToString();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //            finally
        //            {
        //                dbconn.Close();
        //                dbconn.Dispose();
        //            }
        //        }
        //    }
        //    return dailyReflection;
        //}

        //[WebMethod(EnableSession = true, Description = "Get Religious Quotes By Mood.")]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public List<ReligiousQuotesModel> Get_Religious_Quotes(string Mood)
        //{
        //    var ReligiousQuote = new List<ReligiousQuotesModel>();

        //    using (var dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
        //    {
        //        if (dbconn.State == ConnectionState.Open)
        //            dbconn.Close();
        //        dbconn.Open();

        //        using (var cmd = new SqlCommand("spGetReligiousQuote", dbconn))
        //        {
        //            try
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@moods", Mood);

        //                var reader = cmd.ExecuteReader();

        //                while (reader.Read())
        //                    ReligiousQuote.Add(new ReligiousQuotesModel
        //                    {
        //                        ReligiousQuoteID = Convert.ToInt32(reader["ReligiousQuoteID"]),
        //                        DateOfQuote = Convert.ToDateTime(reader["DateOfQuote"]),
        //                        RQCode = reader["RQCode"].ToString(),
        //                        EmotionsReactions = reader["EmotionsReactions"].ToString(),
        //                        Author = reader["Author"].ToString(),
        //                        Quote = reader["Quote"].ToString(),
        //                        DisplayListForMobile = reader["DisplayListForMobile"].ToString()
        //                    });
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //            finally
        //            {
        //                dbconn.Close();
        //                dbconn.Dispose();
        //            }
        //        }
        //    }
        //    return ReligiousQuote;
        //}


        //[WebMethod(EnableSession = true, Description = "Get Daily Religious Quote By Date.")]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public ReligiousQuotesModel Get_Daily_Religious_Quote(DateTime DateOfQuote)
        //{
        //    var ReligiousQuote = new ReligiousQuotesModel();

        //    using (var dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
        //    {
        //        if (dbconn.State == ConnectionState.Open)
        //            dbconn.Close();
        //        dbconn.Open();

        //        using (var cmd = new SqlCommand("spGetDailyReligiousQuote", dbconn))
        //        {
        //            try
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@dateofQuote", DateOfQuote);

        //                var reader = cmd.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    ReligiousQuote.ReligiousQuoteID = Convert.ToInt32(reader["ReligiousQuoteID"]);
        //                    ReligiousQuote.DateOfQuote = Convert.ToDateTime(reader["DateOfQuote"]);
        //                    ReligiousQuote.RQCode = reader["RQCode"].ToString();
        //                    ReligiousQuote.EmotionsReactions = reader["EmotionsReactions"].ToString();
        //                    ReligiousQuote.Author = reader["Author"].ToString();
        //                    ReligiousQuote.Quote = reader["Quote"].ToString();
        //                    ReligiousQuote.DisplayListForMobile = reader["DisplayListForMobile"].ToString();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //            finally
        //            {
        //                dbconn.Close();
        //                dbconn.Dispose();
        //            }
        //        }
        //    }
        //    return ReligiousQuote;
        //}

        //[WebMethod(EnableSession = true, Description = "Get Nearby Churches (For Visita Iglesia).")]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public List<ChurchInfo> GetNearByChurches(double Longitude, double Latitude)
        //{
        //    var SimbahanInfo = new ChurchInfo();
        //    var ListOfChurches = new List<ChurchInfo>();
        //    var Church_Reviews = new List<ChurchReview>();
        //    var Vent = new List<VentilationModel>();
        //    var Sched = new List<ChurchScheduleModel>();
        //    var UserReview = new List<ChurchReview>();
        //    var Photos = new List<ChurchPhotosModel>();
        //    var Confess = new List<ConfessionSchedule>();
        //    var Church_Parking = new List<ParkingSlot>();

        //    using (var dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
        //    {
        //        if (dbconn.State == ConnectionState.Open)
        //            dbconn.Close();
        //        dbconn.Open();

        //        using (var cmd = new SqlCommand("spGetChurchesNearby", dbconn))
        //        {
        //            try
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@latitude", Latitude);
        //                cmd.Parameters.AddWithValue("@longitude", Longitude);

        //                var dr = cmd.ExecuteReader();

        //                while (dr.Read())
        //                {
        //                    SimbahanInfo = new ChurchInfo();
        //                    Church_Reviews = new List<ChurchReview>();
        //                    Vent = new List<VentilationModel>();
        //                    Sched = new List<ChurchScheduleModel>();
        //                    UserReview = new List<ChurchReview>();
        //                    Photos = new List<ChurchPhotosModel>();
        //                    Confess = new List<ConfessionSchedule>();
        //                    Church_Parking = new List<ParkingSlot>();

        //                    SimbahanInfo.SimbahanID = Convert.ToInt32(dr["SimbahanID"]);
        //                    SimbahanInfo.StreetNo = Convert.IsDBNull(dr["StreetNo"])
        //                        ? null
        //                        : (int?) Convert.ToInt32(dr["StreetNo"]);
        //                    SimbahanInfo.StreetName = dr["StreetName"].ToString();
        //                    SimbahanInfo.Barangay = dr["Barangay"].ToString();
        //                    SimbahanInfo.City = dr["City"].ToString();
        //                    SimbahanInfo.StateProvince = dr["StateOrProvince"].ToString();
        //                    SimbahanInfo.ZipCode = dr["ZipCode"].ToString();
        //                    SimbahanInfo.Parish = dr["Parish"].ToString();
        //                    SimbahanInfo.Diocese = dr["Diocese"].ToString();
        //                    SimbahanInfo.Priest = dr["ParishPriest"].ToString();
        //                    SimbahanInfo.Vicariate = dr["Vicariate"].ToString();
        //                    SimbahanInfo.DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString());
        //                    SimbahanInfo.LastUpdate = Convert.ToDateTime(dr["LastUpdate"].ToString());
        //                    SimbahanInfo.HasAdorationChapel = Convert.ToBoolean(dr["HasAdorationChapel"]);
        //                    SimbahanInfo.ChurchHistory = dr["ChurchHistory"].ToString();
        //                    SimbahanInfo.CompleteAddress = dr["CompleteAddress"].ToString();
        //                    SimbahanInfo.ChurchTypeID = Convert.IsDBNull(dr["ChurchTypeID"])
        //                        ? null
        //                        : (int?) Convert.ToInt32(dr["ChurchTypeID"]);
        //                    SimbahanInfo.Longitude = Convert.ToDouble(dr["Longitude"]);
        //                    SimbahanInfo.Latitude = Convert.ToDouble(dr["Latitude"]);
        //                    SimbahanInfo.ContactNo = dr["ContactNo"].ToString();
        //                    SimbahanInfo.Website = dr["Website"].ToString();
        //                    SimbahanInfo.EmailAddress = dr["EmailAddress"].ToString();
        //                    SimbahanInfo.DateEstablished = dr["DateEstablished"].ToString();
        //                    SimbahanInfo.FeastDay = dr["FeastDay"].ToString();
        //                    SimbahanInfo.LocationID = Convert.IsDBNull(dr["LocationID"])
        //                        ? null
        //                        : (int?) Convert.ToInt32(dr["LocationID"]);
        //                    SimbahanInfo.OfficeHours = dr["OfficeHours"].ToString();
        //                    SimbahanInfo.DevotionSchedule = dr["DevotionSchedule"].ToString();
        //                    SimbahanInfo.ChurchCode = dr["ChurchCode"].ToString();

        //                    // Ventilation Processing...
        //                    char[] separator = {','};
        //                    var ventilation = dr["Ventilations"].ToString().Split(separator, StringSplitOptions.None);
        //                    foreach (var strVent in ventilation)
        //                        Vent.Add(new VentilationModel {VentType = strVent});
        //                    SimbahanInfo.Ventilations = Vent;

        //                    // Mass Schedules Processing...
        //                    if (!Convert.IsDBNull(dr["MassSchedules"]))
        //                    {
        //                        var schedules = dr["MassSchedules"]
        //                            .ToString()
        //                            .Split(separator, StringSplitOptions.None);
        //                        foreach (var schedule in schedules)
        //                        {
        //                            char[] pipeSeparator = {'|'};
        //                            var massSchedule = schedule.Split(pipeSeparator, StringSplitOptions.None);

        //                            var massDetail = new MassDetailsModel();
        //                            massDetail.MassDetailID =
        //                                massSchedule[0] == "" ? 0 : Convert.ToInt32(massSchedule[0]);
        //                            massDetail.Language = massSchedule[2];
        //                            massDetail.Time = massSchedule[3];
        //                            massDetail.TimeStandardID =
        //                                massSchedule[4] == "" ? 0 : Convert.ToInt32(massSchedule[4]);
        //                            massDetail.DateCreated =
        //                                massSchedule[5] == "" ? DateTime.Now : Convert.ToDateTime(massSchedule[5]);

        //                            switch (Convert.ToInt32(massSchedule[1]))
        //                            {
        //                                case 1:
        //                                    massDetail.Days = "Sunday";
        //                                    massDetail.ScheduleID = 1;
        //                                    SimbahanInfo.SundayMassSchedule.Add(massDetail);
        //                                    break;

        //                                case 2:
        //                                    massDetail.Days = "Monday";
        //                                    massDetail.ScheduleID = 2;
        //                                    SimbahanInfo.MondayMassSchedule.Add(massDetail);
        //                                    break;

        //                                case 3:
        //                                    massDetail.Days = "Tuesday";
        //                                    massDetail.ScheduleID = 3;
        //                                    SimbahanInfo.TuesdayMassSchedule.Add(massDetail);
        //                                    break;

        //                                case 4:
        //                                    massDetail.Days = "Wednesday";
        //                                    massDetail.ScheduleID = 4;
        //                                    SimbahanInfo.WednesdayMassSchedule.Add(massDetail);
        //                                    break;

        //                                case 5:
        //                                    massDetail.Days = "Thursday";
        //                                    massDetail.ScheduleID = 5;
        //                                    SimbahanInfo.ThursdayMassSchedule.Add(massDetail);
        //                                    break;

        //                                case 6:
        //                                    massDetail.Days = "Friday";
        //                                    massDetail.ScheduleID = 6;
        //                                    SimbahanInfo.FridayMassSchedule.Add(massDetail);
        //                                    break;

        //                                case 7:
        //                                    massDetail.Days = "Saturday";
        //                                    massDetail.ScheduleID = 7;
        //                                    SimbahanInfo.SaturdayMassSchedule.Add(massDetail);
        //                                    break;
        //                            }
        //                        }
        //                    }

        //                    switch (DateTime.Now.DayOfWeek)
        //                    {
        //                        case DayOfWeek.Sunday:
        //                            SimbahanInfo.MassSchedules = SimbahanInfo.SundayMassSchedule;
        //                            break;
        //                        case DayOfWeek.Monday:
        //                            SimbahanInfo.MassSchedules = SimbahanInfo.MondayMassSchedule;
        //                            break;
        //                        case DayOfWeek.Tuesday:
        //                            SimbahanInfo.MassSchedules = SimbahanInfo.TuesdayMassSchedule;
        //                            break;
        //                        case DayOfWeek.Wednesday:
        //                            SimbahanInfo.MassSchedules = SimbahanInfo.WednesdayMassSchedule;
        //                            break;
        //                        case DayOfWeek.Thursday:
        //                            SimbahanInfo.MassSchedules = SimbahanInfo.ThursdayMassSchedule;
        //                            break;
        //                        case DayOfWeek.Friday:
        //                            SimbahanInfo.MassSchedules = SimbahanInfo.FridayMassSchedule;
        //                            break;
        //                        case DayOfWeek.Saturday:
        //                            SimbahanInfo.MassSchedules = SimbahanInfo.SaturdayMassSchedule;
        //                            break;
        //                    }


        //                    // Church Reviews Processing...
        //                    var review = dr["Reviews"].ToString().Split(separator, StringSplitOptions.None);
        //                    foreach (var strRev in review)
        //                        UserReview.Add(new ChurchReview
        //                        {
        //                            Comments = strRev
        //                        });
        //                    SimbahanInfo.ChurchReviews = UserReview;

        //                    // Church Photos Processing...
        //                    var photos = dr["ChurchPhotos"].ToString().Split(separator, StringSplitOptions.None);
        //                    foreach (var strPix in photos)
        //                        Photos.Add(new ChurchPhotosModel
        //                        {
        //                            ChurchPhotos = strPix == string.Empty
        //                                ? ""
        //                                : "http://www.mycatholicportal.org/images/photos/" + strPix
        //                        });
        //                    SimbahanInfo.ChurchPhotos = Photos;

        //                    // Baptism
        //                    SimbahanInfo.BaptismDetails = dr["BaptismSched"].ToString();

        //                    // Confession
        //                    char[] splitter = {',', '|'};
        //                    var confession = dr["ConfessionSched"].ToString().Split(splitter, StringSplitOptions.None);

        //                    if (confession.Length > 1)
        //                    {
        //                        int i = 0, j = 1;
        //                        for (; i < confession.Length;)
        //                        {
        //                            Confess.Add(new ConfessionSchedule
        //                            {
        //                                Confess_Day = confession[i],
        //                                Confess_Time = confession[j]
        //                            });
        //                            i = i + 2;
        //                            j = j + 2;
        //                        }
        //                    }
        //                    SimbahanInfo.ConfessionDetails = Confess;

        //                    // Wedding
        //                    SimbahanInfo.WeddingDetails = dr["WeddingSched"].ToString();

        //                    // Parking
        //                    var parking = dr["ParkingSlot"].ToString().Split(separator, StringSplitOptions.None);
        //                    foreach (var strPark in parking)
        //                        Church_Parking.Add(new ParkingSlot {ParkingType = strPark});
        //                    SimbahanInfo.ChurchParking = Church_Parking;

        //                    ListOfChurches.Add(SimbahanInfo);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //            finally
        //            {
        //                dbconn.Close();
        //                dbconn.Dispose();
        //            }
        //        }
        //    }

        //    return ListOfChurches;
        //}

        //[WebMethod(EnableSession = true, Description = "Search Adoration Chapel.")]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public List<Adoration> Search_Adoration_Chapel(string Keyword, string Location, string Day, string Time,
        //    string ChapelLocation, string Ventilation)
        //{
        //    var adorations = new List<Adoration>();

        //    using (var dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
        //    {
        //        if (dbconn.State == ConnectionState.Open)
        //            dbconn.Close();
        //        dbconn.Open();

        //        using (var cmd = new SqlCommand("spSearchAdorations", dbconn))
        //        {
        //            try
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@keyword", Keyword);
        //                cmd.Parameters.AddWithValue("@location", Location);
        //                cmd.Parameters.AddWithValue("@scheduleTime", Time);
        //                cmd.Parameters.AddWithValue("@scheduleDay", Day);
        //                cmd.Parameters.AddWithValue("@chapelLocation", ChapelLocation);
        //                cmd.Parameters.AddWithValue("@ventilation", Ventilation);

        //                var dr = cmd.ExecuteReader();

        //                while (dr.Read())
        //                {
        //                    var adoration = new Adoration();

        //                    adoration.AdorationID = Convert.ToInt32(dr["AdorationID"]);
        //                    adoration.SimbahanID = Convert.ToInt32(dr["SimbahanID"]);
        //                    adoration.Is247 = Convert.ToBoolean(dr["IsOpen24By7"]);

        //                    char[] separator = {','};
        //                    char[] pipeSeparator = {'|'};

        //                    var ventilations = dr["Ventilations"].ToString().Split(separator, StringSplitOptions.None);

        //                    foreach (var ventilation in ventilations)
        //                        adoration.Ventilations.Add(ventilation);

        //                    var photos = dr["AdorationPhotos"].ToString().Split(separator, StringSplitOptions.None);
        //                    foreach (var strPix in photos)
        //                        adoration.Images.Add(strPix == string.Empty ? "" : @"Images\Photos\" + strPix);

        //                    var masses = dr["Masses"].ToString().Split(separator, StringSplitOptions.None);

        //                    foreach (var massDB in masses)
        //                    {
        //                        var massDetail = massDB.Split(pipeSeparator, StringSplitOptions.None);

        //                        var mass = new MassDetailsModel();
        //                        mass.ScheduleID = Convert.ToInt32(massDetail[2]);
        //                        mass.Time = massDetail[3];
        //                        mass.TimeStandardID = Convert.ToInt32(massDetail[4]);

        //                        switch (Convert.ToInt32(massDetail[2]))
        //                        {
        //                            case 1:
        //                                mass.Days = "Sunday";
        //                                adoration.SundaySchedule.Add(mass);
        //                                break;
        //                            case 2:
        //                                mass.Days = "Monday";
        //                                adoration.MondaySchedule.Add(mass);
        //                                break;
        //                            case 3:
        //                                mass.Days = "Tuesday";
        //                                adoration.TuesdaySchedule.Add(mass);
        //                                break;
        //                            case 4:
        //                                mass.Days = "Wednesday";
        //                                adoration.WednesdaySchedule.Add(mass);
        //                                break;
        //                            case 5:
        //                                mass.Days = "Thursday";
        //                                adoration.ThursdaySchedule.Add(mass);
        //                                break;
        //                            case 6:
        //                                mass.Days = "Friday";
        //                                adoration.FridaySchedule.Add(mass);
        //                                break;
        //                            case 7:
        //                                mass.Days = "Saturday";
        //                                adoration.SaturdaySchedule.Add(mass);
        //                                break;
        //                        }
        //                    }

        //                    adoration.Church = new ChurchInfo();

        //                    adoration.Church.SimbahanID = Convert.ToInt32(dr["SimbahanID"]);
        //                    adoration.Church.StreetNo = Convert.IsDBNull(dr["StreetNo"])
        //                        ? null
        //                        : (int?) Convert.ToInt32(dr["StreetNo"]);
        //                    adoration.Church.StreetName = dr["StreetName"].ToString();
        //                    adoration.Church.Barangay = dr["Barangay"].ToString();
        //                    adoration.Church.City = dr["City"].ToString();
        //                    adoration.Church.StateProvince = dr["StateOrProvince"].ToString();
        //                    adoration.Church.ZipCode = dr["ZipCode"].ToString();
        //                    adoration.Church.Parish = dr["Parish"].ToString();
        //                    adoration.Church.Diocese = dr["Diocese"].ToString();
        //                    adoration.Church.Priest = dr["ParishPriest"].ToString();
        //                    adoration.Church.Vicariate = dr["Vicariate"].ToString();
        //                    adoration.Church.DateCreated = Convert.ToDateTime(dr["DateCreated"].ToString());
        //                    adoration.Church.LastUpdate = Convert.ToDateTime(dr["LastUpdate"].ToString());
        //                    adoration.Church.HasAdorationChapel =
        //                        Convert.ToBoolean(dr["HasAdorationChapel"].ToString());
        //                    adoration.Church.ChurchHistory = dr["ChurchHistory"].ToString();
        //                    adoration.Church.CompleteAddress = dr["CompleteAddress"].ToString();
        //                    adoration.Church.ChurchTypeID = Convert.IsDBNull(dr["ChurchTypeID"])
        //                        ? 0
        //                        : Convert.ToInt32(dr["ChurchTypeID"]);
        //                    adoration.Church.Longitude = Convert.ToDouble(dr["Longitude"].ToString());
        //                    adoration.Church.Latitude = Convert.ToDouble(dr["Latitude"].ToString());
        //                    adoration.Church.ContactNo = dr["ContactNo"].ToString();
        //                    adoration.Church.Website = dr["Website"].ToString();
        //                    adoration.Church.EmailAddress = dr["EmailAddress"].ToString();
        //                    adoration.Church.DateEstablished = dr["DateEstablished"].ToString();
        //                    adoration.Church.FeastDay = dr["FeastDay"].ToString();
        //                    adoration.Church.LocationID = Convert.IsDBNull(dr["LocationID"])
        //                        ? 0
        //                        : Convert.ToInt32(dr["LocationID"]);
        //                    adoration.Church.OfficeHours = dr["OfficeHours"].ToString();
        //                    adoration.Church.DevotionSchedule = dr["DevotionSchedule"].ToString();

        //                    adorations.Add(adoration);
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                // Ignored
        //            }
        //        }
        //    }

        //    return adorations;
        //}

        [WebMethod(EnableSession = true, Description = "Store Catholic Organization information into database.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public clsCatholicOrganization Add_CatholicOrganization_Details(clsCatholicOrganization CatholicOrg)
        {
            clsCatholicOrganization catholicOrg = new clsCatholicOrganization();
            catholicOrg.OrgName = CatholicOrg.OrgName;
            catholicOrg.LastUpdate = CatholicOrg.LastUpdate;
            catholicOrg.CompleteAddress = CatholicOrg.CompleteAddress;
            catholicOrg.StreetNo = CatholicOrg.StreetNo;
            catholicOrg.StreetName = CatholicOrg.StreetName;
            catholicOrg.Barangay = CatholicOrg.Barangay;
            catholicOrg.CityOrMunicipality = CatholicOrg.CityOrMunicipality;
            catholicOrg.StateOrProvince = CatholicOrg.StateOrProvince;
            catholicOrg.Country = CatholicOrg.Country;
            catholicOrg.DateEstablished = CatholicOrg.DateEstablished;
            catholicOrg.ParentOrganization = CatholicOrg.ParentOrganization;
            catholicOrg.FeastBuilderOrPreacher = CatholicOrg.FeastBuilderOrPreacher;
            catholicOrg.BranchOrLocation = CatholicOrg.BranchOrLocation;
            catholicOrg.ContactNo = CatholicOrg.ContactNo;
            catholicOrg.EmailAddress = CatholicOrg.EmailAddress;
            catholicOrg.Website = CatholicOrg.Website;
            catholicOrg.OrgLocID = CatholicOrg.OrgLocID;
            catholicOrg.RetreatSchedule = CatholicOrg.RetreatSchedule;
            catholicOrg.RecollectSchedule = CatholicOrg.RecollectSchedule;
            catholicOrg.TalkSchedule = CatholicOrg.TalkSchedule;
            catholicOrg.CampSchedule = CatholicOrg.CampSchedule;
            catholicOrg.VolunteerSchedule = CatholicOrg.VolunteerSchedule;
            catholicOrg.Latitude = CatholicOrg.Latitude;
            catholicOrg.Longitude = CatholicOrg.Longitude;
            catholicOrg.About = CatholicOrg.About;

            using (SqlConnection dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                dbconn.Open();

                using (SqlCommand cmd = new SqlCommand("spInsertCatholicOrganization", dbconn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OrgName", catholicOrg.OrgName);
                        cmd.Parameters.AddWithValue("@LastUpdate", catholicOrg.LastUpdate);
                        cmd.Parameters.AddWithValue("@CompleteAddress", catholicOrg.CompleteAddress);
                        cmd.Parameters.AddWithValue("@StreetNo", catholicOrg.StreetNo);
                        cmd.Parameters.AddWithValue("@StreetName", catholicOrg.StreetName);
                        cmd.Parameters.AddWithValue("@Barangay", catholicOrg.Barangay);
                        cmd.Parameters.AddWithValue("@CityOrMunicipality", catholicOrg.CityOrMunicipality);
                        cmd.Parameters.AddWithValue("@StateOrProvince", catholicOrg.StateOrProvince);
                        cmd.Parameters.AddWithValue("@Country", catholicOrg.Country);
                        cmd.Parameters.AddWithValue("@DateEstablished", catholicOrg.DateEstablished);
                        cmd.Parameters.AddWithValue("@ParentOrganization", catholicOrg.ParentOrganization);
                        cmd.Parameters.AddWithValue("@FeastBuilderOrPreacher", catholicOrg.FeastBuilderOrPreacher);
                        cmd.Parameters.AddWithValue("@BranchOrLocation", catholicOrg.BranchOrLocation);
                        cmd.Parameters.AddWithValue("@ContactNo", catholicOrg.ContactNo);
                        cmd.Parameters.AddWithValue("@EmailAddress", catholicOrg.EmailAddress);
                        cmd.Parameters.AddWithValue("@Website", catholicOrg.Website);
                        cmd.Parameters.AddWithValue("@OrgLocationID", catholicOrg.OrgLocID);
                        cmd.Parameters.AddWithValue("@RetreatSchedule", catholicOrg.RetreatSchedule);
                        cmd.Parameters.AddWithValue("@RecollectSchedule", catholicOrg.RecollectSchedule);
                        cmd.Parameters.AddWithValue("@TalkSchedule", catholicOrg.TalkSchedule);
                        cmd.Parameters.AddWithValue("@CampSchedule", catholicOrg.CampSchedule);
                        cmd.Parameters.AddWithValue("@VolunteerSchedule", catholicOrg.VolunteerSchedule);
                        cmd.Parameters.AddWithValue("@Latitude", catholicOrg.Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", catholicOrg.Longitude);
                        cmd.Parameters.AddWithValue("@About", catholicOrg.About);
                        cmd.Parameters.Add("@CatholicOrgAutoId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        catholicOrg.OrgID = Convert.ToInt32(cmd.Parameters["@CatholicOrgAutoId"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        dbconn.Close();
                        dbconn.Dispose();
                    }
                }
            }
            return catholicOrg;
        }

        [WebMethod(EnableSession = true, Description = "Store announcement information into database.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public Announcement Add_Announcement_Details(Announcement Announcement)
        {
            Announcement announce = new Announcement();
            announce.Title = Announcement.Title;
            announce.Description = Announcement.Description;
            announce.StartDate = Announcement.StartDate;
            announce.StartTime = Announcement.StartTime;
            announce.EndDate = Announcement.EndDate;
            announce.EndTime = Announcement.EndTime;
            announce.Venue = Announcement.Venue;
            announce.SimbahanId = Announcement.SimbahanId;
            announce.ImagePath = Announcement.ImagePath;

            using (SqlConnection dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                dbconn.Open();

                using (SqlCommand cmd = new SqlCommand("spInsertAnnouncement", dbconn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@title", announce.Title);
                        cmd.Parameters.AddWithValue("@description", announce.Description);
                        cmd.Parameters.AddWithValue("@startDate", announce.StartDate);
                        cmd.Parameters.AddWithValue("@startTime", announce.StartTime);
                        cmd.Parameters.AddWithValue("@endDate", announce.EndDate);
                        cmd.Parameters.AddWithValue("@endTime", announce.EndTime);
                        cmd.Parameters.AddWithValue("@address", announce.Venue);
                        cmd.Parameters.AddWithValue("@simbahanID", announce.SimbahanId);
                        cmd.Parameters.AddWithValue("@imagePath", announce.ImagePath);
                        cmd.Parameters.Add("@announceAutoId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        announce.Id = Convert.ToInt32(cmd.Parameters["@announceAutoId"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        dbconn.Close();
                        dbconn.Dispose();
                    }
                }
            }
            return announce;
        }


        [WebMethod(EnableSession = true, Description = "Store Catholic Organization Announcement information into database.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public clsCatholicOrgAnnounceModel Add_CatholicOrg_Announcement_Details(clsCatholicOrgAnnounceModel announcement)
        {
            clsCatholicOrgAnnounceModel catholicOrgAnnounce = new clsCatholicOrgAnnounceModel();
            catholicOrgAnnounce.OrganizationId = announcement.AnnouncementId;
            catholicOrgAnnounce.TitleContent = announcement.TitleContent;
            catholicOrgAnnounce.AnnouncementDesc = announcement.AnnouncementDesc;
            catholicOrgAnnounce.Address = announcement.Address;
            catholicOrgAnnounce.ImagePath = announcement.ImagePath;
            catholicOrgAnnounce.StartDate = announcement.StartDate;
            catholicOrgAnnounce.StartTime = announcement.StartTime;
            catholicOrgAnnounce.EndDate = announcement.EndDate;
            catholicOrgAnnounce.EndTime = announcement.EndTime;

            using (SqlConnection dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                dbconn.Open();

                using (SqlCommand cmd = new SqlCommand("spInsertCatholicOrgAnnouncement", dbconn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@orgID", catholicOrgAnnounce.TitleContent);
                        cmd.Parameters.AddWithValue("@titleContent", catholicOrgAnnounce.TitleContent);
                        cmd.Parameters.AddWithValue("@announceDesc", catholicOrgAnnounce.TitleContent);
                        cmd.Parameters.AddWithValue("@address", catholicOrgAnnounce.TitleContent);
                        cmd.Parameters.AddWithValue("@imagePath", catholicOrgAnnounce.TitleContent);
                        cmd.Parameters.AddWithValue("@startDate", catholicOrgAnnounce.TitleContent);
                        cmd.Parameters.AddWithValue("@startTime", catholicOrgAnnounce.TitleContent);
                        cmd.Parameters.AddWithValue("@endDate", catholicOrgAnnounce.TitleContent);
                        cmd.Parameters.AddWithValue("@endTime", catholicOrgAnnounce.TitleContent);

                        cmd.Parameters.Add("@catholicOrgAnnounceAutoId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        catholicOrgAnnounce.AnnouncementId = Convert.ToInt32(cmd.Parameters["@catholicOrgAnnounceAutoId"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        dbconn.Close();
                        dbconn.Dispose();
                    }
                }
            }
            return catholicOrgAnnounce;
        }

        [WebMethod(EnableSession = true, Description = "Store Bible Schedules information into database.")]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public clsBibleSchedulesModel Add_BibleSchedules_Details(clsBibleSchedulesModel BibleSchedule)
        {
            clsBibleSchedulesModel bibleSched = new clsBibleSchedulesModel();
            bibleSched.OrgnizationId = BibleSchedule.OrgnizationId;
            bibleSched.ScheduleId = BibleSchedule.ScheduleId;
            bibleSched.TimeStandardId = BibleSchedule.TimeStandardId;
            bibleSched.Time = BibleSchedule.Time;

            using (SqlConnection dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                dbconn.Open();

                using (SqlCommand cmd = new SqlCommand("spInsertBibleSchedules", dbconn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OrgnizationId", BibleSchedule.OrgnizationId);
                        cmd.Parameters.AddWithValue("@ScheduleId", BibleSchedule.ScheduleId);
                        cmd.Parameters.AddWithValue("@TimeStandardId", BibleSchedule.TimeStandardId);
                        cmd.Parameters.AddWithValue("@Time", BibleSchedule.Time);
                        cmd.Parameters.Add("@BibleSchedAutoId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        bibleSched.OrgBibleSchedId = Convert.ToInt32(cmd.Parameters["@BibleSchedAutoId"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        dbconn.Close();
                        dbconn.Dispose();
                    }
                }
            }
            return bibleSched;
        }


        //[WebMethod(EnableSession = true, Description = "Store...")]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public void Add_Template_Here()
        //{


        //    using (SqlConnection dbconn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString))
        //    {
        //        if (dbconn.State == ConnectionState.Open)
        //        {
        //            dbconn.Close();
        //        }
        //        dbconn.Open();

        //        using (SqlCommand cmd = new SqlCommand("spInsert", dbconn))
        //        {
        //            try
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                //cmd.Parameters.AddWithValue("@OrgnizationId", BibleSchedule.OrgnizationId);
                        
        //                //cmd.Parameters.Add("@BibleSchedAutoId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
        //                cmd.ExecuteNonQuery();

        //                //bibleSched.OrgBibleSchedId = Convert.ToInt32(cmd.Parameters["@BibleSchedAutoId"].Value.ToString());
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //            finally
        //            {
        //                dbconn.Close();
        //                dbconn.Dispose();
        //            }
        //        }
        //    }
        //    return;
        //}
        #endregion
    }
}