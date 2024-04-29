using Models.DTO;
using Models.DTO.Interfaces;
using Services.Factory.Interfaces;
using Services.FND.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Factory
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, (string, Type, Type)> _serviceTypes = new Dictionary<string, (string, Type, Type)>
        {
            { "category", ("categories", typeof(ICategoriesService), typeof(CategoryDTO)) }
        };

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public dynamic GetService(string type)
        {
            if (_serviceTypes.TryGetValue(type.ToLower(), out var serviceInfo))
            {
                var service = _serviceProvider.GetService(serviceInfo.Item2);
                if (service != null)
                {
                    return (dynamic)service;
                }
            }
            return null;
        }

        public Type GetInterface(string type)
        {
           return _serviceTypes[type].Item2;
        }

        public Type GetClass(string type)
        {
            return _serviceTypes[type].Item3;
        }
    }
}
