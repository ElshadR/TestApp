using Domain.Interfaces;

namespace TestApp.Services
{
    public class BaseService
    {
        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected internal IUnitOfWork UnitOfWork { get; set; }
    }
}
