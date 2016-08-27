using MyTryHard.Helpers;
using MyTryHard.Models;
using MyTryHard.Models.Tracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTryHard.Bll
{
    public class TrackerContext : BaseContext
    {
        public TrackerContext(string connStr) : base(connStr)
        {
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
                cmd.AddParameterWithValue("sportid_in", null);

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

        public void SaveEntryForUser(Guid userId, TrackerEntry te)
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pSaveTrackerEntryForUser";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.AddParameterWithValue("userid_in", userId.ToString());
                cmd.AddParameterWithValue("distance_in", te.Distance);
                cmd.AddParameterWithValue("start_in", te.DateTimeStart);
                cmd.AddParameterWithValue("end_in", te.DateTimeEnd);
                cmd.AddParameterWithValue("sportid_in", te.SportId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
