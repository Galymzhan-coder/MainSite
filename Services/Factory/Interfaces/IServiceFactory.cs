using Models.DTO.Interfaces;
using Services.FND.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Factory.Interfaces
{
    public interface IServiceFactory
    {
        dynamic GetService(string type);

        public Type GetInterface(string type);

        public Type GetClass(string type);

        public List<string> GetServiceTypes();
    }
}
