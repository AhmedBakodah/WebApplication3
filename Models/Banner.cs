using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Banner
    {
        public int Id { get; set; }

        [Display(Name="الاسم")]
        [Required(ErrorMessage ="{0} - حقل مطلوب")]
        [StringLength(100,MinimumLength =10,ErrorMessage ="{0} يجب أن يكون بين {2} و {1}")]
        public string Name { get; set; } = string.Empty;

        [Display(Name="الوصف")]
        [Required(ErrorMessage = "{0} - حقل مطلوب")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "{0} يجب أن يكون بين {2} و {1}")]
        public string? Description { get; set; }
    }
}
