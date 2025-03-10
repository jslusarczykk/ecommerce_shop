using Xunit;

namespace EShop.Application.Tests
{
    public class CreditCardServiceTests
    {
        private readonly CreditCardService _service;

        public CreditCardServiceTests()
        {
            _service = new CreditCardService();
        }

        // TESTY METODY: ValidateCard
        [Theory]
        // 1) Visa 16 cyfr (poprawny wg Luhna)
        [InlineData("4539 1488 0343 6467", true)]
        // 2) Fa³szywy Luhn
        [InlineData("4539148803436468", false)]
        // 3) Za krótki (12 cyfr)
        [InlineData("123456789012", false)]
        // 4) Za d³ugi (20 cyfr)
        [InlineData("45391488034364671234", false)]
        // 5) MasterCard poprawny
        [InlineData("5298761234567890", false)]
        // 6) Pusty
        [InlineData("", false)]
        // 7) Bia³e znaki
        [InlineData("   ", false)]
        // 8) Z liter¹ (nie tylko cyfry)
        [InlineData("411111111111A", false)]
        // 9) 13 cyfr, realnie poprawne Luhna (np. 4222222222222)
        [InlineData("4222222222222", true)]
        // 10) 19 cyfr, realnie poprawne Luhna (np. 4242424242424242422)
        [InlineData("4242424242424242422", false)]
        public void ValidateCard_ShouldReturnExpectedResult(string input, bool expectedResult)
        {
            // Act
            var result = _service.ValidateCard(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        // TESTY METODY: GetCardType
        [Theory]
        [InlineData("4539 1488 0343 6467", "Visa")]
        [InlineData("5298761234567890", "MasterCard")]
        [InlineData("378282246310005", "American Express")]
        [InlineData("6011111111111117", "Discover")]
        [InlineData("3530111333300000", "JCB")]
        [InlineData("30569309025904", "Diners Club")]
        [InlineData("5000000000000611", "Maestro")]
        [InlineData("999999999999999", "Unknown")]
        public void GetCardType_ShouldReturnExpectedType(string input, string expectedType)
        {
            // Act
            var type = _service.GetCardType(input);

            // Assert
            Assert.Equal(expectedType, type);
        }
    }
}
