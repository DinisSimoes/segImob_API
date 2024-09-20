using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportAPI.Models;

namespace TransportAPI_Tests.Models
{
    public class ServicoTests
    {
        [Fact]
        public void CalcularVolume_DeveRetornarVolumeCorreto()
        {
            var servico = new Servico(null)
            {
                Altura = 2,
                Largura = 3,
                Comprimento = 4
            };

            var volume = servico.CalcularVolume();

            Assert.Equal(24, volume);
        }

        [Fact]
        public void CalcularCusto_DeveRetornarCustoCorreto()
        {
            var mockTransporte = new Transporte { Id = 1, Nome = "Teste", CustoPorMetroCubico = 10 };

            var servico = new Servico(mockTransporte)
            {
                Altura = 2,
                Largura = 3,
                Comprimento = 4
            };

            var custo = servico.CalcularCusto();

            Assert.Equal(240, custo);
        }

        [Fact]
        public void CalcularCusto_TransporteNaoDefinido_DeveLancarException()
        {
            var servico = new Servico(null)
            {
                Altura = 2,
                Largura = 3,
                Comprimento = 4
            };

            var exception = Assert.Throws<NullReferenceException>(() => servico.CalcularCusto());
            Assert.Equal("Transporte não está definido", exception.Message);
        }
    }
}
