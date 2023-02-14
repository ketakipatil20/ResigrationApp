using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegisterApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RegisterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly OurExcelDataContext ourExcelDataContext;
        public UserController(OurExcelDataContext ourExcelData)
        {
            ourExcelDataContext = ourExcelData;
        }

       /* [HttpGet("GetUsers")]
        public async Task<IActionResult<IEnumerable<Usertable>>> GetUsers()
        {
            return await ourExcelDataContext.Usertables.ToListAsync();
        }*/


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authentication([FromBody] Usertable usertable)
        {
            if (usertable == null)
                return BadRequest();
            var user = await ourExcelDataContext.Usertables.FirstOrDefaultAsync(x => x.UserName == usertable.UserName && x.Password == usertable.Password);
            if (user == null)
                return NotFound(new { Message = "User Not Found" });
            return Ok(new
            {
                Message = "Login Success!"
            });

        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] Usertable usertable)
        {
            if (usertable == null)
                return BadRequest();
            await ourExcelDataContext.Usertables.AddAsync(usertable);
            await ourExcelDataContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "User Registered!"
            });
        }
    }
}

