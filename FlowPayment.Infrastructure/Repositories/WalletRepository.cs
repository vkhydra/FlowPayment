using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlowPayment.Domain.Entities;
using FlowPayment.Domain.Interfaces;
using FlowPayment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace FlowPayment.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction _transaction;

        public WalletRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Wallet wallet)
        {
            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task<Wallet?> GetByIdAsync(Guid id)
        {
            return await _context.Wallets
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
        
        public async Task CommitTransactionAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}