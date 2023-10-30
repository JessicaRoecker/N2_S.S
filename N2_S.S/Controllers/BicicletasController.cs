using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N2_S.S.Model;
using N2_S.S.Repositorio;

namespace N2_S.S.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BicicletasController : ControllerBase
    {
        private readonly IBicicletaRepository _repository;


        public BicicletasController(IBicicletaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("Buscar")]
        public async Task<ActionResult> Buscar_Todas()
        {
            try
            {
                var bicicletas = await _repository.BuscarTodasBicletasAsync();
                if (bicicletas.Any())
                {
                    return Ok(bicicletas);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Buscar por Marca e Cor")]
        public async Task<ActionResult> Buscar_Marca_Cor(string marca, string cor)
        {
            try
            {
                var bicicleta = await _repository.BuscarBicicletaCorMarcaAsync(marca,cor);
                if (bicicleta != null)
                {
                    return Ok(bicicleta);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Inserir")]
        public async Task<ActionResult> Inserir_Novas([FromBody] BicicletasModel bicicleta)
        {
            try
            {
                var success = await _repository.AdicionarAsync(bicicleta);
                if (success)
                {
                    return CreatedAtAction(nameof(Buscar_Todas), new { cod_bicicleta = bicicleta.Cod_bicicleta }, bicicleta);
                }
                return BadRequest("Erro ao adicionar a bicicleta.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult> Atualizar(int cod_bicicleta, [FromBody] BicicletasModel bicicleta)
        {
            try
            {
                var success = await _repository.AtualizarAsync(bicicleta, cod_bicicleta);
                if (success)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("Deletar")]
        public async Task<ActionResult> Deletar(int cod_bicicleta)
        {
            try
            {
                var success = await _repository.DeletarAsync(cod_bicicleta);
                if (success)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
