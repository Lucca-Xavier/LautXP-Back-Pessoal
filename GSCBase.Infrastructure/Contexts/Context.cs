using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Entities.Base;
using GSCBase.Infrastructure.Mapping.Base;
using GSCBase.Infrastructure.Mappings;
using GSCBase.Domain.Entities.Cadastro;
using GSCBase.Infrastructure.Mapping.Cadastro;
using Cliente = GSCBase.Domain.Entities.Cadastro.Cliente;
using ClienteMap = GSCBase.Infrastructure.Mapping.Cadastro.ClienteMap;

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

        public DbSet<Produto> Produto { get; set; }

        public DbSet<Campanha> Campanha { get; set; }
        public DbSet<Venda> Venda { get; set; }

        #region BASE
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Unidade> Unidade { get; set; }
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
            modelBuilder.Entity<Produto>(new ProdutoMap().Configure);
            #region BASE 
            modelBuilder.Entity<ApplicationUser>(new ApplicationUserMap().Configure);
            modelBuilder.Entity<Pessoa>(new PessoaMap().Configure);
            modelBuilder.Entity<Cliente>(new ClienteMap().Configure);
            modelBuilder.Entity<Unidade>(new UnidadeMap().Configure);
            modelBuilder.Entity<TipoUsuario>(new TipoUsuarioMap().Configure);
            modelBuilder.Entity<Estado>(new EstadoMap().Configure);
            modelBuilder.Entity<Cidade>(new CidadeMap().Configure);
            modelBuilder.Entity<Bairro>(new BairroMap().Configure);
            #endregion
            modelBuilder.Entity<Campanha>(new CampanhaMap().Configure);

            base.OnModelCreating(modelBuilder);
        }
    }
}
