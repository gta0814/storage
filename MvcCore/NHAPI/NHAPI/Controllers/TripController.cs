using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHAPI.Controllers
{
    public class CreateTripRequestModel
    {
        public string Driver
    }
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetList()
        {
            //Load from db
            return new List<string> { "Trip1", "Trip1", "Trip1", "Trip2" };
        }
        
        [HttpPost]
        public ActionResult CreateTrip()
        {
            
        }
    }
}
