using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.EXCEPTIONS
{
    public class CardNumberInvalidException : Exception
    {
        public virtual int StatusCode => 406;

        // Default constructor with default message
        public CardNumberInvalidException()
            : base("Provider invalid")
        {
        }

        // Constructor with custom message
        public CardNumberInvalidException(string message)
            : base(message)
        {
        }

        // Constructor with custom message and inner exception
        public CardNumberInvalidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
