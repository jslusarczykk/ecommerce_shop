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

        public CardNumberInvalidException()
            : base("Provider invalid")
        {
        }
    }
}
