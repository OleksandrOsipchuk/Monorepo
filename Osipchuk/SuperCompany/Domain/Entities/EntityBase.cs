using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SuperCompany.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase() =>DateAdded = DateTime.UtcNow; 
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public virtual string Title { get; set; }

        [Display(Name = "Short Description")]
        public virtual string SubTitle { get; set; } = "";
        [Display(Name = "Full Description")]
        public virtual string Text { get; set; } = "";
        [Display(Name = "Title Image")]
        public virtual string TitleImagePath { get; set; } = "";
        [Display(Name = "SEO MetaTitle")]
        public string MetaTitle { get; set; } = "";
        [Display(Name = "SEO MetaDescription")]
        public string MetaDescription { get; set; } = "";
        [Display(Name = "SEO MetaKeyWotds")]
        public string MetaKeyWords { get; set; } = "";
        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }
    }
}
