//representación codificada de la bd.

using Microsoft.EntityFrameworkCore;

namespace ConcursoSofka.Data
{
    public class BusinessContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            //overriding nueva funcionalidad a la funcion
        {
            optionsBuilder.UseSqlServer("server=localhost;database=ConcursoSofka;trusted_connection=true;");
        }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Historical> Historicals { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
    }
}
