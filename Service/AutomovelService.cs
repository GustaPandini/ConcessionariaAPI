using ConcessionariaAPI.Entity;
using ConcessionariaAPI.Repository;

namespace ConcessionariaAPI.Services
{
    public class AutomovelService
    {
        private readonly AutomovelRepository _repository;

        public AutomovelService(AutomovelRepository repository)
        {
            
            _repository = repository;
        }

        public void Inserir(Automovel automovel)
        {
            if (VerificarAnoValido(automovel))
            {
                if (VerificarAnoModeloValido(automovel))
                {
                    _repository.Inserir(automovel);
                }
                else
                {
                    throw new ArgumentException("Ano do modelo inválido. O ano do modelo deve ser igual ou um ano superior ao ano do automóvel.");
                }
            }
            else
            {
                throw new ArgumentException("Ano inválido. O ano deve ser entre 1886 e o ano atual.");
            }
        }

        public void Alterar(Automovel automovel)
        {
            if (VerificarAnoValido(automovel))
            {
                if (VerificarAnoModeloValido(automovel))
                {
                    _repository.Alterar(automovel);
                }
                else
                {
                    throw new ArgumentException("Ano do modelo inválido. O ano do modelo deve ser igual ou um ano superior ao ano do automóvel.");
                }
            }
            else
            {
                throw new ArgumentException("Ano inválido. O ano deve ser entre 1886 e o ano atual.");
            }
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
        
    }
}
