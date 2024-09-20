using Microsoft.AspNetCore.Mvc;
using TransportAPI.Interfaces;
using TransportAPI.Models;

namespace TransportAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransporteController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransporteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetServicos()
        {
            var servicos = await _unitOfWork.TransporteRepository.GetAllAsync();
            return Ok(servicos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransporteById(int id)
        {
            var servico = await _unitOfWork.TransporteRepository.GetByIdAsync(id);
            if (servico == null)
            {
                return NotFound();     
            }

            return Ok(servico);
        }

        [HttpPost]
        public async Task<IActionResult> PostTransport([FromBody] Transporte transport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }


            var transporteExistente = await _unitOfWork.TransporteRepository.GetByIdAsync(transport.Id);

            if (transporteExistente != null)
            {
                return BadRequest("Transporte já existe.");
            }

            _unitOfWork.TransporteRepository.AddAsync(transport);
            await _unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(GetTransporteById), new { id = transport.Id }, transport);
        }
    }
}
