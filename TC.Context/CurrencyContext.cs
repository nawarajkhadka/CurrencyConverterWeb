using Microsoft.EntityFrameworkCore;
using System;
using TC.Model;

namespace TC.Context
{
    public class CurrencyContext : DbContext
    {
        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options)
        {

        }
        public DbSet<CurrencyRates> CurrencyRates { get; set; }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }

}
