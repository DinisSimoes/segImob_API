using TransportAPI.Data;
using TransportAPI.Interfaces;
using TransportAPI.Models;

namespace TransportAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransportDBContext _context;
        private Repository<Servico> _servicoRepository;
        private Repository<Transporte> _transporteRepository;

        public UnitOfWork(TransportDBContext context)
        {
            _context = context;
        }

        public TransportDBContext Context => _context;

        public IRepository<Servico> ServicoRepository => _servicoRepository ??= new Repository<Servico>(_context);
        public IRepository<Transporte> TransporteRepository => _transporteRepository ??= new Repository<Transporte>(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
