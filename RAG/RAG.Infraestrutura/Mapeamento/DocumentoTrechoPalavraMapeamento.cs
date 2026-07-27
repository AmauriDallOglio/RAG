using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RAG.Dominio.Entidade;

namespace RAG.Infraestrutura.Mapeamento
{
    public class DocumentoTrechoPalavraMapeamento : IEntityTypeConfiguration<DocumentoTrechoPalavra>
    {
        public void Configure(EntityTypeBuilder<DocumentoTrechoPalavra> builder)
        {
            builder.ToTable("DocumentoTrechoPalavra");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Palavra).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Quantidade).IsRequired();

            builder.HasOne(p => p.DocumentoTrecho)
                   .WithMany(t => t.Palavras)
                   .HasForeignKey(p => p.IdDocumentoTrecho)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

    /*
     * 
     * 
     
    CREATE TABLE DocumentoTrechosPalavra (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Palavra NVARCHAR(100) NOT NULL,
    Quantidade INT NOT NULL,
    IdDocumentoTrecho INT NULL,
    CONSTRAINT FK_DocumentoTrechoPalavra_DocumentoTrecho FOREIGN KEY (IdDocumentoTrecho) REFERENCES DocumentoTrecho(Id) 
    );

     * 
     */
}
