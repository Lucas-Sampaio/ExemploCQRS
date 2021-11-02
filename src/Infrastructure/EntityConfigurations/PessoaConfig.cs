using Domain.PessoaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class PessoaConfig : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoas");

            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.DataNascimento).IsRequired();
            builder.OwnsOne(x => x.Cpf, e =>
            {
                e.Property(x => x.Numero).IsRequired().HasMaxLength(11);
            });
            builder.HasMany(x => x.Enderecos).WithOne();
        }
    }
}
