using System.ComponentModel.DataAnnotations;
using static WebApplication1.Models.TarefaModel;

namespace WebApplication1.Dto
{
    public class CriarTarefaDto
    {
        [Required(ErrorMessage = "Título é obrigatório.")]
        [StringLength(100)]
        public string Titulo { get; set; }

        [StringLength(1000)]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Data de vencimento é obrigatória.")]
        public DateTime DataVencimento { get; set; }

        [Required(ErrorMessage = "Status é obrigatório.")]
        public StatusTarefa Status { get; set; }
    }
}
