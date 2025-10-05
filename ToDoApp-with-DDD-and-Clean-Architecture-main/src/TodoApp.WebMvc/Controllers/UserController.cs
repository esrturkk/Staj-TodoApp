using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TodoApp.WebMvc.Models;
using TodoApp.WebMvc.Services;

namespace TodoApp.WebMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly UserApiClient _userApiClient;

        public UserController(UserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var user = await _userApiClient.LoginAsync(model);
                if (user == null)
                {
                    ModelState.AddModelError("", "Geçersiz e-posta veya şifre.");
                    return View(model);
                }

                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("Username", user.Username);

                return RedirectToAction("Index", "TodoList");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Giriş işlemi sırasında bir hata oluştu: " + ex.Message);
                return View(model);
            }
        }

        // ------------------- REGISTER -------------------

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                await _userApiClient.RegisterAsync(model);

                // Kayıt başarılıysa login sayfasına yönlendir
                TempData["SuccessMessage"] = "Kayıt başarılı! Şimdi giriş yapabilirsiniz.";
                return RedirectToAction("Login", "User");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Kayıt işlemi sırasında bir hata oluştu: " + ex.Message);
                return View(model);
            }
        }



        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync();
        //    HttpContext.Session.Clear();
        //    return RedirectToAction("Login", "User");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // sadece session
            return RedirectToAction("Login", "User");
        }



        // ------- YENİ: Profil (mevcut kullanıcıyı göster) -------
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login");

            var userId = Guid.Parse(userIdStr);
            var user = await _userApiClient.GetByIdAsync(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Kullanıcı bilgileri alınamadı.";
                return RedirectToAction("Login");
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmail(Guid id, string newEmail)
        {
            await _userApiClient.UpdateEmailAsync(id, newEmail);
            HttpContext.Session.SetString("Email", newEmail);
            TempData["SuccessMessage"] = "E-posta güncellendi.";
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUsername(Guid id, string newUsername)
        {
            await _userApiClient.UpdateUsernameAsync(id, newUsername);
            HttpContext.Session.SetString("Username", newUsername);
            TempData["SuccessMessage"] = "Kullanıcı adı güncellendi.";
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            await _userApiClient.DeleteAsync(id);
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Hesabınız silindi.";
            return RedirectToAction("Register");
        }

        // (Opsiyonel) Tüm kullanıcıları listelemek için:
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userApiClient.GetAllAsync();
            return View(users);
        }



    }
}
