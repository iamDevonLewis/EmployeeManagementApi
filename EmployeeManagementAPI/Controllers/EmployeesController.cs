using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public IActionResult GetEmployees()
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            var employees = _context.Employees.Include(x => x.Address).ToList();
            return Ok(employees);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
          
            var employee =_context.Employees.Include(x => x.Address).FirstOrDefault(j => j.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // POST: api/Employees
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }


        // PUT: api/Employees/5

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id,Employee employee)
        {
            var existingEmployee = _context.Employees.Include(e => e.Address).FirstOrDefault(e => e.Id == id);


            if (existingEmployee == null)
            {
                return NotFound();
            }

            // Update the employee properties
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Department = employee.Department;

            //detach address
            //_context.Entry(existingEmployee.Address).State = EntityState.Detached;
            existingEmployee.Address.City = employee.Address.City;
            existingEmployee.Address.State = employee.Address.State;
            existingEmployee.Address.Street = employee.Address.Street;
            existingEmployee.Address.Zipcode = employee.Address.Zipcode;
            // Update other properties as needed
            _context.SaveChanges();

            return Ok(existingEmployee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var employee = _context.Employees.Include(x => x.Address).FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Remove(employee.Address);

            _context.Employees.Remove(employee);
            _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
