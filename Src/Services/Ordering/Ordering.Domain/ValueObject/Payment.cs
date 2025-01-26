namespace Ordering.Domain.ValueObject
{
    public record Payment
    {
        public string CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string CVV { get; } = default!;
        public string Expiration { get; } = default!;
        public int PaymentMethod { get; }

        protected Payment()
        {
            
        }

        private Payment(string cardName, string cardNumber, string cVV, string expiration, int paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            CVV = cVV;
            Expiration = expiration;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string cardName, string cardNumber, string cVV, string expiration, int paymentMethod)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cVV.Length, 3);
            ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(cVV);

            return new Payment(cardName, cardNumber, cVV, expiration, paymentMethod);
        }
    }
}