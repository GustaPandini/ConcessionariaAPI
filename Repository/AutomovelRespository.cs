using MySql.Data.MySqlClient;
using Dapper;
using ConcessionariaAPI.Repositories;
using ConcessionariaAPI.Entities;

namespace ConcessionariaAPI.Repository
{
    public class AutomovelRepository : Database, IAutomovelRepository
    {
        private readonly Automovel _automovel;
        public AutomovelRepository(IConfiguration configuration) : base(configuration) { }

        public void Inserir(Automovel automovel)
        {
            string sql = @"INSERT INTO automovel 
                         (Marca, Modelo, Powertrain, Versao, Cor, Ano, AnoModelo, Quilometragem, Preco, Blindado, QuantidadeDonos) 
                         VALUEs 
                         (@Marca, @Modelo, @Powertrain, @Versao, @Cor, @Ano, @AnoModelo, @Quilometragem, @Preco, @Blindado, @QuantidadeDonos)";
            Execute(sql, automovel);
        }
        public List<Automovel> Listar()
        {
            string sql = @"SELECT * FROM automovel";

            using (MySqlConnection conexao = GetConnection())
            {
                List<Automovel> automoveis = conexao.Query<Automovel>(sql).ToList();
                return automoveis;
            }
        }
        public void Alterar(Automovel automovel)
        {
            string sql = @"UPDATE automovel 
                         SET MARCA = @marca, MODELO = @modelo, POWERTRAIN = @powertrain, VERSAO = @versao, COR = @cor, 
                         ANO = @ano, ANOMODELO = @anoModelo, QUILOMETRAGEM = @quilometragem, PRECO = @preco, 
                         BLINDADO = @blindado, QUANTIDADEDONOS = @quantidadeDonos WHERE Id = @id";

            Execute(sql, automovel);
        }
        public Automovel MostrarAutomovelPorId(int Id)
        {
            string sql = @"SELECT * FROM automovel WHERE Id = @id";
            using (MySqlConnection conexao = GetConnection())
            {
                return conexao.QuerySingleOrDefault<Automovel>(sql, new { Id });
            }
        }
        public void Deletar(Automovel automovel)
        {
            string sql = "DELETE FROM automovel WHERE ID = @Id";

            Execute(sql, automovel);
        }
    }
}
