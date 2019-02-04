using Microsoft.AspNetCore.Identity;

namespace Identity.Dominio.Classes
{
    public class Perfil : IdentityRole
    {
        public string Descricao { get; set; }
    }
}