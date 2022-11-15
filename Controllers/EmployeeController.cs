using DataAccessLayer;
using HotelListing.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employee;
        public EmployeeController(IEmployeeRepository employee)
        {
            _employee = employee;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _employee.GetEmployees());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data From Database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var result = await _employee.GetEmployee(id);
            try
            {
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return result;
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data From Database");

            }

        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                else
                {
                    var createemp = await _employee.AddEmployee(employee);
                    return CreatedAtAction(nameof(GetEmployee), new { id = createemp.Id }, createemp);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data From Database");

            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest("Id miss match");
                }
                else
                {
                    var employeeupdate = await _employee.GetEmployee(id);
                    if (employeeupdate == null)
                    {
                        return NotFound($"Employee Id ={id} not found");
                    }
                    else
                    {
                        return await _employee.UpdateEmployee(employee);
                    }

                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data From Database");
            }
        }


        //delete employee
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employeeDelete = await _employee.GetEmployee(id);
                if (employeeDelete == null)
                {
                    return NotFound($"Employee Id{id} not found");
                }
                else
                {
                    return await _employee.DeleteEmployee(id);
                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data From Database");
            }
        }
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name)
        {
            try
            {
                var result = await _employee.SearchEmployee(name);

                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retriving Data From Database");
            }
        }
    }



}
