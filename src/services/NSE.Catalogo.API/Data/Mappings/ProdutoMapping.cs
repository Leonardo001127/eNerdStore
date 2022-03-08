using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Catalogo.API.Models;

namespace NSE.Catalogo.API.Data.Mappings
{
    public class ProdutoMapping: IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("varchar(250)");

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnName("varchar(500)");

            builder.Property(x => x.Imagem)
                .IsRequired()
                .HasColumnName("varchar(250)");


            builder.ToTable("Produtos");
        }
      
    }
}
