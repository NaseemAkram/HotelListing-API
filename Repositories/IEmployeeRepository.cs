using DataAccessLayer;

namespace HotelListing.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> SearchEmployee(string name);

        Task<IEnumerable<Employee>> GetEmployees();

        Task<Employee> GetEmployee(int id);
        Task<Employee> AddEmployee(Employee employee);

        Task<Employee> UpdateEmployee(Employee employee);

        Task<Employee> DeleteEmployee(int id);


    }
}
