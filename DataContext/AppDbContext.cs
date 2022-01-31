using Domain.Dto.Layer.Request;
using Microsoft.EntityFrameworkCore;
using RetoTecnicoBCP.Domain;

namespace RetoTecnicoBCP.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
            
        }

        public DbSet<ExchangeRate> ExchangeRate { get; set; }
    }
}
