using Microsoft.AspNetCore.Mvc;
using MyTryHard.FiltersAttributes;
using MyTryHard.Models;
using MyTryHard.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace MyTryHard.Controllers
{
    [AdminOnly]
    public class AdminController : Controller
    {
        private ApplicationDbContext _ctx;

        public AdminController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// The query is quite heavy for the stats, so we put a 25 seconds duration
        /// on the cache for this specific action.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 25)]
        public IActionResult Index()
        {
            DashboardViewModel dvm = new DashboardViewModel();
            dvm.GeneralStats = _ctx.Admin.Statistics.GetGeneralStats();
            dvm.GeneralOptions = new GeneralOptionsModel();
            return View(dvm);
        }

        public IActionResult Articles()
        {
            return View();
        }

        public IActionResult Categories()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public IActionResult DeleteArticle(string id)
        {
            Guid articleId = new Guid();
            bool isGuid = Guid.TryParse(id, out articleId);

            if (!isGuid)
            {
                // Return to error here.
                return RedirectToAction("articles");
            }

            _ctx.Admin.Articles.DeleteArticle(articleId);

            return RedirectToAction("articles");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DeleteCategory(string id)
        {
            Guid categoryId = new Guid();
            bool isGuid = Guid.TryParse(id, out categoryId);

            if (isGuid)
                _ctx.Categories.DeleteCategory(categoryId);

            return RedirectToAction("categories");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveArticle(CreateEditArticleViewModel ceavm)
        {
            if (ceavm.IsToDelete)
                return DeleteArticle(ceavm.Article.ArticleId.ToString());

            Article article = ceavm.Article;
            article.ArticleId = article.ArticleId != Guid.Empty ? article.ArticleId : Guid.NewGuid();
            article.AuthorName = User.Identity.Name;
            article.AuthorId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            DateTime dtNow = DateTime.Now;
            article.PublishedDate = article.IsOnline ? dtNow : DateTime.MaxValue;
            article.LastEditDate = dtNow;

            // TODO: switch to ArticlesAdminCommand
            _ctx.Articles.SaveArticle(article);

            return RedirectToAction("articles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCategory(CreateEditCategoryViewModel cecvm)
        {
            if (cecvm.IsToDelete)
                return DeleteCategory(cecvm.Category.CategoryID.ToString());

            Category cat = cecvm.Category;

            if (cat.CategoryID == Guid.Empty && !cecvm.IsDefault)
                cat.CategoryID = Guid.NewGuid();

            _ctx.Categories.SaveCategory(cat);

            return RedirectToAction("categories");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult PanelListArticles()
        {
            List<Article> lstArticles = _ctx.Admin.Articles.GetArticles(0, 50);
            ArticlesViewModel avm = new ArticlesViewModel();
            avm.Articles = lstArticles;
            avm.CurrentPage = 0;
            avm.TotalPage = _ctx.Admin.Articles.GetArticlesCount();
            return PartialView("Admin/Articles/_articlesList", avm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult PanelDraftArticles()
        {
            List<Article> lstArticles = _ctx.Admin.Articles.GetDraftArticles(0, 50);
            ArticlesViewModel avm = new ArticlesViewModel();
            avm.Articles = lstArticles;
            avm.CurrentPage = 0;
            avm.TotalPage = 0;// ArticlesAdminCommand.GetArticlesCount();

            return PartialView("Admin/Articles/_articlesList", avm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult PanelListCategories()
        {
            CategoriesViewModel cvm = new CategoriesViewModel();
            cvm.Categories = _ctx.Categories.GetCategories();
            return PartialView("Admin/Categories/_categoriesList", cvm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult PanelCreateArticle()
        {
            CreateEditArticleViewModel ceavm = new CreateEditArticleViewModel();
            ceavm.Categories = _ctx.Categories.GetCategories();

            return PartialView("Admin/Articles/_articleCreateEdit", ceavm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult PanelCreateCategory()
        {
            CreateEditCategoryViewModel cecvm = new CreateEditCategoryViewModel();
            cecvm.Categories = _ctx.Categories.GetCategories();

            return PartialView("Admin/Categories/_categoryCreateEdit", cecvm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult PanelEditArticle(string param)
        {
            // TODO return a real error message.
            if (string.IsNullOrWhiteSpace(param))
                return PanelListArticles();

            Guid guid = Guid.Parse(param);
            Article article = _ctx.Admin.Articles.GetArticle(guid);

            CreateEditArticleViewModel ceavm = new CreateEditArticleViewModel();
            ceavm.Article = article;
            ceavm.CanDelete = true;
            ceavm.Categories = _ctx.Categories.GetCategories();

            return PartialView("Admin/Articles/_articleCreateEdit", ceavm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IActionResult PanelEditCategory(string param)
        {
            if (string.IsNullOrWhiteSpace(param))
                return PanelListCategories();

            Guid guid = Guid.Parse(param);
            Category cat = _ctx.Categories.GetCategory(guid);

            CreateEditCategoryViewModel cecvm = new CreateEditCategoryViewModel();
            cecvm.Category = cat;
            cecvm.CanDelete = cat.CategoryID != Guid.Empty;

            if (guid == Guid.Empty)
                cecvm.IsDefault = true;

            cecvm.Categories = _ctx.Categories.GetCategories();

            return PartialView("Admin/Categories/_categoryCreateEdit", cecvm);
        }

        public IActionResult Comments()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Planet()
        {
            return View();
        }
    }
}
