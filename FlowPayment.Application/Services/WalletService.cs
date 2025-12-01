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
                throw new Exception("Carteira não encontrada");
            }
            
            wallet.Deposit(dto.Amount);
            
            await _walletRepository.UpdateAsync(wallet);
            
            return new WalletResponseDto(wallet.Id, wallet.CurrentBalance, wallet.UserId);
        }

        public async Task<WalletResponseDto> TransferAsync(TransferDto dto)
        {
            await _walletRepository.BeginTransactionAsync();

            try
            {
                var sender = await _walletRepository.GetByIdAsync(dto.SenderId);
                var receiver = await _walletRepository.GetByIdAsync(dto.ReceiverId);
                
                if (sender == null) throw new Exception("Remetente não encontrado.");
                if (receiver == null) throw new Exception("Destinário não encontrado.");
                if (sender.Id == receiver.Id)  throw new Exception("Não pode transferir para si mesmo.");
                
                sender.Debit(dto.Amount);
                receiver.Deposit(dto.Amount);
                
                await _walletRepository.UpdateAsync(sender);
                await _walletRepository.UpdateAsync(receiver);

                await _walletRepository.CommitTransactionAsync();
                
                return new WalletResponseDto(sender.Id, sender.CurrentBalance, sender.UserId);
            }
            catch (Exception e)
            {
                await _walletRepository.RollbackTransactionAsync();
                throw;
            }
        }
    }
}