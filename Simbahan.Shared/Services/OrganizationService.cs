using System;
using System.Collections.Generic;
using System.Data;
using Simbahan.Database;
using Simbahan.Models;
using Simbahan.Transformers;

namespace Simbahan.Services
{
    public class OrganizationService
    {
        private bool HasColumn(IDataRecord dr, string columnName)
        {
            for (var i = 0; i < dr.FieldCount; i++)
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            return false;
        }

        public List<Organization> Search(string keyword, string location, string organizationName, string useSchedule,
            string time, string language, string day, string activities, string attendees, string ventilation,
            string parking, string venue)
        {
            var organizationTransformer = new OrganizationTransformer();
            var organizations = new List<Organization>();
            char[] commaSeparator = {','};
            char[] pipeSeparator = {'|'};

            using (var sp = new StoredProcedure("spSearchOrganizations"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@keyword", keyword);
                sp.SqlCommand.Parameters.AddWithValue("@location", location);
                sp.SqlCommand.Parameters.AddWithValue("@organizationName", organizationName);
                sp.SqlCommand.Parameters.AddWithValue("@schedule", useSchedule);
                sp.SqlCommand.Parameters.AddWithValue("@scheduleDay", day);
                sp.SqlCommand.Parameters.AddWithValue("@scheduleTime", time);
                sp.SqlCommand.Parameters.AddWithValue("@scheduleLanguage", language);
                sp.SqlCommand.Parameters.AddWithValue("@activities", activities);
                sp.SqlCommand.Parameters.AddWithValue("@attendees", attendees);
                sp.SqlCommand.Parameters.AddWithValue("@ventilation", ventilation);
                sp.SqlCommand.Parameters.AddWithValue("@parking", parking);
                sp.SqlCommand.Parameters.AddWithValue("@venue", venue);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var organization = organizationTransformer.Transform(reader);

                    //TODO: Organization Photos

                    if (!HasColumn(reader, "Schedules"))
                    {
                        organizations.Add(organization);
                        continue;
                    }

                    var schedules = reader["Schedules"].ToString().Split(commaSeparator, StringSplitOptions.None);

                    foreach (var scheduleItem in schedules)
                    {
                        var schedule = scheduleItem.Split(pipeSeparator, StringSplitOptions.None);
                        var araw = ""; // LOL
                        if (schedule.Length > 1)
                        {
                            switch (Convert.ToInt32(schedule[1]))
                            {
                                case 1:
                                    araw = "Sunday";
                                    break;
                                case 2:
                                    araw = "Monday";
                                    break;
                                case 3:
                                    araw = "Tuesday";
                                    break;
                                case 4:
                                    araw = "Wednesday";
                                    break;
                                case 5:
                                    araw = "Thursday";
                                    break;
                                case 6:
                                    araw = "Friday";
                                    break;
                                case 7:
                                    araw = "Saturday";
                                    break;
                            }
                        }

                        if (schedule.Length > 3)
                            organization.Masses.Add(new OrganizationMass
                            {
                                Id = Convert.ToInt32(schedule[0]),
                                ScheduleId = Convert.ToInt32(schedule[1]),
                                Time = schedule[2],
                                TimeStandardId = Convert.ToInt32(schedule[3]),
                                Day = araw
                            });
                    }

                    organizations.Add(organization);
                }
            }

            return organizations;
        }

        public Organization Find(int id)
        {
            var organizationTransformer = new OrganizationTransformer();
            var organization = new Organization();

            using (var sp = new StoredProcedure("spFindOrganization"))
            {
                sp.SqlCommand.Parameters.AddWithValue("@organizationID", id);

                var reader = sp.SqlCommand.ExecuteReader();

                while (reader.Read())
                    organization = organizationTransformer.Transform(reader);

                reader.NextResult();
                while (reader.Read())
                    organization.Locations.Add(reader["OrgLocation"].ToString());

                reader.NextResult();
                while (reader.Read())
                    organization.Ventilations.Add(reader["OrgVentilationType"].ToString());

                reader.NextResult();
                while (reader.Read())
                {
                    // TODO: Implement
                }

                reader.NextResult();
                while (reader.Read())
                    organization.Parkings.Add(reader["OrgVehicleParkingType"].ToString());

                reader.NextResult();
                while (reader.Read())
                    organization.Attendees.Add(reader["AttendeesType"].ToString());

                reader.NextResult();
                while (reader.Read())
                    organization.Activities.Add(reader["ActivityName"].ToString());

                reader.NextResult();
                while (reader.Read())
                    organization.Masses.Add(new OrganizationMass
                    {
                        Id = Convert.ToInt32(reader["OrgMassID"]),
                        ScheduleId = Convert.ToInt32(reader["ScheduleID"]),
                        TimeStandardId = Convert.ToInt32(reader["TimeStandardID"]),
                        OrganizationId = Convert.ToInt32(reader["OrganizationID"]),
                        Day = reader["Days"].ToString(),
                        Time = reader["Time"].ToString()
                    });

                reader.NextResult();
                while (reader.Read())
                    organization.BibleStudySchedules.Add(new OrganizationMass
                    {
                        Id = Convert.ToInt32(reader["OrgBibleScheduleID"]),
                        ScheduleId = Convert.ToInt32(reader["ScheduleID"]),
                        TimeStandardId = Convert.ToInt32(reader["TimeStandardID"]),
                        OrganizationId = Convert.ToInt32(reader["OrganizationID"]),
                        Day = reader["Days"].ToString(),
                        Time = reader["Time"].ToString()
                    });

                reader.NextResult();
                while (reader.Read())
                    organization.WorshipSchedules.Add(new OrganizationMass
                    {
                        Id = Convert.ToInt32(reader["OrgWorshipScheduleID"]),
                        ScheduleId = Convert.ToInt32(reader["ScheduleID"]),
                        TimeStandardId = Convert.ToInt32(reader["TimeStandardID"]),
                        OrganizationId = Convert.ToInt32(reader["OrganizationID"]),
                        Day = reader["Days"].ToString(),
                        Time = reader["Time"].ToString()
                    });
            }

            return organization;
        }
    }
}