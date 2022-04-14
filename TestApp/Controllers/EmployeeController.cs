using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp.Attributes;
using TestApp.DTOs.Employee;
using TestApp.Services.Employees;

namespace TestApp.Controllers
{
    [ApiController]
    [ValidateModel]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService employeeService)
        {
            _service = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetEmployeeRequest request)
        {
            try
            {
                var response = await _service.GetAsync(request);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetList([FromQuery] GetEmployeeListRequest request)
        {
            try
            {
                var response = await _service.GetListAsync(request);
                if (response == null || !response.Any())
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddEmployeeRequest request)
        {
            try
            {
                var exist = await _service.GetExistAsync(request.Name, request.Surname, request.FatherName);
                if (exist)
                {
                    ModelState.AddModelError("Exist", "Employee already in use");
                    return BadRequest(ModelState);
                }

                var response = await _service.AddNewAsync(request);
                return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeRequest request)
        {
            try
            {
                var employee = await _service.GetAsync(new GetEmployeeRequest { Id = request.Id });
                if (employee == null)
                {
                    return NotFound($"Employee with Id = {request.Id} not found");
                }

                var response = await _service.UpdateAsync(request);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteEmployeeRequest request)
        {
            try
            {
                var employee = await _service.GetAsync(new GetEmployeeRequest { Id = request.Id });
                if (employee == null)
                {
                    return NotFound($"Employee with Id = {request.Id} not found");
                }

                var response = await _service.DeleteAsync(request);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
