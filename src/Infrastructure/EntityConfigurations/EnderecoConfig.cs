using Domain.PessoaAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class EnderecoConfig : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Enderecos");

            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Logradouro).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Cidade).IsRequired();
            builder.Property(x => x.Estado).IsRequired();
            builder.Property(x => x.Bairro).IsRequired();
            builder.Property(x => x.Numero);
            builder.Property(x => x.CEP).IsRequired();

        }
    }
}
