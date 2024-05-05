using travel_analyzer_dijkstra.Models;

namespace travel_analyzer_dijkstra.Services
{
    public interface IGrafoService
    {
        Task<List<Rota>> EncontrarMenorCusto(string origem, string destino);
    }
}