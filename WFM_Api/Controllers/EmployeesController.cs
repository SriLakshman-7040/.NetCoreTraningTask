using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WFM.Domain_DbFrstApprh.Models;
using WFM_Api.API_Context;

namespace WFM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly WFM_API_Context _API_Context;
        public EmployeesController(WFM_API_Context _Context)
        {
            _API_Context = _Context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee_DBF>>> GetAllEmployeeDetails()
        {
            try
            {
                if (_API_Context.Employees == null)
                {
                    return NotFound();
                }
                return await _API_Context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        // Get Employee Detail Based on Given Employee ID - Using Parameter
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<Employee_DBF>> GetEmployeeById(int employeeId)
        {
            if(employeeId == 0 || employeeId < 0)
            {
                return BadRequest("User Input Not-Valid");
            }
            try 
            {
                var emplyeeDetail = await _API_Context.Employees.FindAsync(employeeId);
                if(emplyeeDetail is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { responseMessage = "Given Employee Id Details Not Found" });
                }
                return emplyeeDetail;
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { responseMessage = ex.Message });
            }
        }

        // Get Employee Detail Based on Given Employee Id - Using Query Parameter
        [HttpGet("GetEmployeeDetailById")]
        public async Task<ActionResult<Employee_DBF>> GetEmployeeDetailById([FromQuery] int employeeId)
        {
            if (employeeId == 0 || employeeId < 0)
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Given Employee Id Not Valid" });
            try
            {
                var emplyeeDetail = await _API_Context.Employees.FindAsync(employeeId);
                if (emplyeeDetail is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { responseMessage = "Given Employee Id Details Not Found" });
                }
                return emplyeeDetail;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { responseMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee_DBF>> AddNewEmployee(Employee_DBF employee)
        {
            if (_API_Context.Employees == null)
            {
                return Problem("Entity set 'Api_DbContext.Employees'  is null.");
            }
            if (employee == null || employee.Name == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Employee some details are missing,Please check it. " });
            }
            try 
            {
                _API_Context.Employees.Add(employee);
                await _API_Context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, new { responseMessage = "New Employee Details Save Successfully ." });
            }
            catch(DbUpdateException)
            {
                if (IfEmployeeExits(employee.EmployeeId))
                    return Conflict();
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, new { responseMessage = "Server Side Error" });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Employee_DBF>> UpdateEmployeeDetails(Employee_DBF employee)
        {
            if (!IfEmployeeExits(employee.EmployeeId))
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Given Employee Id Not Exits , Please Check it Once." });
            else
                _API_Context.Entry(employee).State = EntityState.Modified;
            try
            {
                await _API_Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Employee_DBF>> DeleteEmployeeDetails(int employeeId)
        {
            if (_API_Context.Employees == null)
                return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Bad Request "});
            try 
            {
                if (employeeId < 0 || employeeId == 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { responseMessage = "Given Input Not Valid Format, Zero or Neative values we don't prcess " });
                }
                else
                {
                    var employeeDetail = await _API_Context.Employees.FindAsync(employeeId);
                    if (employeeDetail is null)
                        return StatusCode(StatusCodes.Status204NoContent, new { responseMessage = "Given Employee Id is not valid" });
                    else
                        _API_Context.Employees.Remove(employeeDetail);
                    await _API_Context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status200OK, new { responseMessage = "Given Employee Id Details Deleted Successfully" });
                }                
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { responseMessage = ex.Message });
            }            
        }

        private bool IfEmployeeExits(int EmployeeId)
        {
            return (_API_Context.Employees?.Any(e => e.EmployeeId == EmployeeId)).GetValueOrDefault();
        }
    }
}
