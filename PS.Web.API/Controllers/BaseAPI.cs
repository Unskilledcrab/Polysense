using Microsoft.AspNetCore.Mvc;
using PS.Web.API.Data;

namespace PS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseAPI : ControllerBase
    {
        protected readonly PolysenseContext _context;

        public BaseAPI(PolysenseContext context)
        {
            _context = context;
        }
    }
}
