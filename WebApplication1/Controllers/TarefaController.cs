using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Dto;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;
        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TarefaModel>>> BuscarTarefas()
        {
            var tarefas = await _tarefaService.BuscarTarefas();

            return Ok(tarefas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TarefaModel>> BuscarTarefaPorId(int id)
        {
            try
            {

                var tarefa = await _tarefaService.BuscarPorIdTarefa(id);
                return Ok(tarefa);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }


        [HttpPost]

        public async Task<ActionResult<TarefaModel>> CriarTarefa(CriarTarefaDto CriarTarefadto)
        {
            try
            {
                var tarefa = await _tarefaService.CriarTarefa(CriarTarefadto);
                return Ok("Tarefa criada com sucesso!");
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TarefaModel>> AtualizarTarefa(int id, [FromBody] AtualizarTarefaDto AttTarefaDto)
        {
            try
            {
                var tarefaAtualizada = await _tarefaService.AtualizarTarefa(id, AttTarefaDto);
                return Ok(tarefaAtualizada);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletarTarefa(int id)
        {
            try
            {
                await _tarefaService.DeletarTarefa(id);
                return Ok("Tarefa Excluida!");
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
