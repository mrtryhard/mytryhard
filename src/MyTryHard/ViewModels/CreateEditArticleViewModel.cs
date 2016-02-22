using MyTryHard.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyTryHard.ViewModels
{
    public class CreateEditArticleViewModel
    {
        public Article Article { get; set; }
        public bool CanDelete { get; set; }
        public List<Category> Categories { get; set; }

        [Display(Name = "Supprimer l'article ?")]
        public bool IsToDelete { get; set; }
    }
}
