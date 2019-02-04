using Identity.Dominio.Classes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.InfraEstrutura.Contexto
{
    public class IdentityContexto : IdentityDbContext<Usuario, Perfil, string>
    {
        public IdentityContexto(DbContextOptions<IdentityContexto> options) : base(options)
        {
        }
    }
}