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
    }
}