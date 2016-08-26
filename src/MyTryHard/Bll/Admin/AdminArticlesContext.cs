using MyTryHard.Helpers;
using MyTryHard.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTryHard.Bll.Admin
{
    /// <summary>
    /// Regroups each articles operations available only to admins.
    /// </summary>
    /// <seealso cref="ArticlesContext"/>
    public class AdminArticlesContext : BaseContext
    {
        public AdminArticlesContext(string connStr) : base(connStr)
        {
        }

        /// <summary>
        /// Delete a single article instance.
        /// </summary>
        /// <param name="articleId">Article's id</param>
        public void DeleteArticle(Guid articleId)
        {
            DeleteArticles(new Guid[1] { articleId });
        }

        /// <summary>
        /// Deletes multiples articles
        /// </summary>
        /// <param name="articlesIds">Array of articles' ids</param>
        public void DeleteArticles(Guid[] articlesIds)
        {
            // Converti le tableau de Guid en tableau de string.
            string[] strIds = articlesIds.Select(id => id.ToString()).ToArray();

            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pDeleteArticlesFromIds";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.AddParameterWithValue("articlesids_in",
                    //NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Varchar,
                    strIds);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Obtains all articles, even those not published (drafts)
        /// </summary>
        /// <param name="start">First article #</param>
        /// <param name="count">Quantity to load.</param>
        /// <returns>List of articles from #start to #(start+count)</returns>
        public List<Article> GetArticles(int start, int count)
        {
            List<Article> lstArticles = new List<Article>();

            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pGetArticles";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.AddParameterWithValue("nbstart_in", start);
                cmd.AddParameterWithValue("count_in", count);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Article article = new Article();
                    DatabaseHelper.FillModelFromReader<Article>(article, dr);
                    lstArticles.Add(article);
                }
            }

            return lstArticles;
        }

        /// <summary>
        /// Gets every articles count, including drafts.
        /// </summary>
        /// <returns>Articles quantities.</returns>
        public int GetArticlesCount()
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(*) AS count FROM \"dbo\".\"Articles\"";
                var dr = cmd.ExecuteReader();

                if (dr.Read())
                    return Convert.ToInt32(dr[0]);
                else
                    return 0;
            }
        }

        /// <summary>
        /// Gets a specified article.
        /// </summary>
        /// <param name="articleId">Article's id</param>
        /// <returns>Article object.</returns>
        public Article GetArticle(Guid articleId)
        {
            Article article = new Article();
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pGetArticle";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.AddParameterWithValue("articleid_in",
                    //NpgsqlTypes.NpgsqlDbType.Varchar,
                    articleId.ToString());

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                    DatabaseHelper.FillModelFromReader<Article>(article, dr);
                else
                    return article;
            }

            return article;
        }

        /// <summary>
        /// Get draft articles.
        /// </summary>
        /// <param name="start">Start index</param>
        /// <param name="count">Number to load.</param>
        /// <returns>List of articles from #start to #(start + count)</returns>
        public List<Article> GetDraftArticles(int start, int count)
        {
            List<Article> lstArticles = new List<Article>();

            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pGetDraftArticles";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.AddParameterWithValue("nbstart_in", start);
                cmd.AddParameterWithValue("count_in", count);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Article article = new Article();
                    DatabaseHelper.FillModelFromReader<Article>(article, dr);
                    lstArticles.Add(article);
                }
            }

            return lstArticles;
        }
    }
}
