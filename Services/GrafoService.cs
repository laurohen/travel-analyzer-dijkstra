using travel_analyzer_dijkstra.Infra.Repository;
using travel_analyzer_dijkstra.Models;

namespace travel_analyzer_dijkstra.Services
{
    public class GrafoService : IGrafoService
    {
        private readonly IRotaRepository _rotaRepository;

        public GrafoService(IRotaRepository rotaRepository)
        {
            _rotaRepository = rotaRepository;
        }

        public async Task<List<Rota>> EncontrarMenorCusto(string origem, string destino)
        {
            var grafos = await _rotaRepository.ObterRotasAsync();

            if (!grafos.Any(g => g.Origem == origem) || !grafos.Any(g => g.Destino == destino))
            {
                return null;
            }

            var distancias = new Dictionary<string, decimal>();
            var anteriores = new Dictionary<string, string>();
            var naoVisitados = new List<string>();

            foreach (var vertice in grafos.Select(g => g.Origem).Concat(grafos.Select(g => g.Destino)).Distinct())
            {
                distancias[vertice] = vertice == origem ? 0 : int.MaxValue;
                anteriores[vertice] = null;
                naoVisitados.Add(vertice);
            }

            while (naoVisitados.Count > 0)
            {
                var u = naoVisitados.OrderBy(v => distancias[v]).First();
                naoVisitados.Remove(u);

                foreach (var v in grafos.Where(g => g.Origem == u).Select(g => g.Destino))
                {
                    var alt = distancias[u] + grafos.First(g => g.Origem == u && g.Destino == v).Valor;
                    if (alt < distancias[v])
                    {
                        distancias[v] = alt;
                        anteriores[v] = u;
                    }
                }
            }

            var caminho = new List<string>();
            var atual = destino;
            while (atual != null)
            {
                caminho.Insert(0, atual);
                atual = anteriores[atual];
            }

            var resultado = new List<Rota>();
            for (int i = 0; i < caminho.Count - 1; i++)
            {
                resultado.Add(grafos.First(g => g.Origem == caminho[i] && g.Destino == caminho[i + 1]));
            }

            return resultado;
        }
    }

}
