using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using static WebApplication1.Models.TarefaModel;

namespace WebApplication1.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _dbContext;

        public TarefaRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TarefaModel>> BuscarTarefasRepository()
        {
            return await _dbContext.Tarefas.AsNoTracking().ToListAsync();
        }

        public async Task<TarefaModel?> BuscarPorIdRepository(int id)
        {
            return await _dbContext.Tarefas.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TarefaModel> CriarTarefaRepository(TarefaModel tarefa)
        {
            _dbContext.Tarefas.Add(tarefa);
            await _dbContext.SaveChangesAsync();
            return tarefa;
        }

        public async Task<TarefaModel> AtualizarTarefaRepository(TarefaModel tarefa)
        {
            _dbContext.Tarefas.Update(tarefa);
            await _dbContext.SaveChangesAsync();
            return tarefa;
        }

        public async Task<bool> DeletarTarefaRepository(int id)
        {
            var tarefa = await _dbContext.Tarefas.FindAsync(id);

            if (tarefa == null)
            {
                return false;  // Tarefa não encontrada
            }

            _dbContext.Tarefas.Remove(tarefa);
            await _dbContext.SaveChangesAsync();
            return true;  // Tarefa deletada com sucesso
        }

        public async Task<bool> ExisteDuplicidadeRepository(string titulo, DateTime dataVencimento)
        {
            return await _dbContext.Tarefas
                .AnyAsync(t => t.Titulo == titulo && t.DataVencimento.Date == dataVencimento.Date);
        }

        public async Task<bool> ExisteDuplicidadeParaAtualizacaoRepository(string titulo, DateTime dataVencimento, int ProprioId)
        {
            return await _dbContext.Tarefas
                .AnyAsync(t => t.Titulo == titulo
                               && t.DataVencimento.Date == dataVencimento.Date
                               && t.Id != ProprioId);  // Ignora a tarefa atual
        }

    }
}
