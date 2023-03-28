using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using netcorepracticeSession.Data;

namespace netcorepracticeSession.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApiDbContext _dbContext = new ApiDbContext();

        [HttpGet]
       [Authorize]
        public IActionResult Get()
        {
           return Ok(_dbContext.categories);
        }
    }
}
