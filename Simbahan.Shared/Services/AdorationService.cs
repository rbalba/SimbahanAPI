using System;
using System.Collections.Generic;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class AdorationService
    {
        public Adoration Find(int id)
        {
            var adoration = new Adoration();
            var churchTransformer = new ChurchTransformer();

            using (var sp = new StoredProcedure("spFindAdoration"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@adorationID", id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    adoration.Church = churchTransformer.Transform(reader);
                    adoration.Is247 = Convert.ToBoolean(reader["IsOpen24By7"]);
                    adoration.ChurchId = Convert.ToInt32(reader["SimbahanID"]);
                    adoration.AdorationId = Convert.ToInt32(reader["AdorationID"]);
                    adoration.DisplayText = reader["DisplayText"].ToString();
                }

                // Adoration Schedule
                reader.NextResult();
                while (reader.Read())
                {
                    var mass = new MassDetailsModel()
                    {
                        Id = Convert.ToInt32(reader["AdorationID"]),
                        ScheduleId = Convert.ToInt32(reader["ScheduleID"]),
                        TimeStandardId = Convert.ToInt32(reader["TimeStandardID"]),
                        Time = reader["Time"].ToString()
                    };

                    switch (Convert.ToInt32(reader["ScheduleID"]))
                    {
                        case 1:
                            mass.Days = "Sunday";
                            adoration.SundaySchedule.Add(mass);
                            break;
                        case 2:
                            mass.Days = "Monday";
                            adoration.MondaySchedule.Add(mass);
                            break;
                        case 3:
                            mass.Days = "Tuesday";
                            adoration.TuesdaySchedule.Add(mass);
                            break;
                        case 4:
                            mass.Days = "Wednesday";
                            adoration.WednesdaySchedule.Add(mass);
                            break;
                        case 5:
                            mass.Days = "Thursday";
                            adoration.ThursdaySchedule.Add(mass);
                            break;
                        case 6:
                            mass.Days = "Friday";
                            adoration.FridaySchedule.Add(mass);
                            break;
                        case 7:
                            mass.Days = "Saturday";
                            adoration.SaturdaySchedule.Add(mass);
                            break;
                    }

                    adoration.Masses.Add(mass);
                }

                reader.NextResult();
                while (reader.Read())
                {
                    adoration.Ventilations.Add(reader["VentType"].ToString());
                }

                reader.NextResult();
                while (reader.Read())
                {
                    adoration.Images.Add("Images/Photos/" + reader["ImagePath"]);
                }
            }

            return adoration;
        }

        public List<Adoration> Search(string keyword, string location, string day, string time, string chapelLocation,
            string ventilation, string parkings)
        {
            var adorations = new List<Adoration>();
            var churchTransformer = new ChurchTransformer();

            using (var sp = new StoredProcedure("spSearchAdorations"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@keyword", keyword);
                sp.SqlCommand.Parameters.AddWithValue("@location", location);
                sp.SqlCommand.Parameters.AddWithValue("@scheduleTime", time);
                sp.SqlCommand.Parameters.AddWithValue("@scheduleDay", day);
                sp.SqlCommand.Parameters.AddWithValue("@chapelLocation", chapelLocation);
                sp.SqlCommand.Parameters.AddWithValue("@ventilation", ventilation);
                sp.SqlCommand.Parameters.AddWithValue("@parkings", parkings);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var adoration = new Adoration
                    {
                        AdorationId = Convert.ToInt32(reader["AdorationID"]),
                        ChurchId = Convert.ToInt32(reader["SimbahanId"]),
                        Is247 = Convert.ToBoolean(reader["IsOpen24By7"]),
                        DisplayText = reader["DisplayText"].ToString()
                    };

                    char[] separator = { ',' };
                    char[] pipeSeparator = { '|' };

                    var ventilations = reader["Ventilations"].ToString().Split(separator, StringSplitOptions.None);

                    foreach (var ventilationItem in ventilations)
                        adoration.Ventilations.Add(ventilationItem);

                    var photos = reader["AdorationPhotos"].ToString().Split(separator, StringSplitOptions.None);
                    foreach (var photo in photos)
                        adoration.Images.Add(photo == string.Empty ? "" : @"Images\Photos\" + photo);

                    var masses = reader["Masses"].ToString().Split(separator, StringSplitOptions.None);

                    foreach (var massDb in masses)
                    {
                        var massDetail = massDb.Split(pipeSeparator, StringSplitOptions.None);

                        var mass = new MassDetailsModel
                        {
                            ScheduleId = Convert.ToInt32(massDetail[2]),
                            Time = massDetail[3],
                            TimeStandardId = Convert.ToInt32(massDetail[4])
                        };

                        switch (Convert.ToInt32(massDetail[2]))
                        {
                            case 1:
                                mass.Days = "Sunday";
                                adoration.SundaySchedule.Add(mass);
                                break;
                            case 2:
                                mass.Days = "Monday";
                                adoration.MondaySchedule.Add(mass);
                                break;
                            case 3:
                                mass.Days = "Tuesday";
                                adoration.TuesdaySchedule.Add(mass);
                                break;
                            case 4:
                                mass.Days = "Wednesday";
                                adoration.WednesdaySchedule.Add(mass);
                                break;
                            case 5:
                                mass.Days = "Thursday";
                                adoration.ThursdaySchedule.Add(mass);
                                break;
                            case 6:
                                mass.Days = "Friday";
                                adoration.FridaySchedule.Add(mass);
                                break;
                            case 7:
                                mass.Days = "Saturday";
                                adoration.SaturdaySchedule.Add(mass);
                                break;
                        }
                    }

                    adoration.Church = churchTransformer.Transform(reader);

                    adorations.Add(adoration);
                }
            }

            return adorations;
        }
    }
}