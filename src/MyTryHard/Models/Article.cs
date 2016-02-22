using MyTryHard.FiltersAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyTryHard.Models
{
    [AllowSqlAutoFill]
    public class Article
    {
        public Guid AuthorId { get; set; }

        public Guid ArticleId { get; set; }

        [Display(Name = "Catégorie")]
        public Guid CategoryId { get; set; }

        [Required]
        [Display(Name = "Titre de l'article")]
        [MinLength(10), MaxLength(255)]
        public string Title { get; set; }

        [Display(Name = "Catégorie")]
        public string CategoryTitle { get; set; }

        [Display(Name = "Auteur")]
        public string AuthorName { get; set; }

        [Display(Name = "Date de publication")]
        public DateTime PublishedDate { get; set; }

        [Display(Name = "Dernière édition")]
        public DateTime LastEditDate { get; set; }

        [Required]
        [Display(Name = "Contenu")]
        [MinLength(100)]
        public string Content { get; set; }

        [Display(Name = "Permettre les commentaires")]
        public bool IsCommentAllowed { get; set; }

        [Display(Name = "Url d'affichage")]
        public string SEOUrl { get; set; }

        /// <summary>
        /// Disallow autofill, we'll do that ourselves.
        /// </summary>
        [AllowSqlAutoFill(false)]
        public string ShortContent { get; set; }

        [AllowSqlAutoFill(false)]
        public short CommentsCount { get; set; }

        /// <summary>
        /// Determines if it is published or not.
        /// </summary>
        [Display(Name = "Publier maintenant")]
        public bool IsOnline { get; set; }
    }
}
