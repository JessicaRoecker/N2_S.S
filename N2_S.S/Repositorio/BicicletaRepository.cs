using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using N2_S.S.Model;

namespace N2_S.S.Repositorio
{
    public class BicicletaRepository : IBicicletaRepository
    {
        private readonly IConfiguration _configuration;
        private string connectionString { get { return _configuration.GetConnectionString("MysqlConnection"); } }

        public BicicletaRepository(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public async Task<IEnumerable<BicicletasModel>> BuscarTodasBicletasAsync()
        {
            using (var con = new MySqlConnection(connectionString))
            {

                return await con.QueryAsync<BicicletasModel>("Select * from bicicletas");

            }

        }
        public async Task<BicicletasModel> BuscarBicicletaCorMarcaAsync(string marca_bicicleta, string cor_bicicleta)
        {
            using (var con = new MySqlConnection(connectionString))
            {
                const string query = "SELECT * FROM bicicletas WHERE cor_bicicleta = @cor_bicicleta and marca_bicicleta = @marca_bicicleta";
                return await con.QueryFirstOrDefaultAsync<BicicletasModel>(query, new {cor_bicicleta = cor_bicicleta, marca_bicicleta = marca_bicicleta });
            }
        }



        public async Task<bool> AdicionarAsync(BicicletasModel model)
        {
            using (var con = new MySqlConnection(connectionString))
            {
                const string query = "INSERT INTO bicicletas (marca_bicicleta, cor_bicicleta) VALUES (@Marca_Bicicleta, @Cor_Bicicleta)";
                var rowsAffected = await con.ExecuteAsync(query, model);
                return rowsAffected > 0;
            }
        }


        public async Task<bool> AtualizarAsync(BicicletasModel model, int id)
        {
            using (var con = new MySqlConnection(connectionString))
            {
                const string query = "UPDATE bicicletas SET marca_bicicleta = @Marca_Bicicleta, cor_bicicleta = @Cor_Bicicleta WHERE Cod_Bicicleta = @Id";
                var updatedModel = new { Id = id, model.Marca_Bicicleta, model.Cor_Bicicleta };
                var rowsAffected = await con.ExecuteAsync(query, updatedModel);
                return rowsAffected > 0;
            }
        }


        public async Task<bool> DeletarAsync(int id)
        {
            using (var con = new MySqlConnection(connectionString))
            {
                const string query = "DELETE FROM bicicletas WHERE cod_bicicleta = @Id";
                var rowsAffected = await con.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
        }

    }
}
