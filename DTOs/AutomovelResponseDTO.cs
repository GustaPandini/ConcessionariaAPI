namespace ConcessionariaAPI.Dtos
{
    public class AutomovelResponseDTO
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Powertrain { get; set; }
        public string Versao { get; set; }
        public string Cor { get; set; }
        public int Ano { get; set; }
        public int AnoModelo { get; set; }
        public int Quilometragem { get; set; }
        public decimal Preco { get; set; }
        public bool Blindado { get; set; }
        public int QuantidadeDonos { get; set; }
    }
}