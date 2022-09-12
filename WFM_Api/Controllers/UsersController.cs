using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WFM.Domain_DbFrstApprh.Models;
using WFM_Api.API_Context;

namespace WFM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly WFM_API_Context _context;
        public UsersController(WFM_API_Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Users_DBF>>> GetAllUsers()
        {
            try 
            {
                if(_context.Users == null)
                    return NotFound();
                return await _context.Users.ToListAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
        [HttpPost]
        public async Task<ActionResult<Users_DBF>> AddNewUser(Users_DBF userDetail)
        {
            if (userDetail == null)
                return BadRequest();
            try
            {
                if (userDetail.Username == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "UserName is Null , Please Give Proper UserName" });
                else if (IfUserAlreadyExits(userDetail.Username))
                    return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = $"UserName {userDetail.Username} Is Already Exits, Please try Different Name" });
                else
                    _context.Users.Add(userDetail);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, new { responseMessage = "New Employee Added SuccessFully..!" + userDetail });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { responseMessage = ex.Message });
            }
        }

        private bool IfUserAlreadyExits(string username)
        {            
            return (_context.Users?.Any(e => e.Username == username)).GetValueOrDefault();
        }
    }
}
