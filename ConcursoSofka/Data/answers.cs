//BD preguntas heredado de la entitybase con caractetisticas propias y relacionado con historial.


using System.ComponentModel.DataAnnotations.Schema;

namespace ConcursoSofka.Data
{
    public class Answer : EntityBase
    {
        public Answer()
        {
            Historicals = new HashSet<Historical>();
        }
        //registro acumulado de interacción del user.
        public string Text { get; set; }
        public int IdQuestion { get; set; }

        public bool IsCorrect { get; set; }

        [ForeignKey("IdQuestion")]
        public virtual Questions Question { get; set; }
        [InverseProperty("Answer")]
        public virtual ICollection<Historical> Historicals { get; set; }

    }
}