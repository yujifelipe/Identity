using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Identity.AppService.ViewModel.Perfil
{
    public class AdicionarPerfilViewModel
    {
        public string Id { get; set; }
        [Required]
        public string NomeDoPerfil { get; set; }
        public string Descricao { get; set; }
    }
}
