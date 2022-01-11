//BD preguntas heredado de la entitybase y con apuntador a respuestas.

using System.ComponentModel.DataAnnotations.Schema;

namespace ConcursoSofka.Data
{
    public class Questions : EntityBase
    {
        public Questions()
        {
            Answers = new HashSet<Answer>();
        }

        //creacion de hashset para colección de respuetas
        public string Question { get; set; }        
        public int Idcategory { get; set; }
        [ForeignKey("Idcategory")]
        public virtual Category Category { get; set; }
        [InverseProperty("Question")]
        public virtual ICollection<Answer> Answers { get; set; }

    }
}

