using Microsoft.AspNetCore.Mvc;
using travel_analyzer_dijkstra.Infra.Repository;
using travel_analyzer_dijkstra.Services;

namespace travel_analyzer_dijkstra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrafoController : ControllerBase
    {
        private readonly IGrafoService _grafoService;
        private readonly IRotaRepository _rotaRepository;

        public GrafoController(IGrafoService grafoService, IRotaRepository rotaRepository)
        {
            _grafoService = grafoService;
            _rotaRepository = rotaRepository;
        }

        [HttpGet("{origem}/{destino}")]
        public async Task<ActionResult<List<GrafoInfo>>> GetMenorCusto(string origem, string destino)
        {
            var menorCusto = await _grafoService.EncontrarMenorCusto(origem, destino);

            if (menorCusto == null || menorCusto.Count == 0)
            {
                return NotFound(new { mensagem = $"Origem '{origem}' e/ou destino '{destino}' não cadastrados" });
            }

            var descricaoViagem = $"{origem} - ";
            foreach (var etapa in menorCusto)
            {
                descricaoViagem += $"{etapa.Destino} - ";
            }
            descricaoViagem += $"ao custo de ${menorCusto.Sum(e => e.Valor)}";

            var resposta = new
            {
                viagem = $"**{descricaoViagem}**",
                etapas = menorCusto
            };

            return Ok(resposta);

        }
    }

}
