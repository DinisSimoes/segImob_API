using Microsoft.AspNetCore.Mvc;
using Moq;
using TransportAPI.Controllers;
using TransportAPI.Interfaces;
using TransportAPI.Models;

namespace TransportAPI_Tests.Controllers
{
    public class ServicoControllerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ServicoController _controller;

        public ServicoControllerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _controller = new ServicoController(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetServicos_ReturnsOkResult_WithListOfServicos()
        {
            var servicos = new List<Servico>
            {
                new Servico(new Transporte { Id = 1 }) { Id = 1, Origem = "A", Destino = "B" },
                new Servico(new Transporte { Id = 2 }) { Id = 2, Origem = "C", Destino = "D" }
            };

            _mockUnitOfWork.Setup(uow => uow.ServicoRepository.GetAllAsync())
                           .ReturnsAsync(servicos);

            var result = await _controller.GetServicos();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnServicos = Assert.IsAssignableFrom<IEnumerable<Servico>>(okResult.Value);
            Assert.Equal(2, returnServicos.Count());
        }

        [Fact]
        public async Task GetServicoById_ReturnsOkResult_WithServico()
        {
            var servico = new Servico(new Transporte { Id = 1 }) { Id = 1, Origem = "A", Destino = "B" };

            _mockUnitOfWork.Setup(uow => uow.ServicoRepository.GetByIdAsync(1))
                           .ReturnsAsync(servico);

            var result = await _controller.GetServicoById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnServico = Assert.IsType<Servico>(okResult.Value);
            Assert.Equal(1, returnServico.Id);
        }

        [Fact]
        public async Task GetServicoById_ReturnsNotFound_WhenServicoDoesNotExist()
        {
            _mockUnitOfWork.Setup(uow => uow.ServicoRepository.GetByIdAsync(1))
                           .ReturnsAsync((Servico)null);

            var result = await _controller.GetServicoById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddServico_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Error", "Model is invalid");

            var result = await _controller.AddServico(new Servico(new Transporte()));

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateServico_ReturnsOkResult_WithUpdatedServico()
        {
            var existingServico = new Servico(new Transporte { Id = 1 })
            {
                Id = 1,
                Origem = "A",
                Destino = "B"
            };
            var updatedServico = new Servico(new Transporte { Id = 1 })
            {
                Origem = "X",
                Destino = "Y",
                Status = "Finalizado"
            };

            _mockUnitOfWork.Setup(uow => uow.ServicoRepository.GetByIdAsync(1))
                           .ReturnsAsync(existingServico);

            _mockUnitOfWork.Setup(uow => uow.ServicoRepository.Update(It.IsAny<Servico>()))
                           .Verifiable();

            // Act
            var result = await _controller.UpdateServico(1, updatedServico);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnServico = Assert.IsType<Servico>(okResult.Value);
            Assert.Equal("Finalizado", returnServico.Status);
        }

        [Fact]
        public async Task UpdateServico_ReturnsNotFound_WhenServicoDoesNotExist()
        {
            var updatedServico = new Servico(new Transporte { Id = 1 }) { Origem = "X", Destino = "Y" };

            _mockUnitOfWork.Setup(uow => uow.ServicoRepository.GetByIdAsync(1))
                           .ReturnsAsync((Servico)null);

            var result = await _controller.UpdateServico(1, updatedServico);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteServico_ReturnsNoContent_WhenServicoIsDeleted()
        {
            var servico = new Servico(new Transporte { Id = 1 }) { Id = 1 };

            _mockUnitOfWork.Setup(uow => uow.ServicoRepository.GetByIdAsync(1))
                           .ReturnsAsync(servico);

            _mockUnitOfWork.Setup(uow => uow.ServicoRepository.Delete(servico))
                           .Verifiable();

            var result = await _controller.DeleteServico(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteServico_ReturnsNotFound_WhenServicoDoesNotExist()
        {
            _mockUnitOfWork.Setup(uow => uow.ServicoRepository.GetByIdAsync(1))
                           .ReturnsAsync((Servico)null);

            var result = await _controller.DeleteServico(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
