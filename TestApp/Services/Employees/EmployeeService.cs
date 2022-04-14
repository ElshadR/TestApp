using Domain.Interfaces;
using TestApp.DTOs.Employee;
using Domain.Employees;
using Infrastructure.Data;

namespace TestApp.Services.Employees
{
    public class EmployeeService : BaseService
    {
        public IAsyncRepository<Employee> _repository { get; set; }
        public EmployeeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

            _repository = UnitOfWork.AsyncRepository<Employee>();
        }
        public async Task<AddEmployeeResponse> AddNewAsync(AddEmployeeRequest model)
        {
            var employee = new Employee
            {
                Name = model.Name,
                Surname = model.Surname,
                FatherName = model.FatherName,
                Position = model.Position,
            };

            await _repository.AddAsync(employee);
            await UnitOfWork.SaveChangesAsync();

            var response = new AddEmployeeResponse()
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                FatherName = employee.FatherName,
                Position = employee.Position,
            };

            return response;
        }
        public async Task<UpdateEmployeeResponse> UpdateAsync(UpdateEmployeeRequest model)
        {
            var employee = await _repository.GetAsync(x => x.Id == model.Id);
            if (Equals(employee, null))
                return null;

            if (!string.IsNullOrEmpty(model.Name))
                employee.Name = model.Name;
            if (!string.IsNullOrEmpty(model.Surname))
                employee.Surname = model.Surname;
            if (!string.IsNullOrEmpty(model.FatherName))
                employee.FatherName = model.FatherName;
            if (!string.IsNullOrEmpty(model.Position))
                employee.Position = model.Position;

            await UnitOfWork.SaveChangesAsync();

            var response = new UpdateEmployeeResponse()
            {
                Id = employee.Id,
                Name = employee.Name,
            };

            return response;
        }
        public async Task<DeleteEmployeeResponse> DeleteAsync(DeleteEmployeeRequest model)
        {
            var employee = await _repository.GetAsync(x => x.Id == model.Id);

            if (Equals(employee, null))
                return null;

            await _repository.DeleteAsync(employee);
            await UnitOfWork.SaveChangesAsync();

            var response = new DeleteEmployeeResponse()
            {
                Id = employee.Id,
                Name = employee.Name,
            };

            return response;
        }
        public async Task<IEnumerable<GetEmployeeListResponse>> GetListAsync(GetEmployeeListRequest model)
        {
            var response = (await _repository
                .ListAsync(x => x.Name.Contains(model.Search)
                || x.Surname.Contains(model.Search)))
                .Select(x => new GetEmployeeListResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    FatherName = x.FatherName,
                    Position = x.Position,
                });

            return response;
        }
        public async Task<GetEmployeeResponse> GetAsync(GetEmployeeRequest model)
        {
            var employee = await _repository.GetAsync(x => x.Id == model.Id);

            if (Equals(employee, null))
                return null;

            var response = new GetEmployeeResponse()
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                FatherName = employee.FatherName,
                Position = employee.Position,
            };

            return response;
        }
        public async Task<bool> GetExistAsync(string name, string surname, string fatherName)
        {
            var employee = await _repository.GetAsync(x => x.Name == name && x.Surname == surname && x.FatherName == fatherName);

            if(Equals(employee, null))
                return false;
            return true;
        }
    }
}
