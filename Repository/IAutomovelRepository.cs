using ConcessionariaAPI.Entities;

namespace ConcessionariaAPI.Repositories
{
    public interface IAutomovelRepository
    {
        void Inserir(Automovel automovel);
        List<Automovel> Listar();
        void Alterar(Automovel automovel);
        Automovel MostrarAutomovelPorId(int id);
        void Deletar(Automovel automovel);
    }
}