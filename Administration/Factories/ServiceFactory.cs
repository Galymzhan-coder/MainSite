using Administration.Factories.Interfaces;
using Models.DTO.Interfaces;
using Services.FND.Interfaces;

namespace Administration.Factories
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _provider;

        public ServiceFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IDBService<T> Create<T>() where T : IDto
        {
            return _provider.GetRequiredService<IDBService<T>>();
        }
    }
}
