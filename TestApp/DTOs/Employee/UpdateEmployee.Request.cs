using System.ComponentModel.DataAnnotations;

namespace TestApp.DTOs.Employee
{
    public class UpdateEmployeeRequest
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string Position { get; set; }
    }
}
