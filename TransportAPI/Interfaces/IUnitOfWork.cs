using TransportAPI.Data;
using TransportAPI.Models;

namespace TransportAPI.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        TransportDBContext Context { get; }
        IRepository<Servico> ServicoRepository { get; }
        IRepository<Transporte> TransporteRepository { get; }
        Task<int> CommitAsync();
    }
}
