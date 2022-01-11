using System.ComponentModel.DataAnnotations.Schema;

namespace ConcursoSofka.Data
{
    public class Historical : EntityBase
    {
     
        public int IdAnswer {get; set; }

        public string User { get; set; }


        [ForeignKey("IdAnswer")]
        public virtual Answer Answer { get; set; }

    }
}