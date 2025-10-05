using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Services;

//IPasswordHasher arayüzünün BCrypt kütüphanesi kullanılarak yapılmış somut implementasyonu.


namespace TodoApp.Infrastructure.Services
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}


//Kullanıcı giriş yaptığında gelen ham şifreyi ve veritabanındaki hash’i alır.
//BCrypt.Net.BCrypt.Verify metodu:
//Hash içinde saklanan salt değerini okur.
//Ham şifreyi aynı algoritmayla yeniden hash’ler.
//Ortaya çıkan sonuç ile saklanan hash’i karşılaştırır.
//Eşleşirse true, aksi halde false döner.