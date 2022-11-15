using DataAccessLayer;
using HotelListing.DataContext;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            var result = await _context.Employees.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (result != null)
            {
                _context.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> SearchEmployee(string name)
        {
            IQueryable<Employee> query = _context.Employees;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));

            }
            return await query.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await _context.Employees.FirstOrDefaultAsync(a => a.Id == employee.Id);
            if (result != null)
            {
                result.Name = employee.Name;
                result.City = employee.City;
                await _context.SaveChangesAsync();
                return result;

            }

            return null;


        }

    }
}
