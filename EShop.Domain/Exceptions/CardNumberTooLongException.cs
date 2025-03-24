using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.EXCEPTIONS
{
    public class CardNumberTooLongException : Exception
    {
        public virtual int StatusCode => 414;

        // Default constructor with predefined message
        public CardNumberTooLongException()
            : base("Card number is too long.")
        {
        }

        // Constructor with custom message
        public CardNumberTooLongException(string message)
            : base(message)
        {
        }

        // Constructor with custom message and inner exception
        public CardNumberTooLongException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
