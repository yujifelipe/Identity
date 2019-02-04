using Identity.AppService.ViewModel.Usuario;
using Identity.Dominio.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDoZero.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<Usuario> Usuario;

        private readonly RoleManager<Perfil> Perfil;

        public UsuarioController(UserManager<Usuario> usuario, RoleManager<Perfil> perfil)
        {
            Usuario = usuario;
            Perfil = perfil;
        }

        public IActionResult Index()
        {
            var viewModel = new List<ListarUsuariosViewModel>();

            viewModel = Usuario.Users.Select(r => new ListarUsuariosViewModel
            {
                Nome = r.Nome,
                Email = r.Email,
                Cpf = r.Cpf
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AdicionarUsuario()
        {
            var ViewModel = new AdicionarUsuarioViewModel();

            return View(ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarUsuario(AdicionarUsuarioViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                var emailJaUtilizado = Usuario.Users.Any(x=>x.Email == ViewModel.Email);

                if (emailJaUtilizado)
                {
                    ModelState.AddModelError("Email", "Email já cadastrado");
                }

                var cpfJaUtilizado = Usuario.Users.Any(x => x.Cpf == ViewModel.Cpf);

                if (cpfJaUtilizado)
                {
                    ModelState.AddModelError("Cpf", "Cpf já cadastrado");
                }

                if (!emailJaUtilizado && !cpfJaUtilizado)
                {
                    var usuario = new Usuario()
                    {
                        UserName = ViewModel.Email,
                        Nome = ViewModel.Nome,
                        Cpf = ViewModel.Cpf,
                        Email = ViewModel.Email,
                    };
                    try
                    {
                        var resultado = await Usuario.CreateAsync(usuario, ViewModel.Password);
                        if (resultado.Succeeded)
                        {
                            ViewBag.Mensagem = "Usuario Adicionado Com Sucesso";
                        }
                        else
                        {
                            ViewBag.Mensagem = "Ocorreu um erro";
                        }
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Mensagem = ex.Message;
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.Mensagem = "Insira os campos obrigatórios";
                }
            }

            return View(ViewModel);
        }
    }
}