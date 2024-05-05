using travel_analyzer_dijkstra.Models;

namespace travel_analyzer_dijkstra.Infra.Repository
{
    public interface IRotaRepository
    {
        Task AlterarRotaAsync(AlterarRotaRequest request);
        Task CadastrarRotaAsync(CadastrarRotaRequest request);
        Task DeletarRotaAsync(int id);
        Task<Rota> ObterRotaAsync(int id);
        Task<IEnumerable<Rota>> ObterRotasAsync();
    }
}