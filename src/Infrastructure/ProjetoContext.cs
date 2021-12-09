using Domain.PessoaAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ProjetoContext : DbContext, IUnitOfWork
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        public ProjetoContext(DbContextOptions<ProjetoContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //usado so em debug para obter erros mais detalhados
#if DEBUG
            optionsBuilder.EnableDetailedErrors();
#endif

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seta as colunas string pra varchar(100)
            var propriedades = modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetProperties().Where(y => y.ClrType == typeof(string)));
            foreach (var property in propriedades)
            {
                property.SetColumnType("varchar(100)");
            }
            // pega as configurações das entidades
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjetoContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public async Task<bool> Commit()
        {
            var entries = ChangeTracker.Entries()
                                 .Where(e => e.Entity is Entity && (
                                        e.State == EntityState.Added
                                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((Entity)entityEntry.Entity).UpdateAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((Entity)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property("CreatedAt").IsModified = false;
                }
            }

            var sucesso = await base.SaveChangesAsync() > 0;
            return sucesso;
        }
    }
}
