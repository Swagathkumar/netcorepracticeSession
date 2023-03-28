using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using netcorepracticeSession.Data;
using netcorepracticeSession.Models;
using System.Security.Claims;

namespace netcorepracticeSession.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        ApiDbContext _dbContext = new ApiDbContext();


        [HttpGet("propertylist")]
        [Authorize]
        public IActionResult GetProperties(int categoryid)
        {
            var propertiesresult = _dbContext.properties.Where(c => c.categoryid == categoryid);
            if(propertiesresult == null)
            {
                return NotFound();
            }
            return Ok(propertiesresult);
        }

        [HttpGet("SearchProperties")]
        [Authorize]
        public IActionResult GetSearchProperties(string address)
        {
            var propertiesresult = _dbContext.properties.Where(p=>p.Address.Contains(address));
            if (propertiesresult == null)
            {
                return NotFound();
            }
            return Ok(propertiesresult);
        }

        [HttpGet("propertyDetail")]
        [Authorize]
        public IActionResult PropertiesDetail(int id)
        {
            var propertiesresult = _dbContext.properties.FirstOrDefault(p => p.Id == id);
            if (propertiesresult == null)
            {
                return NotFound();
            }
            return Ok(propertiesresult);
        }

        [HttpGet("Trendingproperties")]
        [Authorize]
        public IActionResult GetTrendingProperties(int categoryid)
        {
            var propertiesresult = _dbContext.properties.Where(c => c.istrending == true);
            if (propertiesresult == null)
            {
                return NotFound();
            }
            return Ok(propertiesresult);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Property property)
        {
            if (property == null) 
            {
                return NoContent();
            }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbContext.users.FirstOrDefault(u => u.Email == userEmail);
                if (user == null) return NotFound();
                property.istrending = false;
                property.userid = user.Id;
                _dbContext.properties.Add(property);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] Property property)
        {
            var propertyresult = _dbContext.properties.FirstOrDefault(p => p.Id == id);
            if (propertyresult == null)
            {
                return NoContent();
            }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbContext.users.FirstOrDefault(u => u.Email == userEmail);
                if (user == null) return NotFound();
                if (propertyresult.userid == user.Id) 
                {
                    propertyresult.Name = property.Name;
                    propertyresult.Email = property.Email;
                    propertyresult.price = property.price;
                    propertyresult.Address = property.Address;
                    property.istrending = false;
                    property.userid = user.Id;

                    _dbContext.SaveChanges();
                    return Ok("Records updated successfully");
                }
                return BadRequest();
            }
        }

            [HttpDelete("{id}")]
            [Authorize]
            public IActionResult Delete(int id)
            {
                var propertyresult = _dbContext.properties.FirstOrDefault(p => p.Id == id);
                if (propertyresult == null)
                {
                    return NoContent();
                }
                else
                {
                    var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                    var user = _dbContext.users.FirstOrDefault(u => u.Email == userEmail);
                    if (user == null) return NotFound();
                    if (propertyresult.userid == user.Id) 
                    {
                       _dbContext.properties.Remove(propertyresult);

                        _dbContext.SaveChanges();
                        return Ok("Records Deleted successfully");
                    }
                    return BadRequest();
                }
            }




    }
}
