using MyTryHard.Helpers;
using MyTryHard.Models.Tracker;
using System;
using System.Collections.Generic;

namespace MyTryHard.Bll
{
    public class TrackerContext : BaseContext
    {
        public TrackerContext(string connStr) : base(connStr)
        {
        }

        public Dictionary<int, string> GetSportsList()
        {
            Dictionary<int, string> sports = new Dictionary<int, string>();

            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM \"dbo\".\"TrackedSports\" ORDER BY \"Title\"";
                cmd.CommandType = System.Data.CommandType.Text;

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    sports.Add((int)dr["TrackedSportID"], dr["Title"] as string);
                }
            }

            return sports;
        }

        public List<TrackerEntry> GetEntriesForUser(Guid userId)
        {
            List<TrackerEntry> lstEntries = new List<TrackerEntry>();

            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pGetTrackerEntriesForUser";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.AddParameterWithValue("userid_in", userId.ToString());
                cmd.AddParameterWithValue("sportid_in", -1);

                var dr = cmd.ExecuteReader();

                while(dr.Read())
                {
                    TrackerEntry te = new TrackerEntry();
                    DatabaseHelper.FillModelFromReader(te, dr);
                    lstEntries.Add(te);
                }
            }

            return lstEntries;
        }

        public List<TrackerEntry> GetEntriesForUserAndSport(Guid userId, int sportId)
        {
            List<TrackerEntry> lstEntries = new List<TrackerEntry>();

            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pGetTrackerEntriesForUser";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.AddParameterWithValue("userid_in", userId.ToString());
                cmd.AddParameterWithValue("sportid_in", sportId);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TrackerEntry te = new TrackerEntry();
                    DatabaseHelper.FillModelFromReader(te, dr);
                    lstEntries.Add(te);
                }
            }

            return lstEntries;
        }

        public void SaveEntryForUser(Guid userId, DateTime dtStart, DateTime dtEnd, int distance, int sportId)
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pSaveTrackerEntryForUser";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.AddParameterWithValue("userid_in", userId.ToString());
                cmd.AddParameterWithValue("distance_in", distance);
                cmd.AddParameterWithValue("datetimestart_in", dtStart);
                cmd.AddParameterWithValue("datetimeend_in", dtEnd);
                cmd.AddParameterWithValue("sportid_in", sportId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
