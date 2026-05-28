using ConcessionariaAPI.Entity;
using ConcessionariaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutomovelController : ControllerBase
    {
        private readonly IAutomovelService _service;

        public AutomovelController(AutomovelService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var automoveis = _service.Listar();
            return Ok(automoveis);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var automovel = _service.ObterPorId(id);
            if (automovel == null)
            {
                return NotFound();
            }
            return Ok(automovel);
        }

        [HttpPost]
        public IActionResult Inserir([FromBody] Automovel automovel)
        {
            _service.Inserir(automovel);
            return CreatedAtAction(nameof(ObterPorId), new { id = automovel.Id }, automovel);
        }

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, [FromBody] Automovel automovel)
        {
            if (id != automovel.Id)
            {
                return BadRequest("O ID da URL não corresponde ao ID do objeto.");
            }

            var existe = _service.ObterPorId(id);
            if (existe == null)
            {
                return NotFound();
            }

            _service.Alterar(automovel);
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
    }
}