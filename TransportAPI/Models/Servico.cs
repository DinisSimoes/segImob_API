namespace TransportAPI.Models
{
    public class Servico
    {
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public DateTime DataSaida { get; set; }
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }
        public int TransporteId { get; set; }
        protected Transporte Transporte { get; set; }
        public string Responsavel { get; set; }
        public string Status { get; set; }
        public decimal CustoTotal { get; set; }
        public Servico() { }

        public Servico(Transporte transporte)
        {
            Transporte = transporte;
        }
        public decimal CalcularVolume()
        {
            return Altura * Largura * Comprimento;
        }

        public decimal CalcularCusto()
        {
            if (Transporte == null)
                throw new NullReferenceException("Transporte não está definido");

            return CalcularVolume() * Transporte.CustoPorMetroCubico;
        }

        public void setTransport(Transporte transport)
        {
            this.Transporte = transport;
        }
    }
}
