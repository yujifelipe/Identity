using Microsoft.AspNetCore.Identity;

namespace Identity.Dominio.Classes
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }

        public string Cpf { get; set; }
    }
}

