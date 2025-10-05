

using System;

namespace TodoApp.WebMvc.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }              // Kullanıcının benzersiz ID'si
        public string Username { get; set; } = "";    // İsim
        public string Email { get; set; } = "";   // E-posta adresi
    }
}


//UI (View) ile Controller arasında veri taşır.
//API’den gelen DTO’yu MVC katmanında daha sade, UI odaklı bir modele dönüştürür.
//Domain katmanındaki User entity’sini veya Application katmanındaki UserDto’yu doğrudan UI’ya sızdırmamak için kullanılır.
//View’de gerekli alanlar dışında hiçbir şey bulundurmaz (şifre, hash gibi hassas veri yok).