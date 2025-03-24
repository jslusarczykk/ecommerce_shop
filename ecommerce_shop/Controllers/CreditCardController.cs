using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ecommerce_shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        //protected ICreditCardService _creditCardService;

     //   public CreditCardController(ICreditCardService creditCardService)
      //  {
      //      _creditCardService = creditCardService;
      //  }


         // GET: api/<CreditCardController>
        [HttpGet]
        //dopisac tutaj kod
       // public IActionResult Get(string cardNumber)
      //  {
        //    try
         //   {

          //  }
      //  }

        // GET api/<CreditCardController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CreditCardController>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<CreditCardController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<CreditCardController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
