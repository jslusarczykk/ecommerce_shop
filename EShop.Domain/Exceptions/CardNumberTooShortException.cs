using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.EXCEPTIONS
{
    public class CardNumberTooShortException : Exception
    {
        public virtual int StatusCode => 400;

        // Default constructor with predefined message
        public CardNumberTooShortException()
            : base("Card number is too short.")
        {
        }

        // Constructor with custom message
        public CardNumberTooShortException(string message)
            : base(message)
        {
        }

        // Constructor with custom message and inner exception
        public CardNumberTooShortException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
