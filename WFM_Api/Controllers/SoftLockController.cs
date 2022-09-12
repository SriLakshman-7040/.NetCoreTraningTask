using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WFM.Domain_DbFrstApprh.Models;
using WFM_Api.API_Context;

namespace WFM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftLockController : ControllerBase
    {
        private readonly WFM_API_Context _context;
        public SoftLockController(WFM_API_Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SoftLock_DBF>>> GetAllDetails()
        {
            try
            {
                if (_context.Softlock == null)
                    return NotFound();
                return await _context.Softlock.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{employeeId}")]
        public async Task<ActionResult<SoftLock_DBF>> GetEmployeeById(int employeeId)
        {
            if (employeeId == 0 || employeeId < 0)
                return StatusCode(StatusCodes.Status400BadRequest, new { responeMessage = "Employee ID Zero Not Vaild" });
            try
            {
                var employeeDetail = await _context.Softlock.FindAsync(employeeId);
                if (employeeDetail is null)
                    return StatusCode(StatusCodes.Status404NotFound, new { responeMessage = "Given Employee Details Not Available" });
                return employeeDetail;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { responseMessage = ex.Message });
            }
        }
        [HttpGet("GetEmployeeDetailById")]
        public async Task<ActionResult<SoftLock_DBF>> GetEmployeeByName([FromQuery] int employeeId)
        {
            if (employeeId == 0 || employeeId < 0)
                return StatusCode(StatusCodes.Status400BadRequest, new { responeMessage = "Employee ID Zero Not Vaild" });
            try
            {
                var employeeDetail = await _context.Softlock.FindAsync(employeeId);
                if (employeeDetail is null)
                    return StatusCode(StatusCodes.Status404NotFound, new { responeMessage = "Given Employee Details Not Available" });
                return employeeDetail;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { responseMessage = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult<SoftLock_DBF>> AddNewEmployeeDetail(SoftLock_DBF employee)
        {
            if(_context.Softlock is null)
                return Problem("Entity set '_context.Softlock'  is null.");
            if(employee is null || employee.EmployeeId == 0)
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Employee Id Or Some details are missing,Please check it. " });
            try
            {
                _context.Softlock.Add(employee);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, new { responseMessage = "New Employee Details Save Successfully in softlock table." });
            }
            catch(DbUpdateException)
            {
                if (IfEmployeeExits(employee.EmployeeId))
                    return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Given Employee Id Already Exits, Please Check it." });
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, new { responseMessage = "Server Side Error" });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { responseMessage = ex.Message });
            }
        }
        [HttpPut]
        public async Task<ActionResult<SoftLock_DBF>> UpdateEmployeeDetails(SoftLock_DBF employee)
        {
            if (employee is null || employee.EmployeeId == 0)
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Given Input Is Not Vaild , Please Check it" });
            else if (!IfEmployeeExits(employee.EmployeeId))
                return StatusCode(StatusCodes.Status404NotFound, new { responseMessage = "Given Employee Detail not available" });
            else
                _context.Entry(employee).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, new { responseMessage = "Given Employee Details Updated Successfully" });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{employeeId}")]
        public async Task<ActionResult<SoftLock_DBF>> DeleteEmployeeDetail(int employeeId)
        {
            if (_context.Softlock is null)
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "SoftLock Table is Empty" });
            try
            {
                if (employeeId < 0 || employeeId == 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Given Input is Wrong format , We don't process Zero or Negative Params" });
                }
                else
                {
                    var employeeDetail = await _context.Softlock.FindAsync(employeeId);
                    if (employeeDetail is null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, new { responseMessage = "Given Employee Id is not valid" });
                    }
                    _context.Softlock.Remove(employeeDetail);
                    await _context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status200OK, new { responseMessage = "Given Employee Id Details Deleted Successfully..!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { responseMessage = ex.Message });
            }
        }

        private bool IfEmployeeExits(int? employeeId)
        {
            return (_context.Softlock?.Any(e => e.EmployeeId == employeeId)).GetValueOrDefault();
        }
    }
}
