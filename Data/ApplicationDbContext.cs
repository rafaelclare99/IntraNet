using IntraNet.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IntraNet.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tabelas da Intranet
        public DbSet<ChatMensagem> ChatMensagens { get; set; }
        public DbSet<Avisos> Avisos { get; set; }
        public DbSet<Processo> Processos { get; set; }
    }
}