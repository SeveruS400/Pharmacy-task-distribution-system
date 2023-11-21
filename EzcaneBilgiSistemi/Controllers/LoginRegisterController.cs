using Entities.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace EzcaneBilgiSistemi.Controllers
{
    public class LoginRegisterController : Controller
    {
        private readonly IRepositoryManager _manager;

        public LoginRegisterController(ILogger<HomeController> logger, IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            return ViewComponent("Login");
        }
        
        public async Task<IActionResult> Login(Users user)
        {
            if (user!= null)
            {
                var userinfo = await _manager.User.Login(user.Email);
                if (userinfo!=null && (user.Password==userinfo.Password))
                {
                    HttpContext.Session.SetString("UserRole", userinfo.UserRole.RoleName);
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.ErrorMessage = "Invalid email or password";
            return ViewComponent("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Register(Users user)
        {

            if (user.UserName != null)
            {
                // Kullanıcı adı ve e-posta benzersiz olmalıdır
                var userIsAvailable = await _manager.User.GetUserByEmail(user.Email);
                if (userIsAvailable==null)
                {

                    // Yeni kullanıcıyı oluştur
                    Users newUser = new Users
                    {
                        UserName = user.UserName,
                        Password = user.Password,
                        Email = user.Email,
                        IsEmailConfirmed = false,
                        UserRoleId = 3

                    };
                    await _manager.User.CreateAsync(newUser);
                    ViewBag.ErrorMessage = "Kayıt Başarılı. Lütfen Giriş yapınız";
                    // Kayıt başarılı, ana sayfaya yönlendir
                    return ViewComponent("Login");
                }
                else
                {
                    // Kullanıcı adı veya e-posta benzersiz değil
                    ViewBag.ErrorMessage = "Username or email already exists";
                    return ViewComponent("Login");
                }
            }

            // Model doğrulama hatası, formu tekrar göster
            return ViewComponent("Login");
        }

        public async Task<IActionResult> Logout()
        {
            // Kullanıcının tüm oturumlarını temizle
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return ViewComponent("Login");
        }

    }
}
