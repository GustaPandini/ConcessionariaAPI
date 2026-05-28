using ConcessionariaAPI.Entity;

namespace ConcessionariaAPI.Services
{
    public interface IAutomovelService
    {
        void Inserir(Automovel automovel);
        void Alterar(Automovel automovel);
        void Deletar(Automovel automovel);
        List<Automovel> Listar();
        Automovel ObterPorId(int id);
    }
}