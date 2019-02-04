using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityDoZero.Models;
using Identity.AppService.ViewModel.Home;
using Microsoft.AspNetCore.Identity;
using Identity.Dominio.Classes;

namespace IdentityDoZero.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<Usuario> signInManager;

        public HomeController(SignInManager<Usuario> signInManager)
        {
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            var viewModel = new LoginViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var resultado = await signInManager.PasswordSignInAsync(login.Email,login.Senha,false,lockoutOnFailure:true);
                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Mensagem = "O usuário ou senha estão incorretos";
                    return View(login);
                }
            }
            else
            {
                return View(login);
            }
        }
    }
}
