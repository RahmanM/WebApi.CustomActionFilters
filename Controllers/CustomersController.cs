using System.Web.Http;
using Web.API.Filters.Models;

namespace Web.API.Filters.Controllers
{
    public class CustomersController : ApiController
    {
        public IHttpActionResult Post(Customer customer)
        {
            // do stuff aftre validatins
            return Ok(customer);
        }
    }
}
