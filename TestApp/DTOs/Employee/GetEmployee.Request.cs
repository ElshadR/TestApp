using System.ComponentModel.DataAnnotations;

namespace TestApp.DTOs.Employee
{
    public class GetEmployeeRequest
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}
