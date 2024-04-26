using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    [ModelMetadataType(typeof(UserMetadata))]
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "الاسم")]
        [StringLength(250, ErrorMessage = "{0} يجب ان لايتجاوز {1}")]
        public string? PersonName { get; set; }

        [Display(Name = "الوظيفة")]
        [StringLength(250, ErrorMessage = "{0} يجب ان لايتجاوز {1}")]
        public string? Job { get; set; }

        [Display(Name = "الصورة")]
        [StringLength(250, ErrorMessage = "{0} يجب ان لايتجاوز {1}")]
        public string? Img { get; set; }
        
        [Display(Name = "الفعالية؟")]
        public bool IsEnabled { get; set; }

        [NotMapped]
        public IEnumerable<IdentityRole>? AllRoles { get; set; }

        [NotMapped]
        public IEnumerable<string>? RolesIds { get; set; }

        [NotMapped]
        [Display(Name = "كلمة المرور")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "{0} يجب ان لايتجاوز {1}ولا تقل عن {2}")]
        public string? Password { get; set; }


        class UserMetadata
        {
            [Display(Name = "اسم المستخدم")]
            [Required(ErrorMessage = "{0} - حقل مطلوب")]
            //[RegularExpression("[a-zA-Z0-9]*$", ErrorMessage = "ادخل حروف انجليزية وارقام فقط")]
            [StringLength(16, MinimumLength = 3, ErrorMessage = "طول اسم المستحدم يجب أن يكون بين 3 إلى 6")]
            public string? UserName { get; set; }
            [Display(Name = "البريد الالكتروني")]
            public string? Email { get; set; }
            [Display(Name = "رقم الهاتف")]
            public string? PhoneNumber { get; set; }
        }
    }
}
