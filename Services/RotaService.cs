using travel_analyzer_dijkstra.Infra.Repository;
using travel_analyzer_dijkstra.Models;

namespace travel_analyzer_dijkstra.Services
{
    public class RotaService : IRotaService
    {
        private readonly IRotaRepository _rotaRepository;

        public RotaService(IRotaRepository rotaRepository)
        {
            _rotaRepository = rotaRepository;
        }

        public async Task AlterarRotaAsync(AlterarRotaRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            await _rotaRepository.AlterarRotaAsync(request);
        }

        public async Task CadastrarRotaAsync(CadastrarRotaRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            await _rotaRepository.CadastrarRotaAsync(request);
        }

        public async Task DeletarRotaAsync(int id)
        {
            await _rotaRepository.DeletarRotaAsync(id);
        }

        public async Task<Rota> ObterRotaAsync(int id)
        {
            var response = await _rotaRepository.ObterRotaAsync(id);

            return response;
        }

        public async Task<IEnumerable<Rota>> ObterRotasAsync()
        {
            var response = await _rotaRepository.ObterRotasAsync();

            return response;
        }
    }

}
