using System.ComponentModel.DataAnnotations;

namespace TestApp.DTOs.Employee
{
    public class GetEmployeeListRequest
    {
        [Required]
        public string Search { get; set; }
    }
}
