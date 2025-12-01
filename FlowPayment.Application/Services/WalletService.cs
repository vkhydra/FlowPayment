using System;
using System.Threading.Tasks;
using FlowPayment.Application.DTOs;
using FlowPayment.Application.Interfaces;
using FlowPayment.Domain.Entities;
using FlowPayment.Domain.Interfaces;

namespace FlowPayment.Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<WalletResponseDto> CreateWalletAsync(CreateWalletDto dto)
        {
            var wallet = new Wallet(dto.UserId);
            await _walletRepository.CreateAsync(wallet);
            return new WalletResponseDto(
                wallet.Id, 
                wallet.CurrentBalance, 
                wallet.UserId
            );
        }

        public async Task<WalletResponseDto> DepositAsync(DepositDto dto)
        {
            var wallet = await _walletRepository.GetByIdAsync(dto.WalletId);

            if (wallet == null)
            {
                throw new Exception("Wallet not found");
            }
            
            wallet.Deposit(dto.Amount);
            
            await _walletRepository.UpdateAsync(wallet);
            
            return new WalletResponseDto(wallet.Id, wallet.CurrentBalance, wallet.UserId);
        }
    }
}