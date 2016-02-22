using MyTryHard.FiltersAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyTryHard.Models
{
    [AllowSqlAutoFill]
    public class Category
    {
        public Guid CategoryID { get; set; }

        [Display(Name = "Catégorie parent")]
        public Guid ParentCategoryID { get; set; }

        [Display(Name = "Nom de la catégorie")]
        [Required]
        [MinLength(3), MaxLength(255)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [MaxLength(255)]
        public string Description { get; set; }

        [Display(Name = "Adresse SEO")]
        [Required]
        [MaxLength(45)]
        public string SEOUrl { get; set; }
    }
}
