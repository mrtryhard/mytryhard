using MyTryHard.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyTryHard.ViewModels
{
    public class CreateEditCategoryViewModel
    {
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
        public bool CanDelete { get; set; }

        [Display(Name = "Supprimer la catégorie ?")]
        public bool IsToDelete { get; set; }

        public bool IsDefault { get; set; }
    }
}
