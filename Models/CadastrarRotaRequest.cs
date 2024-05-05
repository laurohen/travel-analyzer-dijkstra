namespace travel_analyzer_dijkstra.Models
{
    public class CadastrarRotaRequest
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
        public decimal Valor { get; set; }
    }
}