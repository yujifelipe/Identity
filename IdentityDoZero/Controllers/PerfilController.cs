using Identity.AppService.ViewModel.Perfil;
using Identity.Dominio.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDoZero.Controllers
{
    public class PerfilController : Controller
    {
        private readonly RoleManager<Perfil> Perfil;

        public PerfilController(RoleManager<Perfil> perfil)
        {
            Perfil = perfil;
        }

        public IActionResult Index()
        {
            var viewModel = new List<ListarPerfilViewModel>();

            viewModel = Perfil.Roles.Select(r => new ListarPerfilViewModel
            {
                Id = r.Id,
                NomeDoPerfil = r.Name,
                Descricao = r.Descricao,
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AdicionarPerfil()
        {
            var ViewModel = new AdicionarPerfilViewModel();
            
            return View(ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPerfil(AdicionarPerfilViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {
                var ExistePerfilComMesmoNome = await Perfil.RoleExistsAsync(ViewModel.NomeDoPerfil);

                if (!ExistePerfilComMesmoNome)
                {
                    var perfil = new Perfil()
                    {
                        Name = ViewModel.NomeDoPerfil,
                        Descricao = ViewModel.Descricao
                    };
                    try
                    {
                        await Perfil.CreateAsync(perfil);
                        ViewBag.Mensagem = "Perfil Adicionado Com Sucesso";
                    }
                    catch(Exception ex)
                    {
                        ViewBag.Mensagem = ex.Message;
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.Mensagem = "Já existe um perfil com esse nome";
                }
            }

            return View(ViewModel);
        }
    }
}