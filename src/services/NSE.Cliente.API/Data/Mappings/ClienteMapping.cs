using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Clientes.API.Models;
using NSE.Core.DomainObjects;

namespace NSE.Clientes.API.Data.Mappings
{
    public class ClienteMapping: IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.OwnsOne(x => x.Cpf, tf =>
             {
                 tf.Property(c => c.Numero)
                 .IsRequired()
                 .HasMaxLength(Cpf.CpfLength)
                 .HasColumnName("CPF")
                 .HasColumnType($"varchar({Cpf.CpfLength})");
             });

            builder.OwnsOne(x => x.Email, tf =>
            {
                tf.Property(c => c._email)
                .IsRequired() 
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.MaxLengthEmail})");
            });

            //definindo a chave estrangeira na tabela de endereço 1:1
            builder.HasOne(x => x.Endereco)
                .WithOne(y => y.Cliente);
             
            builder.ToTable("Clientes");
        }
      
    }
}
