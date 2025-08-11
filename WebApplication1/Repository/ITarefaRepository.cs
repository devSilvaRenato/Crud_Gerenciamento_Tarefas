using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface ITarefaRepository
    {
        Task<List<TarefaModel>> BuscarTarefasRepository();
        Task<TarefaModel?> BuscarPorIdRepository(int id);
        Task<TarefaModel> CriarTarefaRepository(TarefaModel tarefa);
        Task<TarefaModel> AtualizarTarefaRepository(TarefaModel tarefa);
        Task<bool> DeletarTarefaRepository(int id);
        Task<bool> ExisteDuplicidadeRepository(string titulo, DateTime dataVencimento);
        Task<bool> ExisteDuplicidadeParaAtualizacaoRepository(string titulo, DateTime dataVencimento, int id);
    }

}
