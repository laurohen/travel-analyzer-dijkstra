namespace travel_analyzer_dijkstra.Models
{
    public class AlterarRotaRequest
    {
        public int Id { get; set; }
        public string? Origem { get; set; }
        public string? Destino { get; set; }
        public decimal? Valor { get; set; }
    }
}