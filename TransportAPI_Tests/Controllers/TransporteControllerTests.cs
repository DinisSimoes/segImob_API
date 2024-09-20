using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportAPI.Controllers;
using TransportAPI.Interfaces;
using TransportAPI.Models;

namespace TransportAPI_Tests.Controllers
{
    public class TransporteControllerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly TransporteController _controller;

        public TransporteControllerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _controller = new TransporteController(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetTransporteById_TransporteExistente_DeveRetornar200OK()
        {
            // Arrange
            var transporte = new Transporte { Id = 1, Nome= "teste", CustoPorMetroCubico = 10 };
            _mockUnitOfWork.Setup(uow => uow.TransporteRepository.GetByIdAsync(1))
                .ReturnsAsync(transporte);

            // Act
            var result = await _controller.GetTransporteById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(transporte, okResult.Value);
        }

        [Fact]
        public async Task GetTransporteById_TransporteNaoExistente_DeveRetornar404NotFound()
        {
            _mockUnitOfWork.Setup(uow => uow.TransporteRepository.GetByIdAsync(1))
                .ReturnsAsync((Transporte)null);

            var result = await _controller.GetTransporteById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostTransport_DadosInvalidos_DeveRetornar400BadRequest()
        {
            _controller.ModelState.AddModelError("Error", "Model is invalid");
            var transporte = new Transporte { Id = 1, Nome = "teste", CustoPorMetroCubico = 10 };

            var result = await _controller.PostTransport(transporte);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task PostTransport_TransporteJaExistente_DeveRetornar400BadRequest()
        {
            var transporte = new Transporte { Id = 1, Nome = "teste", CustoPorMetroCubico = 10 };
            _mockUnitOfWork.Setup(uow => uow.TransporteRepository.GetByIdAsync(1))
                .ReturnsAsync(transporte);

            var result = await _controller.PostTransport(transporte);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Transporte já existe.", badRequestResult.Value);
        }

        [Fact]
        public async Task PostTransport_TransporteValido_DeveRetornar201Created()
        {
            var transporte = new Transporte { Id = 1, Nome = "teste", CustoPorMetroCubico = 10 };
            _mockUnitOfWork.Setup(uow => uow.TransporteRepository.GetByIdAsync(1))
                .ReturnsAsync((Transporte)null);

            var result = await _controller.PostTransport(transporte);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdAtActionResult.StatusCode);
            Assert.Equal("GetTransporteById", createdAtActionResult.ActionName);
            Assert.Equal(transporte, createdAtActionResult.Value);
        }
    }
}
