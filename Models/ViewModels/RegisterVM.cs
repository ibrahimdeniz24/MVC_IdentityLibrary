using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _22_MVC_Identity.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez")]
        [Display(Name = "Kullanıcı Adi")]
        [MinLength(3, ErrorMessage = "kullanıcı Adi 3 karakterden az olamaz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre Boş Geçilemez")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "E-Mail Boş Geçilemez")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Mail Formatı Doğrulanamadı",ErrorMessageResourceName =default)]
        public string Email { get; set; }
    }
}
