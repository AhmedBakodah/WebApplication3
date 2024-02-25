using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Display(Name = "الاسم")]
        [Required(ErrorMessage = "{0} - حقل مطلوب")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "{0} يجب أن يكون بين {2} و {1}")]
        public string Name { get; set; } = default!;

        [Display(Name = "العنوان")]
        [Required(ErrorMessage = "{0} - حقل مطلوب")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "{0} يجب أن يكون بين {2} و {1}")]
        public string? Title { get; set; }

        [Display(Name = "رابط الصورة")]
        [StringLength(100)]
        public string? ImageUrl { get; set; }
    }
}
