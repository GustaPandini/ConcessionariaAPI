using ConcessionariaAPI.Dtos;
using ConcessionariaAPI.Entities;
using ConcessionariaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutomovelController : ControllerBase
    {
        private readonly IAutomovelService _service;

        public AutomovelController(IAutomovelService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var automoveis = _service.Listar();
            var response = automoveis.Select(a => MapearParaResponse(a)).ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var automovel = _service.ObterPorId(id);
            if (automovel == null)
            {
                return NotFound();
            }
            return Ok(MapearParaResponse(automovel));
        }

        [HttpPost]
        public IActionResult Inserir([FromBody] AutomovelRequestDTO requestDto)
        {
            var automovel = MapearParaEntidade(requestDto);

            _service.Inserir(automovel);

            var response = MapearParaResponse(automovel);
            return CreatedAtAction(nameof(ObterPorId), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, [FromBody] AutomovelRequestDTO requestDto)
        {
            var existe = _service.ObterPorId(id);
            if (existe == null)
            {
                return NotFound();
            }

            var automovelAtualizado = MapearParaEntidade(requestDto);
            automovelAtualizado.Id = id;

            _service.Alterar(automovelAtualizado);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var automovel = _service.ObterPorId(id);
            if (automovel == null)
            {
                return NotFound();
            }

            _service.Deletar(automovel);
            return NoContent();
        }

        private Automovel MapearParaEntidade(AutomovelRequestDTO dto)
        {
            return new Automovel
            {
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Powertrain = dto.Powertrain,
                Versao = dto.Versao,
                Cor = dto.Cor,
                Ano = dto.Ano,
                AnoModelo = dto.AnoModelo,
                Quilometragem = dto.Quilometragem,
                Preco = dto.Preco,
                Blindado = dto.Blindado,
                QuantidadeDonos = dto.QuantidadeDonos
            };
        }

        private AutomovelResponseDTO MapearParaResponse(Automovel automovel)
        {
            return new AutomovelResponseDTO
            {
                Id = automovel.Id,
                Marca = automovel.Marca,
                Modelo = automovel.Modelo,
                Powertrain = automovel.Powertrain,
                Versao = automovel.Versao,
                Cor = automovel.Cor,
                Ano = automovel.Ano,
                AnoModelo = automovel.AnoModelo,
                Quilometragem = automovel.Quilometragem,
                Preco = automovel.Preco,
                Blindado = automovel.Blindado,
                QuantidadeDonos = automovel.QuantidadeDonos
            };
        }
    }
}