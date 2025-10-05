using System.ComponentModel.DataAnnotations;

namespace TodoApp.WebMvc.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        [MaxLength(50, ErrorMessage = "Kullanıcı adı en fazla 50 karakter olmalıdır.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre tekrarı zorunludur")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
