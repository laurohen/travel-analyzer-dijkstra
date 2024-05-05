using Microsoft.AspNetCore.Mvc;
using travel_analyzer_dijkstra.Models;
using travel_analyzer_dijkstra.Services;

namespace travel_analyzer_dijkstra.Controllers
{
    public class RotaController : Controller
    {
        private readonly IRotaService _rotaService;

        public RotaController(IRotaService rotaService)
        {
            _rotaService = rotaService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<Rota> ObterRota(int id)
        {
            var rotas = await _rotaService.ObterRotaAsync(id);
            return rotas;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Rota>> ListarRotas()
        {
            var rotas = await _rotaService.ObterRotasAsync();
            return rotas;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CadastrarRota([FromBody] CadastrarRotaRequest request)
        {
            await _rotaService.CadastrarRotaAsync(request);
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> AlterarRota([FromBody] AlterarRotaRequest request)
        {
            await _rotaService.AlterarRotaAsync(request);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeletarRota(int id)
        {
            await _rotaService.DeletarRotaAsync(id);
            return Ok();
        }
    }
}
