using Domain.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>
        , IEmployeeRepository
    {
        public EmployeeRepository(EFDbContext dbContext) : base(dbContext)
        {
        }
    }
}
