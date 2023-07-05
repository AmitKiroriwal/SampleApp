using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SampleApp.Data;

namespace SampleApp.Models
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly AppDbContext appDbContext;

        public EmployeeRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            appDbContext.Add(employee);
            await appDbContext.SaveChangesAsync();
            return employee;

        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            Employee emp = await appDbContext.employees.FindAsync(id);
            if(emp!=null)
            {
                appDbContext.employees.Remove(emp);
                await appDbContext.SaveChangesAsync();

            }
            return emp;
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
            return await appDbContext.employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await appDbContext.employees.FirstOrDefaultAsync(x => x.Emp_Id == id);
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            Employee employee1 = await appDbContext.employees.FindAsync(employee.Emp_Id);
            if(employee1!=null)
            {
                employee1.Employee_Name = employee.Employee_Name;
                employee1.Age = employee.Age;
                employee1.Email = employee.Email;
                employee1.Designation = employee.Designation;
                employee1.Emp_Status = employee.Emp_Status;
                employee1.Gender = employee.Gender;
            }
            await appDbContext.SaveChangesAsync();
            return employee;
        }
    }
}
