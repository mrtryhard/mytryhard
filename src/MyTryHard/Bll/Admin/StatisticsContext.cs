using MyTryHard.Helpers;
using MyTryHard.Models;

namespace MyTryHard.Bll.Admin
{
    /// <summary>
    /// Regroups each statistical operations (mainly readings)
    /// </summary>
    public class StatisticsContext : BaseContext
    {
        public StatisticsContext(string connStr) : base(connStr)
        {
        }
        
        /// <summary>
        /// Gets a statistical overview.
        /// </summary>
        /// <returns>GeneralStats object.</returns>
        public GeneralStatsModel GetGeneralStats()
        {
            GeneralStatsModel model = new GeneralStatsModel();

            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pGetGeneralStats";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var dr = cmd.ExecuteReader();

                if (dr.Read())
                    DatabaseHelper.FillModelFromReader<GeneralStatsModel>(model, dr);
            }

            return model;
        }
    }
}
