using System;
using System.Threading.Tasks;
using FlowPayment.Domain.Entities;

namespace FlowPayment.Domain.Interfaces
{
    public interface IWalletRepository
    {
        Task CreateAsync(Wallet wallet);
        Task<Wallet?> GetByIdAsync(Guid id);
        Task UpdateAsync(Wallet wallet);
    }
}