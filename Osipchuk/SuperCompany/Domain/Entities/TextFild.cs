using System.ComponentModel.DataAnnotations;

namespace SuperCompany.Domain.Entities
{
    public class TextFild: EntityBase
    {
        [Required]
       public string CodeWord { get; set; }
        [Display(Name ="Name")]
        public override string Title { get; set; } = "Information Page";

        [Display(Name = "Full Description")]
        public override string Text { get; set; } = "Аilled in by the administrator";
    }
}
