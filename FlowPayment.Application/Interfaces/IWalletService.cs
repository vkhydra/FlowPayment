using System.Threading.Tasks;
using FlowPayment.Application.DTOs;

namespace FlowPayment.Application.Interfaces
{
    public interface IWalletService
    {
        Task<WalletResponseDto> CreateWalletAsync(CreateWalletDto dto);
    }
}