using MyTryHard.Helpers;
using MyTryHard.Models;
using System;
using System.Collections.Generic;

namespace MyTryHard.Bll
{
    /// <summary>
    /// Regroups all categories operations
    /// </summary>
    public class CategoriesContext : BaseContext
    {
        public CategoriesContext(string connStr) : base(connStr)
        {
        }

        /// <summary>
        /// Gets every categories.
        /// </summary>
        /// <returns>List of categories</returns>
        public List<Category> GetCategories()
        {
            List<Category> lstCategories = new List<Category>();

            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM \"dbo\".\"Categories\"";
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Category cat = new Category();
                    DatabaseHelper.FillModelFromReader<Category>(cat, dr);
                    lstCategories.Add(cat);
                }
            }

            return lstCategories;
        }

        /// <summary>
        /// Gets a single category.
        /// </summary>
        /// <param name="id">Category's id</param>
        /// <returns>Category object</returns>
        public Category GetCategory(Guid id)
        {
            Category cat = new Category();

            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM \"dbo\".\"Categories\" WHERE \"CategoryID\" = @catId";
                cmd.AddParameterWithValue("@catId", id.ToString());

                var dr = cmd.ExecuteReader();

                if (dr.Read())
                    DatabaseHelper.FillModelFromReader<Category>(cat, dr);
            }

            return cat;
        }

        /// <summary>
        /// Saves category.
        /// </summary>
        /// <param name="category">Category to save</param>
        public void SaveCategory(Category category)
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pAddSetCategory";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.AddParameterWithValue("categoryid_in",
                    //NpgsqlTypes.NpgsqlDbType.Varchar,
                    category.CategoryID.ToString());

                cmd.AddParameterWithValue("parentcategoryid_in",
                    //NpgsqlTypes.NpgsqlDbType.Varchar,
                    category.ParentCategoryID.ToString());

                cmd.AddParameterWithValue("title_in",
                    //NpgsqlTypes.NpgsqlDbType.Varchar,
                    category.Title);

                cmd.AddParameterWithValue("description_in",
                    //NpgsqlTypes.NpgsqlDbType.Varchar,
                    category.Description);

                cmd.AddParameterWithValue("seourl_in",
                    //NpgsqlTypes.NpgsqlDbType.Varchar,
                    category.SEOUrl);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes specified category.
        /// </summary>
        /// <param name="catId">Category's id.</param>
        public void DeleteCategory(Guid catId)
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "dbo.pDeleteCategoryFromId";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.AddParameterWithValue("categoryid_in",
                    // NpgsqlTypes.NpgsqlDbType.Varchar,
                    catId.ToString());

                cmd.ExecuteNonQuery();
            }
        }
    }
}
