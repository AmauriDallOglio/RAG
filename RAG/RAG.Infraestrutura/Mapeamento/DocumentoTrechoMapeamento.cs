using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RAG.Dominio.Entidade;

namespace RAG.Infraestrutura.Mapeamento
{
    public class DocumentoTrechoMapeamento : IEntityTypeConfiguration<DocumentoTrecho>
    {
        public void Configure(EntityTypeBuilder<DocumentoTrecho> builder)
        {
            builder.ToTable("DocumentoTrecho");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Frase).IsRequired();

            builder.HasOne(t => t.Documento)
                   .WithMany(d => d.Trechos)
                   .HasForeignKey(t => t.IdDocumento)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

    /*
     * 
     * 
     * 
     * 
     * 
     
 
     * 
     * 
     * 
     */
}
