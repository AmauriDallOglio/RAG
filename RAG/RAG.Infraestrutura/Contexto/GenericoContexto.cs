using Microsoft.EntityFrameworkCore;
using RAG.Dominio.Entidade;
using RAG.Infraestrutura.Mapeamento;

namespace RAG.Infraestrutura.Contexto
{
    public class GenericoContexto : DbContext
    {

        public GenericoContexto(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Documento> Documento { get; set; }

        public DbSet<DocumentoTrecho> DocumentoTrecho { get; set; }

        public DbSet<DocumentoTrechoPalavra> DocumentoTrechoPalavra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new DocumentoMapeamento());
            modelBuilder.ApplyConfiguration(new DocumentoTrechoMapeamento());
            modelBuilder.ApplyConfiguration(new DocumentoTrechoPalavraMapeamento());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
