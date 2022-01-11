//entidad común y data time buenas practicas.
namespace ConcursoSofka.Data
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
