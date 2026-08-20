using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioService _service;

        public FuncionariosController(IFuncionarioService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna a lista de funcionários.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FuncionarioOutputDto>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        /// <summary>
        /// Retorna um funcionário pelo id.
        /// </summary>
        /// <param name="id">Id do funcionário</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FuncionarioOutputDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _service.GetByIdAsync(id);
                return Ok(item);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Cria um novo funcionário.
        /// </summary>
        /// <param name="dto">Dados do funcionário</param>
        [HttpPost]
        [ProducesResponseType(typeof(FuncionarioOutputDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] FuncionarioInputDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualiza um funcionário existente.
        /// </summary>
        /// <param name="id">Id do funcionário</param>
        /// <param name="dto">Dados atualizados</param>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] FuncionarioInputDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Remove um funcionário pelo id.
        /// </summary>
        /// <param name="id">Id do funcionário</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
