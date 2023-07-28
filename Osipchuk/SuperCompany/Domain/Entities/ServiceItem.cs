using System.ComponentModel.DataAnnotations;

namespace SuperCompany.Domain.Entities
{
    public class ServiceItem : EntityBase
    {
        [Required(ErrorMessage = "Fill name of Item")]
        [Display(Name = "Name of Item")]
        public override string Title { get; set; }

        [Display(Name = "Short Description")]
        public override string SubTitle { get; set; }

        [Display(Name = "Full Description")]
        public override string Text { get; set; }
    }
}
