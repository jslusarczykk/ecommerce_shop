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

        public CardNumberTooLongException()
            : base("Card number is too long.")
        {
        }
    }
}
