using MySql.Data.MySqlClient;
using Dapper;
using ConcessionariaAPI.Repositories;
using ConcessionariaAPI.Entities;

namespace ConcessionariaAPI.Repository
{
    public class AutomovelRepository : Database, IAutomovelRepository
    {
        
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
            string sql = @"SELECT 
                         Id, Marca, Modelo, Powertrain, Versao, Cor, Ano, AnoModelo, Quilometragem, Preco, Blindado, QuantidadeDonos
                         FROM automovel";

            using (MySqlConnection conexao = GetConnection())
            {
                List<Automovel> automoveis = conexao.Query<Automovel>(sql).ToList();
                return automoveis;
            }
        }
        public void Alterar(Automovel automovel)
        {
            string sql = @"UPDATE automovel 
                         SET MARCA = @Marca, MODELO = @Modelo, POWERTRAIN = @Powertrain, VERSAO = @Versao, COR = @Cor, 
                         ANO = @Ano, ANOMODELO = @AnoModelo, QUILOMETRAGEM = @Quilometragem, PRECO = @Preco, 
                         BLINDADO = @Blindado, QUANTIDADEDONOS = @QuantidadeDonos WHERE Id = @id";

            Execute(sql, automovel);
        }
        public Automovel MostrarAutomovelPorId(int Id)
        {
            string sql = @"SELECT 
                         Id, Marca, Modelo, Powertrain, Versao, Cor, Ano, AnoModelo, Quilometragem, Preco, Blindado, QuantidadeDonos
                         FROM automovel WHERE Id = @id";
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
