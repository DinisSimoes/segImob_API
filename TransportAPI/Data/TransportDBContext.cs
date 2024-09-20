using Microsoft.EntityFrameworkCore;
using TransportAPI.Models;

namespace TransportAPI.Data
{
    public class TransportDBContext : DbContext
    {
        public DbSet<Transporte> Transportes { get; set; }
        public DbSet<Servico> Servicos { get; set; }

        public TransportDBContext(DbContextOptions<TransportDBContext> options) : base(options) { }

    }
}
