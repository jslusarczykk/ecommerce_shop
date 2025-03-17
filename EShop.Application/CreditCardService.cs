using System;
using System.Linq;
using System.Text.RegularExpressions;
using EShop.Domain.Enums;
using EShop.Domain.Exceptions;
using EShop.Domain.EXCEPTIONS;

namespace EShop.Application
{
    public class CreditCardService
    {
        /// <summary>
        /// Sprawdza, czy podany numer karty jest poprawny zgodnie z algorytmem Luhna.
        /// Jeśli numer karty jest błędny, rzuca wyjątek z odpowiednim kodem błędu.
        /// </summary>
        public bool ValidateCard(string cardNumber)
        {
            // 1) Sprawdzenie pustego/nieważnego inputu
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new CardNumberInvalidException("Card number is empty or contains only whitespace.");
            }

            // 2) Usuwamy spacje i myślniki
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            // 3) Numer karty musi składać się wyłącznie z cyfr
            if (!cardNumber.All(char.IsDigit))
            {
                throw new CardNumberInvalidException("Card number contains invalid characters.");
            }

            // 4) Sprawdzenie poprawnej długości karty
            if (cardNumber.Length < 13)
            {
                throw new CardNumberTooShortException($"Card number is too short (length: {cardNumber.Length}).");
            }
            if (cardNumber.Length > 19)
            {
                throw new CardNumberTooLongException($"Card number is too long (length: {cardNumber.Length}).");
            }

            // 5) Walidacja algorytmem Luhna
            if (!IsLuhnValid(cardNumber))
            {
                throw new CardNumberInvalidException("Card number failed the Luhn algorithm check.");
            }

            return true; // Karta jest poprawna
        }

        /// <summary>
        /// Określa typ karty kredytowej na podstawie wyrażeń regularnych.
        /// Jeśli typ karty nie jest obsługiwany, rzuca wyjątek UnsupportedCardProviderException.
        /// </summary>
        public CreditCardProvider GetCardType(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new CardNumberInvalidException("Card number is empty or contains only whitespace.");
            }

            // Usuwamy spacje i myślniki
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            // Visa – zaczyna się od 4, długość od 13 do 19
            if (Regex.IsMatch(cardNumber, @"^4(\d{12}|\d{15}|\d{18})$"))
                return CreditCardProvider.Visa;

            // MasterCard – stare BIN: 51–55, nowe BIN: 2221–2720
            if (Regex.IsMatch(cardNumber, @"^(5[1-5]\d{14}|2(2[2-9][1-9]|2[3-9]\d{2}|[3-6]\d{3}|7([01]\d{2}|20\d))\d{10})$"))
                return CreditCardProvider.MasterCard;

            // American Express – zaczyna się od 34 lub 37 (15 cyfr)
            if (Regex.IsMatch(cardNumber, @"^3[47]\d{13}$"))
                return CreditCardProvider.AmericanExpress;

            // Jeśli karta nie pasuje do żadnej obsługiwanej kategorii
            throw new UnsupportedCardProviderException($"Unsupported card provider for number: {cardNumber}");
        }

        /// <summary>
        /// Implementacja algorytmu Luhna do walidacji numeru karty kredytowej.
        /// </summary>
        private bool IsLuhnValid(string cardNumber)
        {
            int sum = 0;
            bool doubleDigit = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = cardNumber[i] - '0';

                // Co drugą cyfrę (licząc od PRAWEJ) podwajamy
                if (doubleDigit)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }

                sum += digit;
                doubleDigit = !doubleDigit;
            }

            return (sum % 10 == 0);
        }
    }
}
