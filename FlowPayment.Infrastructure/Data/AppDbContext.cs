using Microsoft.EntityFrameworkCore;
using FlowPayment.Domain.Entities;

namespace FlowPayment.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.CurrentBalance)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });
        }
    }
}