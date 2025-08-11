using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Models;
using WebApplication1.Repository;
using static WebApplication1.Models.TarefaModel;


namespace WebApplication1.Services
{

    public class TarefaService : ITarefaService
    {

        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }


        public void ValidarTarefa(TarefaModel tarefa)
        {
            if (tarefa is null)
                throw new ArgumentNullException(nameof(tarefa));

            if (string.IsNullOrWhiteSpace(tarefa.Titulo))
                throw new InvalidOperationException("Título é obrigatório.");

            if (tarefa.DataVencimento.Date < DateTime.Today)
                throw new InvalidOperationException("Data de vencimento deve ser hoje ou uma data futura.");


        }


        public void ValidarStatus(StatusTarefa status)
        {
            if (!Enum.IsDefined(typeof(StatusTarefa), status))
                throw new InvalidOperationException("Status inválido. Use: Pendente, EmProgresso ou Concluida.");
        }


        public async Task VerificaDuplicidadeParaAtualizacao(TarefaModel tarefa)
        {
            // Verifica duplicidade de tarefas com o mesmo título e data, mas ignorando a tarefa atual (com o ID)
            var verifica_duplicidade = await _tarefaRepository.ExisteDuplicidadeParaAtualizacaoRepository(
                tarefa.Titulo,
                tarefa.DataVencimento,
                tarefa.Id  // Ignora a tarefa com o mesmo ID
            );

            if (verifica_duplicidade)
            {
                throw new InvalidOperationException("Já existe uma tarefa com os mesmos dados (título e data de vencimento).");
            }
        }


        public async Task<List<TarefaModel>> BuscarTarefas()
        {
            return await _tarefaRepository.BuscarTarefasRepository();
        }

        public async Task<TarefaModel?> BuscarPorIdTarefa(int id)
        {
            var resultado_consulta_id = await _tarefaRepository.BuscarPorIdRepository(id);

            if (resultado_consulta_id == null)
            {
                throw new KeyNotFoundException("Tarefa não encontrada.");
            }

            return resultado_consulta_id;
        }

        public async Task<TarefaModel> CriarTarefa(CriarTarefaDto CriarTarefadto)
        {
            var tarefa = new TarefaModel
            {
                Titulo = CriarTarefadto.Titulo.Trim(),
                Descricao = CriarTarefadto.Descricao?.Trim(),
                DataVencimento = CriarTarefadto.DataVencimento,
                Status = CriarTarefadto.Status
            };

            ValidarStatus(tarefa.Status);
            ValidarTarefa(tarefa);

            // Verifica duplicidade de tarefa com o mesmo título e data, independentemente do status
            var verifica_duplicidade = await _tarefaRepository.ExisteDuplicidadeRepository(tarefa.Titulo, tarefa.DataVencimento);
            if (verifica_duplicidade)
            {
                throw new InvalidOperationException("Já existe uma tarefa com os mesmos dados (título e data de vencimento).");
            }

            return await _tarefaRepository.CriarTarefaRepository(tarefa);
        }



        public async Task<TarefaModel> AtualizarTarefa(int id, AtualizarTarefaDto AttTarefaDto)
        {
            var tarefa = await _tarefaRepository.BuscarPorIdRepository(id);
            if (tarefa == null)
                throw new KeyNotFoundException("Tarefa não encontrada.");

            // Verifica se nenhum campo foi informado para atualização
            if (AttTarefaDto.GetType().GetProperties().All(p => p.GetValue(AttTarefaDto) is null))
            {
                throw new InvalidOperationException("Nenhum dado ou campo para atualização foi informado.");
            }

            // Atualiza apenas os campos que foram passados
            if (AttTarefaDto.Titulo is not null)
                tarefa.Titulo = AttTarefaDto.Titulo.Trim();
            if (AttTarefaDto.Descricao is not null)
                tarefa.Descricao = AttTarefaDto.Descricao.Trim();
            if (AttTarefaDto.DataVencimento.HasValue)
                tarefa.DataVencimento = AttTarefaDto.DataVencimento.Value;
            if (AttTarefaDto.Status.HasValue)
            {
                ValidarStatus(AttTarefaDto.Status.Value);
                tarefa.Status = AttTarefaDto.Status.Value;
            }

            // Verifica duplicidade de título e data de vencimento, mas ignora a própria tarefa (com o ID)
            await VerificaDuplicidadeParaAtualizacao(tarefa);

            ValidarTarefa(tarefa);
            return await _tarefaRepository.AtualizarTarefaRepository(tarefa);
        }



        public async Task DeletarTarefa(int id)
        {
            var resultado_delete_tarefa = await _tarefaRepository.DeletarTarefaRepository(id);

            if (!resultado_delete_tarefa)
            {
                throw new KeyNotFoundException("Tarefa não encontrada.");
            }

            return;

        }
    }
}