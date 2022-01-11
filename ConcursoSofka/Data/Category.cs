//Creación de bd categoría.

using System.ComponentModel.DataAnnotations.Schema;

namespace ConcursoSofka.Data
{
    public class Category : EntityBase
    {
        public Category()
        {
            Questions = new HashSet<Questions>();
        }
        public string Name { get; set; }
        public int Dificult { get; set; }

        // apuntador hacia la tabla de interes

        [InverseProperty("Category")]
        public ICollection<Questions> Questions { get; set; }
    }
}
