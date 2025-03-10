using System.Linq;
using System.Text.RegularExpressions;

namespace EShop.Application
{
    public class CreditCardService
    {
        /// <summary>
        /// Sprawdza, czy podany numer karty jest poprawny zgodnie z algorytmem Luhna.
        /// Dopuszczane są spacje i myślniki w numerze karty (zostają usunięte przed weryfikacją).
        /// </summary>
        public bool ValidateCard(string cardNumber)
        {
            // 1) Sprawdzenie pustego/nieważnego inputu
            if (string.IsNullOrWhiteSpace(cardNumber))
                return false;

            // 2) Usuwamy spacje i myślniki
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            // 3) Numer karty musi składać się wyłącznie z cyfr
            if (!cardNumber.All(char.IsDigit))
                return false;

            // *** LINIE PONIŻEJ DODAJĄ WALIDACJĘ DŁUGOŚCI 13–19 ***
            if (cardNumber.Length < 13 || cardNumber.Length > 19)
                return false;
            // *** KONIEC DODANEGO KODU ***

            int sum = 0;
            bool doubleDigit = false;  // false oznacza: "nie podwajaj pierwszej (prawej) cyfry"

            // 4) Iterujemy od PRAWEJ do LEWEJ
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

            // 5) Suma musi być podzielna przez 10
            return (sum % 10 == 0);
        }



        /// <summary>
        /// Określa typ karty kredytowej na podstawie wyrażeń regularnych.
        /// </summary>
        public string GetCardType(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return "Unknown";

            // Usuwamy spacje i myślniki
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            // Visa – zaczyna się od 4, długość od 13 do 19
            if (Regex.IsMatch(cardNumber, @"^4(\d{12}|\d{15}|\d{18})$"))
                return "Visa";

            // MasterCard – stare BIN: 51–55, nowe BIN: 2221–2720
            if (Regex.IsMatch(cardNumber, @"^(5[1-5]\d{14}|2(2[2-9][1-9]|2[3-9]\d{2}|[3-6]\d{3}|7([01]\d{2}|20\d))\d{10})$"))
                return "MasterCard";

            // American Express – zaczyna się od 34 lub 37 (15 cyfr)
            if (Regex.IsMatch(cardNumber, @"^3[47]\d{13}$"))
                return "American Express";

            // Discover – 6011..., 622..., 64..., 65...
            if (Regex.IsMatch(cardNumber, @"^(6011\d{12}|65\d{14}|64[4-9]\d{13}|622(1[2-9][6-9]|[2-8]\d{2}|9([01]\d|2[0-5]))\d{10})$"))
                return "Discover";

            // JCB
            if (Regex.IsMatch(cardNumber, @"^(352[89]|35[3-8]\d)\d{12}$"))
                return "JCB";

            // Diners Club
            if (Regex.IsMatch(cardNumber, @"^3(0[0-5]|[68]\d)\d{11}$"))
                return "Diners Club";

            // Maestro
            if (Regex.IsMatch(cardNumber, @"^(50|5[6-9]|6\d)\d{10,17}$"))
                return "Maestro";

            // W innym przypadku nie rozpoznano typu
            return "Unknown";
        }
    }
}
