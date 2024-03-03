using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
       
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmpolyee()
        {
            var employees = await _employeeDbContext.Employees.ToListAsync();

            return Ok(employees);


        }

        [HttpPost]

        public async Task<IActionResult> AddEmployee ([FromBody] Employee employee)
        {
            await _employeeDbContext.Employees.AddAsync(employee);
            await _employeeDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSingleEmployee([FromRoute] int id )
        {
              var employee =  await _employeeDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, Employee updateEmployeeReq)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);
            if (employee == null) {
                return NotFound();
            }
            employee.Name = updateEmployeeReq.Name;
            employee.Email = updateEmployeeReq.Email;
            employee.Address = updateEmployeeReq.Address;   
            employee.Salary = updateEmployeeReq.Salary;

            await _employeeDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id}")]
        
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _employeeDbContext.Employees.Remove(employee);
            
            await _employeeDbContext.SaveChangesAsync();

            return Ok(employee);
        }
    }
}
