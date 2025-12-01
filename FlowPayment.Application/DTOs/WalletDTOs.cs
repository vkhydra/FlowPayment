using System;

namespace FlowPayment.Application.DTOs
{
    public record CreateWalletDto(Guid UserId);

    public record WalletResponseDto(Guid Id, decimal CurrentBalance, Guid UserId);

    public record DepositDto(Guid WalletId, decimal Amount);
}