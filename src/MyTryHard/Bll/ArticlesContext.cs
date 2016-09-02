using MyTryHard.Helpers;
using MyTryHard.Models;
using System;
using System.Collections.Generic;

namespace MyTryHard.Bll
{
    /// <summary>
    /// Regroups each operations appliable to public articles.
    /// </summary>
    /// <seealso cref="Admin.AdminArticlesContext"/>
    public class ArticlesContext : BaseContext
    {
        public ArticlesContext(string connStr) : base(connStr)
        {
        }

        /// <summary>
        /// Gets the articles for the given category.
        /// </summary>
        /// <param name="catName">Category's name</param>
        /// <param name="startIndex">First article.</param>
        /// <param name="nbShow">Number of article to fetch.</param>
        /// <returns>Article list.</returns>
        public List<Article> GetCategoryArticles(string catUrl, int startIndex, int nbShow)
        {
            List<Article> lstArticles = new List<Article>();

            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pGetCategoryArticles";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.AddParameterWithValue("caturl_in", catUrl);
                cmd.AddParameterWithValue("nbstart_in", startIndex);
                cmd.AddParameterWithValue("count_in", nbShow);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Article article = new Article();
                    DatabaseHelper.FillModelFromReader(article, dr);
                    article.ShortContent = article.Content.GetEllipsis(
                        Math.Min(article.Content.Length - 1, 500),
                        false);

                    lstArticles.Add(article);
                }
            }

            return lstArticles;
        }

        /// <summary>
        /// Gets articles
        /// </summary>
        /// <param name="startIndex">First article</param>
        /// <param name="nbShow">Number article to show.</param>
        /// <returns>Articles list from #startIndex to #startIndex + nbShow</returns>
        public List<Article> GetArticles(int startIndex, int nbShow)
        {
            List<Article> lstArticles = new List<Article>();

            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pGetPublicArticles";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.AddParameterWithValue("nbstart_in", startIndex);
                cmd.AddParameterWithValue("count_in", nbShow);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Article article = new Article();
                    DatabaseHelper.FillModelFromReader<Article>(article, dr);

                    article.ShortContent = article.Content.GetEllipsis(
                        Math.Min(article.Content.Length - 1, 500),
                        false);

                    lstArticles.Add(article);
                }
            }

            return lstArticles;
        }

        /// <summary>
        /// Gets a single article
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>Chosen article</returns>
        public Article GetArticle(string articleTitle)
        {
            Article article = new Article();
            bool exists = false;

            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pGetPublicArticle";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.AddParameterWithValue("articletitle_in", articleTitle);

                var dr = cmd.ExecuteReader();

                exists = dr.Read();

                if (exists)
                    DatabaseHelper.FillModelFromReader<Article>(article, dr);

            }

            return article;
        }

        /// <summary>
        /// Saves the article in the database.
        /// TODO: move into admin ?
        /// </summary>
        /// <param name="article">Article to save.</param>
        public void SaveArticle(Article article)
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pAddSetArticle";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.AddParameterWithValue("userid_in",
                    //NpgsqlTypes.NpgsqlDbType.Varchar,
                    article.AuthorId.ToString());

                cmd.AddParameterWithValue("title_in",
                    //NpgsqlTypes.NpgsqlDbType.Varchar,
                    article.Title);

                cmd.AddParameterWithValue("articleid_in",
                    //NpgsqlTypes.NpgsqlDbType.Varchar,
                    article.ArticleId.ToString());

                cmd.AddParameterWithValue("lasteditiondate_in",
                    article.LastEditDate);

                cmd.AddParameterWithValue("publisheddate_in",
                    article.PublishedDate);

                cmd.AddParameterWithValue("seourl_in",
                    //NpgsqlTypes.NpgsqlDbType.Varchar,
                    article.SEOUrl);

                cmd.AddParameterWithValue("content_in",
                    article.Content);

                cmd.AddParameterWithValue("categoryid_in",
                    //NpgsqlTypes.NpgsqlDbType.Varchar,
                    article.CategoryId.ToString());

                cmd.AddParameterWithValue("iscommentallowed_in", article.IsCommentAllowed);
                cmd.AddParameterWithValue("isonline_in", article.PublishedDate < DateTime.MaxValue);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Gets articles count.
        /// </summary>
        /// <returns></returns>
        public int GetArticlesCount()
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(*) AS count FROM \"dbo\".\"Articles\" WHERE \"IsOnline\"=TRUE";

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                    return Convert.ToInt32(dr[0]);
                else
                    return 0;
            }
        }

        /// <summary>
        /// Gets the articles count for a given category.
        /// </summary>
        /// <param name="catid">Category's id.</param>
        /// <returns></returns>
        public int GetCategoryArticlesCount(string caturl)
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(*) AS count FROM \"dbo\".\"Articles\" \"ar\" " +
                    "JOIN \"dbo\".\"Categories\" \"c\" ON \"c\".\"CategoryID\" = \"ar\".\"CategoryID\" " +
                    "WHERE \"IsOnline\"=TRUE AND \"c\".\"SEOUrl\" = @caturl";
                cmd.AddParameterWithValue("caturl", caturl);

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                    return Convert.ToInt32(dr[0]);
                else
                    return 0;
            }
        }
    }
}
