using System.ComponentModel.DataAnnotations;

namespace SampleApp.Models
{
    public class Employee
    {
        [Required]
        [Key]
        public int Emp_Id { get; set; }
        [Required]
        public string Employee_Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Designation { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        [Required]
        public string Emp_Status { get; set; }  
    }
}
