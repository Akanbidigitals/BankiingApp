using BankingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.DataAcess.DataContext
{
    public class SimpleBankingContext : DbContext
    {
        public SimpleBankingContext(DbContextOptions options):base(options) { }
        public DbSet<SimpleBanking> SimplBankings { get; set; } 
    }
}
