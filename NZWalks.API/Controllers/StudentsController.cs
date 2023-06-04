using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            //return all the student names

            string[] studentNames = new string[] { "Soujum", "Chithkala", "Prabhakar" };

            return Ok(studentNames);
        }
    }
}
