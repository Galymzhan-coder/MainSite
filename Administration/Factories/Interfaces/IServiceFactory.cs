using Models.DTO.Interfaces;
using Services.FND.Interfaces;

namespace Administration.Factories.Interfaces
{
    public interface IServiceFactory
    {
        IDBService<T> Create<T>() where T : IDto;
    }
}
