using System.ComponentModel.DataAnnotations;

namespace TestApp.DTOs.Employee
{
    public class AddEmployeeRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string Position { get; set; }
    }
}
