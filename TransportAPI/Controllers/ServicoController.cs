using Microsoft.AspNetCore.Mvc;
using TransportAPI.Interfaces;
using TransportAPI.Models;

namespace TransportAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServicoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetServicos()
        {
            var servicos = await _unitOfWork.ServicoRepository.GetAllAsync();
            return Ok(servicos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServicoById(int id)
        {
            var servico = await _unitOfWork.ServicoRepository.GetByIdAsync(id);
            if (servico == null)
            {
                return NotFound();
            }

            return Ok(servico);
        }

        [HttpPost]
        public async Task<IActionResult> AddServico([FromBody] Servico servico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transporteExistente = await _unitOfWork.TransporteRepository.GetByIdAsync(servico.TransporteId);

            if (transporteExistente == null)
            {
                return BadRequest("Transporte não encontrado. Certifique-se de que o transporte já existe.");
            }

            var service = new Servico(transporteExistente)
            {
                Origem = servico.Origem,
                Destino = servico.Destino,
                DataSaida = servico.DataSaida,
                Altura = servico.Altura,
                Largura = servico.Largura,
                Comprimento = servico.Comprimento,
                TransporteId = servico.TransporteId,
                Responsavel = servico.Responsavel,
                Status = servico.Status,
                CustoTotal = 0
            };

            service.setTransport(transporteExistente);

            service.CustoTotal = service.CalcularCusto();

            await _unitOfWork.ServicoRepository.AddAsync(service);
            await _unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(GetServicoById), new { id = service.Id }, service);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServico(int id, [FromBody] Servico updatedServico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var servico = await _unitOfWork.ServicoRepository.GetByIdAsync(id);
            if (servico == null)
            {
                return NotFound();
            }

            servico.Status = updatedServico.Status;

            _unitOfWork.ServicoRepository.Update(servico);
            await _unitOfWork.CommitAsync();

            return Ok(servico);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServico(int id)
        {
            var servico = await _unitOfWork.ServicoRepository.GetByIdAsync(id);
            if (servico == null)
            {
                return NotFound();
            }

            _unitOfWork.ServicoRepository.Delete(servico);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }
    }
}
