using ConcessionariaAPI.Entities;
using ConcessionariaAPI.Repositories;


namespace ConcessionariaAPI.Services
{
    public class AutomovelService : IAutomovelService
    {
        private readonly IAutomovelRepository _repository;

        public AutomovelService(IAutomovelRepository repository)
        {
            
            _repository = repository;
        }

        public void Inserir(Automovel automovel)
        {
            if (!VerificarPlacaOuChassiValido(automovel))
                throw new ArgumentException("Placa ou chassi inválido. (A placa deve ter 7 caracteres, ou o chassi deve ter 17 caracteres).");

            if (!VerificarAnoValido(automovel))
                throw new ArgumentException("Ano inválido. O ano deve ser entre 1886 e o ano atual.");

            if (!VerificarAnoModeloValido(automovel))
                throw new ArgumentException("Ano do modelo inválido. O ano do modelo deve ser igual ou um ano superior ao ano do automóvel.");

            _repository.Inserir(automovel);
        }

        public void Alterar(Automovel automovel)
        {
            if (!VerificarPlacaOuChassiValido(automovel))
                throw new ArgumentException("Placa ou chassi inválido. (A placa deve ter 7 caracteres, ou o chassi deve ter 17 caracteres).");

            if (!VerificarAnoValido(automovel))
                throw new ArgumentException("Ano inválido. O ano deve ser entre 1886 e o ano atual.");

            if (!VerificarAnoModeloValido(automovel))
                throw new ArgumentException("Ano do modelo inválido. O ano do modelo deve ser igual ou um ano superior ao ano do automóvel.");

            _repository.Alterar(automovel);
        }

        public void Deletar(Automovel automovel) => _repository.Deletar(automovel);
        public List<Automovel> Listar() => _repository.Listar();
        public Automovel ObterPorId(int id) => _repository.MostrarAutomovelPorId(id);

        public bool VerificarAnoValido(Automovel automovel)
        {
            DateTime agora = DateTime.Now;

            if (automovel.Ano < 1886 || automovel.Ano > agora.Year)
            {
                return false;
            }

            return true;
        }
        public bool VerificarAnoModeloValido(Automovel automovel)
        {
            if (automovel.AnoModelo < automovel.Ano || automovel.AnoModelo > automovel.Ano + 1)
            {
                return false;
            }

            return true;
        }

        public bool VerificarPlacaOuChassiValido(Automovel automovel)
        {

            if (automovel.PlacaOuChassi.Length == 7 || automovel.PlacaOuChassi.Length == 17)
            {
                return true;
            }

            return false;
        }

    }
}
