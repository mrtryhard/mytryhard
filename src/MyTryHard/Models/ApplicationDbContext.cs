using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTryHard.Bll;
using MyTryHard.Bll.Admin;

namespace MyTryHard.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private ArticlesContext _actx;
        private CategoriesContext _cctx;
        private AdminContext _admCtx;
        private ProjectsContext _projCtx;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public AdminContext Admin
        {
            get
            {
                if (_admCtx == null)
                    _admCtx = new AdminContext(Database.GetDbConnection().ConnectionString);

                return _admCtx;
            }
        }

        public ArticlesContext Articles
        {
            get
            {
                if (_actx == null)
                    _actx = new ArticlesContext(Database.GetDbConnection().ConnectionString);

                return _actx;
            }
        }

        public CategoriesContext Categories
        {
            get
            {
                if (_cctx == null)
                    _cctx = new CategoriesContext(Database.GetDbConnection().ConnectionString);

                return _cctx;
            }
        }

        public ProjectsContext Projects
        {
            get
            {
                if (_projCtx == null)
                    _projCtx = new ProjectsContext(Database.GetDbConnection().ConnectionString);

                return _projCtx;
            }
        }

        /// <summary>
        /// Administration part.
        /// </summary>
        public class AdminContext
        {
            private StatisticsContext _sctx;
            private AdminArticlesContext _admArtCtx;
            private string _connStr;

            public AdminContext(string connStr)
            {
                _connStr = connStr;
            }

            public StatisticsContext Statistics
            {
                get
                {
                    if (_sctx == null)
                        _sctx = new StatisticsContext(_connStr);

                    return _sctx;
                }
            }

            public AdminArticlesContext Articles
            {
                get
                {
                    if (_admArtCtx == null)
                        _admArtCtx = new AdminArticlesContext(_connStr);

                    return _admArtCtx;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbo");
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }
    }
}
