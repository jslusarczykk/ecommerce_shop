using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Exceptions
{
    public class CardNumberTooShortException : Exception
    {
        public virtual int StatusCode => 400;

        public CardNumberTooShortException()
            : base("Card number is too short.")
        {
        }
    }
}
