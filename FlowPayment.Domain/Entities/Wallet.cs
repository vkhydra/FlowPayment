using System;

namespace FlowPayment.Domain.Entities
{
    public class Wallet
    {
        protected Wallet() { }

        public Wallet(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CurrentBalance = 0;
            UpdatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public decimal CurrentBalance { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor do depósito deve ser maior que zero.");

            CurrentBalance += amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Debit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor do débito deve ser maior que zero.");

            if (CurrentBalance < amount)
                throw new InvalidOperationException("Saldo insuficiente.");

            CurrentBalance -= amount;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}