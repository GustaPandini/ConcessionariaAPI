namespace ConsoleApp2.Entity;

using System.ComponentModel.DataAnnotations;

public class Automovel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "A Marca é obrigatória.")]
    public string Marca { get; set; }

    [Required(ErrorMessage = "O Modelo é obrigatório.")]
    public string Modelo { get; set; }

    [Required(ErrorMessage = "O PowerTrain é obrigatório.")]
    public string Powertrain { get; set; }

    [Required(ErrorMessage = "A Versão é obrigatória.")]
    public string Versao { get; set; }

    [Required(ErrorMessage = "A Cor é obrigatória.")]
    public string Cor { get; set; }

    [Range(1886, int.MaxValue, ErrorMessage = "O ano deve ser maior que 1886.")]
    public int Ano { get; set; }

    public int AnoModelo { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quilometragem não pode ser negativa.")]
    public int Quilometragem { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que 0.")]
    public decimal Preco { get; set; }

    public bool Blindado { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade de donos não pode ser negativa.")]
    public int QuantidadeDonos { get; set; }
}