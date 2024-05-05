using Agenda.Infrastructure.DataAccess;
using Dapper;
using System.Runtime.ConstrainedExecution;
using travel_analyzer_dijkstra.Models;

namespace travel_analyzer_dijkstra.Infra.Repository
{
    public class RotaRepository : IRotaRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public RotaRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task AlterarRotaAsync(AlterarRotaRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var param = new DynamicParameters();
            param.Add("@Id", request.Id, dbType: System.Data.DbType.Int32);

            param.Add("@Origem", request.Origem, dbType: System.Data.DbType.String, size: 3);
            param.Add("@Destino", request.Destino, dbType: System.Data.DbType.String, size: 3);
            param.Add("@Valor", request.Valor, dbType: System.Data.DbType.Decimal);

            const string sql = @"
    UPDATE Rotas
       SET 
       Origem = ISNULL(@Origem, Origem),
       Destino = ISNULL(@Destino, Destino),
       Valor = ISNULL(@Valor, Valor)
     WHERE Id = @Id;
";

            using var connection = _connectionFactory.Create();
            await connection.ExecuteAsync(sql, param);
        }

        public async Task CadastrarRotaAsync(CadastrarRotaRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var param = new DynamicParameters();
            param.Add("@Origem", request.Origem, dbType: System.Data.DbType.String, size: 3);
            param.Add("@Destino", request.Destino, dbType: System.Data.DbType.String, size: 3);
            param.Add("@Valor", request.Valor, dbType: System.Data.DbType.Decimal);

            const string sql = @"
        INSERT INTO Rotas(Origem, Destino, Valor)
             VALUES (@Origem, @Destino, @Valor);
    ";

            using var connection = _connectionFactory.Create();
            await connection.ExecuteAsync(sql, param); ;
        }

        public async Task DeletarRotaAsync(int id)
        {

            var param = new DynamicParameters();
            param.Add("@Id", id, dbType: System.Data.DbType.Int32);

            const string sql =
                @"
            DELETE FROM Rotas
             WHERE Id = @Id;
            ";

            using var connection = _connectionFactory.Create();
            await connection.ExecuteAsync(sql, param);
        }

        public async Task<Rota> ObterRotaAsync(int id)
        {
          
            var param = new DynamicParameters();
            param.Add("@Id", id, dbType: System.Data.DbType.Int32, size: 8);

            const string sql =
                @"
            SELECT *
              FROM Rotas
             WHERE Id = @Id
            ";

            using var connection = _connectionFactory.Create();
            var rota = await connection.QueryFirstOrDefaultAsync<Rota>(sql, param);

            if (rota == null)
            {
                return null;
            }

            return rota;
        }

        public async Task<IEnumerable<Rota>> ObterRotasAsync()
        {
            const string sql = @"
                SELECT *
                  FROM Rotas
                "
            ;

            using var connection = _connectionFactory.Create();
            var rotas = await connection.QueryAsync<Rota>(sql);

            return rotas;
        }
    }
}
