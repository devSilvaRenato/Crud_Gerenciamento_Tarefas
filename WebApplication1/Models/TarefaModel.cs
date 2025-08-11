using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class TarefaModel
    {
        public enum StatusTarefa
        {
            Pendente = 0,
            EmProgresso = 1,
            Concluida = 2
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // IDENTITY(1,1) no banco

        [Required(ErrorMessage = "Título é obrigatório.")]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Data de vencimento é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataVencimento { get; set; }

        [Required(ErrorMessage = "Status é obrigatório.")]
        public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
    }
}
