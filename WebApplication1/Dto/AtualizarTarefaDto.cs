using static WebApplication1.Models.TarefaModel;

namespace WebApplication1.Dto
{
    public class AtualizarTarefaDto
    {
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public StatusTarefa? Status { get; set; }
    }
}
