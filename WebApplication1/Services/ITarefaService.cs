using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface ITarefaService
    {
        Task<List<TarefaModel>> BuscarTarefas();
        Task<TarefaModel?> BuscarPorIdTarefa(int id);
        Task<TarefaModel> CriarTarefa(CriarTarefaDto CriarTarefadto);
        Task<TarefaModel> AtualizarTarefa(int id, AtualizarTarefaDto AttTarefaDto);
        Task DeletarTarefa(int id);
        //Task CriarAsync(CriarTarefaDto tarefadto);
    }
}
