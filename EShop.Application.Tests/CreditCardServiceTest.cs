using Xunit;
using EShop.Application;
using EShop.Domain.Enums;
using EShop.Domain.EXCEPTIONS;

namespace EShop.Application.Tests
{
    public class CreditCardServiceTests
    {
        private readonly CreditCardService _service;

        public CreditCardServiceTests()
        {
            _service = new CreditCardService();
        }

        // TESTY METODY: ValidateCard - poprawne przypadki
        [Theory]
        [InlineData("4539148803436467", CreditCardProvider.Visa)]  // Visa poprawny
        [InlineData("5298761234567890", CreditCardProvider.MasterCard)]  // MasterCard poprawny
        [InlineData("378282246310005", CreditCardProvider.AmericanExpress)]  // Amex poprawny
        public void ValidateCard_ShouldReturnValidResult(string input, CreditCardProvider expectedProvider)
        {
            // Act
            var isValid = _service.ValidateCard(input);
            Assert.True(isValid);



            // Assert
            Assert.True(isValid);
        }

        // TESTY METODY: ValidateCard - b��dy (powinny rzuca� wyj�tki)
        [Theory]
        [InlineData("123456789012")]  // Za kr�tki (12 cyfr)
        [InlineData("000000000000")]  // 12 zer - za kr�tki
        [InlineData(" ")]  // Pusty ci�g znak�w
        public void ValidateCard_TooShort_ShouldThrowCardNumberTooShortException(string input)
        {
            // Act & Assert
            Assert.Throws<CardNumberTooShortException>(() => _service.ValidateCard(input));
        }

        [Fact]
        public void ValidateCard_TooLong_ShouldThrowCardNumberTooLongException()
        {
            string tooLongCardNumber = "45391488034364671234"; // 20 cyfr

            // Act & Assert
            Assert.Throws<CardNumberTooLongException>(() => _service.ValidateCard(tooLongCardNumber));
        }

        [Fact]
        public void ValidateCard_InvalidChecksum_ShouldThrowCardNumberInvalidException()
        {
            string invalidCheckSumNumber = "4539148803436468"; // Nie przechodzi Luhna

            // Act & Assert
            Assert.Throws<CardNumberInvalidException>(() => _service.ValidateCard(invalidCheckSumNumber));
        }

        [Fact]
        public void ValidateCard_InvalidCharacters_ShouldThrowCardNumberInvalidException()
        {
            string invalidCardWithLetter = "411111111111A"; // Zawiera liter�

            // Act & Assert
            Assert.Throws<CardNumberInvalidException>(() => _service.ValidateCard(invalidCardWithLetter));
        }

        [Fact]
        public void ValidateCard_UnsupportedProvider_ShouldThrowUnsupportedCardProviderException()
        {
            var input = "9999999999999999"; // Example unsupported card number

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _service.ValidateCard(input));
        }

        // TESTY METODY: GetCardType
        [Theory]
        [InlineData("4539148803436467", "Visa")]
        [InlineData("5298761234567890", "MasterCard")]
        [InlineData("378282246310005", "American Express")]
        [InlineData("6011111111111117", "Discover")]  // Nieobs�ugiwany wydawca
        [InlineData("3530111333300000", "JCB")]  // Nieobs�ugiwany wydawca
        [InlineData("30569309025904", "Diners Club")]  // Nieobs�ugiwany wydawca
        [InlineData("5000000000000611", "Maestro")]  // Nieobs�ugiwany wydawca
        [InlineData("999999999999999", "Unknown")]  // Nieznany wydawca
        public void GetCardType_ShouldReturnExpectedType(string input, string expectedType)
        {
            // Act
            var type = _service.GetCardType(input);

            // Assert
            Assert.Equal(expectedType, type.ToString());
        }
    }
}
