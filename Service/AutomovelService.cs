using ConsoleApp2.Entity;
using ConsoleApp2.Repository;

namespace ConsoleApp2.Services
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

        public void Alterar(Automovel automovel) => _repository.Alterar(automovel);
        public void Deletar(Automovel automovel) => _repository.Deletar(automovel);
        public List<Automovel> Listar() => _repository.Listar();
        public Automovel ObterPorId(int id) => _repository.MostrarAutomovelPorId(id);

        public bool ValidacaoValorString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            return true;
        }

        public bool LerValidacaoBlindado(string input, out bool blindado)
        {
            blindado = false;
            if (input == "sim")
            {
                blindado = true;
                return true;
            }
            else if (input == "não" || input == "nao")
            {
                blindado = false;
                return true;
            }
            return blindado;
        }

        public string MostrarBlindagem(Automovel automovel)
        {
            string blindado;
            if (automovel.Blindado == true)
            {
                blindado = "Sim";
                return blindado;
            }
            else
            {
                blindado = "Não";
                return blindado;
            }
        }
        public string VerificarAlteracaoString(string input, string valorAntigo)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return valorAntigo;
            }
            else
            {
                return input;
            }
        }
        public bool VerificarAlteracaoInt(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return true;
            }
            return false;
        }
        public bool VerificarAlteracaoValidaInt(string input, out int valor)
        {
            if (int.TryParse(input, out valor))
            {
                return true;
            }
            return false;
        }

        public bool VerificarAlteracaoDecimal(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return true;
            }
            return false;
        }

        public bool VerificarAlteracaoValidaDecimal(string input, out decimal valorAtualizado)
        {
            if (decimal.TryParse(input, out valorAtualizado))
            {
                return true;
            }
            return false;
        }
        public bool VerificarPreco(string input, out decimal valor)
        {
            valor = 0;
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            if (!decimal.TryParse(input, out valor))
            {
                return false;
            }
            if (valor < 0)
            {
                return false;
            }
            return true;
        }

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
        public bool VerificarValorInt(string input, out int valor)
        {
            valor = 0;
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            if (!int.TryParse(input, out valor))
            {
                return false;
            }
            return true;
        }
        public bool VerificarBlindadoValido(string input, out bool blindado)
        {
            input.ToLower();
            blindado = false;
            if (input == "sim")
            {
                blindado = true;
                return true;
            }
            else if (input == "nao" || input == "não")
            {
                blindado = false;
                return true;
            }

            return false;
        }
    }
}
