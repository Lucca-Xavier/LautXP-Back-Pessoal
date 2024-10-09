using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Mapping.Base;
using GSCBase.Infrastructure.Mappings;

namespace GSCBase.Infrastructure.Contexts
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public Context()
        {
        }

        #region BASE
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Unidade> Unidade { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Bairro> Bairro { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region BASE 
            modelBuilder.Entity<ApplicationUser>(new ApplicationUserMap().Configure);
            modelBuilder.Entity<Pessoa>(new PessoaMap().Configure);
            modelBuilder.Entity<Cliente>(new ClienteMap().Configure);
            modelBuilder.Entity<Unidade>(new UnidadeMap().Configure);
            modelBuilder.Entity<Module>(new ModuleMap().Configure);
            modelBuilder.Entity<TipoUsuario>(new TipoUsuarioMap().Configure);
            modelBuilder.Entity<Estado>(new EstadoMap().Configure);
            modelBuilder.Entity<Cidade>(new CidadeMap().Configure);
            modelBuilder.Entity<Bairro>(new BairroMap().Configure);
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
