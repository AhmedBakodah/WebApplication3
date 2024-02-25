using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Display(Name = "العنوان")]
        [Required(ErrorMessage = "{0} - حقل مطلوب")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "{0} يجب أن يكون بين {2} و {1}")]
        public string Title { get; set; } = default!;


        [Display(Name = "الوصف")]
        [Required(ErrorMessage = "{0} - حقل مطلوب")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "{0} يجب أن يكون بين {2} و {1}")]
        public string? Description { get; set; }


        [Display(Name = "الصورة")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "{0} يجب أن يكون بين {2} و {1}")]
        public string? ImageUrl { get; set; }


        [Display(Name = "عنوان الصورة")]
        [Required(ErrorMessage = "{0} - حقل مطلوب")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "{0} يجب أن يكون بين {2} و {1}")]
        public string? ImageCaption { get; set; }


        [Display(Name = "التايخ")]
        [Required(ErrorMessage = "{0} - حقل مطلوب")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }
}
