using MyTryHard.Models;
using System.Collections.Generic;

namespace MyTryHard.ViewModels
{
    public class ArticlesViewModel
    {
        public string Message { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPage { get; set; }

        public List<Article> Articles { get; set; }
    }
}
