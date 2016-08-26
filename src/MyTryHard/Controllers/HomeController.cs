using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyTryHard.ViewModels;
using MyTryHard.Models;
using MyTryHard.Bll;

namespace MyTryHard.Controllers
{
    public class HomeController : Controller
    {
        private static readonly int ARTICLES_PER_PAGE = 5;
        private ApplicationDbContext _ctx;

        public HomeController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p">Page number</param>
        /// <returns></returns>
        [ResponseCache(Duration = 5, NoStore = true)]
        public IActionResult Index(int page = 0)
        {
            ArticlesViewModel avm = new ArticlesViewModel();
            avm.Message = "Blog de MrTryHard";
            avm.Articles = new List<Article>();

            //avm.Articles = ArticlesCommand.GetArticles(page * ARTICLES_PER_PAGE, ARTICLES_PER_PAGE);
            avm.Articles = _ctx.Articles.GetArticles(page * ARTICLES_PER_PAGE, ARTICLES_PER_PAGE);

            avm.CurrentPage = page;
            avm.TotalPage = _ctx.Articles.GetArticlesCount() / ARTICLES_PER_PAGE;
            return View(avm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p">Page number</param>
        /// <returns></returns>
        [ResponseCache(Duration = 5, NoStore = true)]
        public IActionResult Articles(int page)
        {
            ArticlesViewModel avm = new ArticlesViewModel();
            avm.Articles = new List<Article>();
            avm.CurrentPage = page;
            avm.Articles = _ctx.Articles.GetArticles(page * ARTICLES_PER_PAGE, ARTICLES_PER_PAGE);
            avm.TotalPage = _ctx.Articles.GetArticlesCount() / ARTICLES_PER_PAGE;

            return View("Index", avm);
        }

        /// <summary>
        /// Find article by name.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [Route("home/article/{title}")]
        [ResponseCache(Duration = 5, NoStore = true)]
        public IActionResult Article(string title)
        {
            // Just check if it is okay.
            if (string.IsNullOrWhiteSpace(title))
                return RedirectToAction("index");

            Article article = _ctx.Articles.GetArticle(title);

            // If article has uninitialized Guid it means that it was unable to find the article.
            if (article.ArticleId == default(Guid))
                return RedirectToAction("index");

            return View(article);
        }

        [Route("home/category/{category}/{page?}")]
        [ResponseCache(Duration = 10, NoStore = true)]
        public IActionResult Category(string category, int page = 0)
        {
            if (string.IsNullOrWhiteSpace(category))
                return RedirectToAction("index");

            ArticlesViewModel avm = new ArticlesViewModel();

            avm.Articles = new List<Article>();
            avm.CurrentPage = page;
            avm.Articles = _ctx.Articles.GetCategoryArticles(category, page * ARTICLES_PER_PAGE, ARTICLES_PER_PAGE);
            avm.TotalPage = _ctx.Articles.GetCategoryArticlesCount(category) / ARTICLES_PER_PAGE;

            return View("Index", avm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 60)]
        public IActionResult Projects()
        {
            ProjectsViewModel pvm = new ProjectsViewModel();
            pvm.ProjectList = _ctx.Projects.GetProjectsList();
            return View(pvm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 60)]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
