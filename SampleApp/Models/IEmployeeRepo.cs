namespace SampleApp.Models
{
    public interface IEmployeeRepo
    {
        public Task<List<Employee>> GetAllEmployee();
        public Task<Employee> GetEmployee(int id);
        public Task<Employee> AddEmployee(Employee employee);
        public Task<Employee> UpdateEmployee(Employee employee);
        public Task<Employee> DeleteEmployee(int id);
    }
}
