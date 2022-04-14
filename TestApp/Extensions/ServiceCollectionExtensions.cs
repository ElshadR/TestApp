using Domain.Employees;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using TestApp.Services.Employees;

namespace TestApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>))
                .AddScoped<IEmployeeRepository, EmployeeRepository>();
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services
            , string conectionString)
        {
            return services.AddDbContext<EFDbContext>(options =>
                     options.UseSqlServer(conectionString,
                     b => b.MigrationsAssembly("TestApp")));
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services
           )
        {
            return services
                .AddScoped<EmployeeService>();
        }
    }
}
