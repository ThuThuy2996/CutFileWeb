using CutFileWeb.Data;
using CutFileWeb.Models;
using CutFileWeb.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CutFileWeb.Controllers.Admin
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user != null)
            {
                var checkPass = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (checkPass)
                {
                    var resultLogin = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (resultLogin.Succeeded)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }
            TempData["Error"] = "Wrong credentials! Please try again.";
            return View(loginViewModel);
        }
    }
}
