//using Administration.Factories.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.DTO.Interfaces;
using Newtonsoft.Json;
using Services.Factory.Interfaces;
using Services.FND;
using Services.FND.Interfaces;

namespace Administration.Controllers
{
    public class AdminController : Controller
    {
        private readonly CategoriesService _categoriesService;
        private readonly IDBIerarchyService<IDto> _dbService;
        //private readonly IServiceProvider _serviceProvider;
        private readonly IServiceFactory _serviceFactory;

        private readonly Dictionary<string, (string,Type, Type)> _serviceTypes = new Dictionary<string, (string,Type, Type)>
        {
            { "category", ("categories",typeof(ICategoriesService),typeof(CategoryDTO)) }
        };
        /*
        public AdminController(IServiceProvider serviceProvider, DBService<IDto> categoriesService)
        {
            _serviceProvider = serviceProvider;
            _categoriesService = categoriesService;
        }
        */
        /*
        public AdminController(IServiceProvider serviceProvider, IDBService<IDto> dbService)
        {
            _serviceProvider = serviceProvider;
            _dbService = dbService;
        }
        */
        
        public AdminController(IServiceFactory serviceFactory)
        {        
            _serviceFactory = serviceFactory;
        }
        /*
        public AdminController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }
        */
        /*
        [HttpGet("Index"), ApiVersion("1")]
        public IActionResult Index()
        {
            var lst = _categoriesService.Index();
            return Ok(lst);
        }
        */
        
        [HttpGet("Index"), ApiVersion("1")]
        public IActionResult Index(string type)
        {
            var service = _serviceFactory.GetService(type); 

            if(service == null)
                return NotFound($"Service for type '{type}' not found.");

            
            var lst = service.Index();
                return Ok(lst);
            
            
        }

        [HttpGet("GetIerarchyList"), ApiVersion("1")]
        public IActionResult GetIerarchyList(string type)
        {
            var service = _serviceFactory.GetService(type);

            if (service == null)
                return NotFound($"GetIerarchyList. Service for type '{type}' not found.");

            var lst = service.getHierarchyLst();
            return Ok(lst);            

        }

        [HttpGet("GetItem"), ApiVersion("1")]
        public IActionResult GetItem(string type, int id)
        {
            var service = _serviceFactory.GetService(type);

            if (service == null)
                return NotFound($"GetItem. Service for type '{type}' not found.");

            var item = service.getItem(id);
            return Ok(item);

        }
        
        [HttpPost("Update"), ApiVersion("1")]
        public IActionResult Update(string type, int id, [FromBody] dynamic json)
        {
            var service = _serviceFactory.GetService(type);

            if (service == null)
                return NotFound($"Update. Service for type '{type}' not found.");

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            try
            {
                var dto = JsonConvert.DeserializeObject(json.ToString(), _serviceFactory.GetClass(type) , settings);

                service.update(id, dto);
                
                return Ok();
                
            }
            catch (JsonException je)
            {
                return BadRequest($"Update. JSON parsing error: {je.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Update. An error occurred: {ex.Message}");
            }
        }

        /*
        [HttpGet("Index"), ApiVersion("1")]
        public IActionResult Index(string type)
        {
            if (_serviceTypes.TryGetValue(type.ToLower(), out var serviceInfo))
            {
                //var service = _serviceProvider.GetService(serviceInfo.Item2) as IDBService<IDto>;
                //var service = (IDBService<IDto>)_serviceProvider.GetService(serviceInfo.Item2);
                //var service = _serviceProvider.GetService(serviceInfo.Item2) as IDBService<IDto>;
                //var service = (dynamic)_serviceProvider.GetService(serviceInfo.Item2);
                var service = (dynamic)_serviceProvider.GetService(serviceInfo.Item2);
                
                if (service != null)
                {
                    var lst = service.Index();
                    return Ok(lst);
                }

                return NotFound($"Service for type '{type}' not found.");
            }
            return BadRequest($"Incorrect service type: {type}.");
        }
        */
        /*
        [HttpGet("GetIerarchyList"), ApiVersion("1")]
        public IActionResult GetIerarchyList(string type)
        {

            if (_serviceProvider != null && _serviceTypes.TryGetValue(type.ToLower(), out var serviceInfo))
            {


                //var service = _serviceProvider.GetService(serviceInfo.Item2) as IDBService<IDto>;
                var service = (dynamic)_serviceProvider.GetService(serviceInfo.Item2);
                if (service != null)
                {
                    var lst = service.getHierarchyLst();
                    return Ok(lst);
                }
                return NotFound($"Service for type '{type}' not found.");
            }

            return BadRequest("GetCategoryIerarchyList: Некорректный тип сервиса: ICategoriesService");

        }

        [HttpGet("GetItem"), ApiVersion("1")]
        public IActionResult GetItem(string type, int id)
        {

            if (_serviceProvider != null && _serviceTypes.TryGetValue(type.ToLower(), out var serviceInfo))
            {

                var service = (dynamic)_serviceProvider.GetService(serviceInfo.Item2);
                if (service != null)
                {
                    var item = service.getItem(id);
                    return Ok(item);
                }
                return NotFound($"Service for type '{type}' not found.");
            }

            return BadRequest("GetItem: Некорректный тип сервиса: IDBService");

        }
        */
        /*
        [HttpPost("Update"), ApiVersion("1")]
        public IActionResult Update(string type, int id, [FromBody] dynamic json)
        {
            if (_serviceTypes.TryGetValue(type.ToLower(), out var serviceDetails))
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                };

                try
                {
                    var dto = JsonConvert.DeserializeObject(json.ToString(), serviceDetails.Item3, settings);
                    var service = (dynamic)_serviceProvider.GetService(serviceDetails.Item2);

                    if (service != null)
                    {
                        service.update(id, dto);
                        return Ok();
                    }

                    return NotFound($"Service for type '{type}' not found.");
                }
                catch (JsonException je)
                {
                    return BadRequest($"JSON parsing error: {je.Message}");
                }
                catch (Exception ex)
                {
                    return BadRequest($"An error occurred: {ex.Message}");
                }
            }

            return BadRequest($"Incorrect service type: {type}.");
        }

        /*
                [HttpGet("CategoryIerarchyList"), ApiVersion("1")]
                public IActionResult GetCategoryIerarchyList()
                {

                   var service = _serviceProvider.GetService(typeof(CategoriesService)) as CategoriesService;


                        if (service != null)
                        {
                            var lst = service != null ? service.getDBHierarchyLst getDBHierarchyLst() : null;

                            return Ok(lst);
                        }

                    return BadRequest("GetCategoryIerarchyList: Некорректный тип сервиса: ICategoriesService");

                }

                [HttpGet("GetCategoryItem"), ApiVersion("1")]
                public IActionResult GetCategoryItem(int id)
                {
                    var item = _categoriesService.getCategoryItem(id);

                    return Ok(item);
                }
        */

        /*
        [HttpGet("IndexCommon"), ApiVersion("1")]
        public IActionResult IndexCommon(string type)//CategoriesService
        {
            if (_serviceTypes.TryGetValue(type.ToLower(), out var serviceType))
            {
                var service = _serviceProvider.GetService(serviceType.Item2) as serviceType.item3;//IBaseService<IDto>;


                if (service != null)
                {
                    var lst = service != null ? service.Index() : null;
                    
                    return Ok(lst);
                }

                 return NotFound($"Сервис для типа '{type}' не найден");
            }

            return BadRequest($"Некорректный тип сервиса: {type}");

        }


        [HttpGet("GetItem"), ApiVersion("1")]
        public IActionResult GetItem(string type, int id)
        {
            if (_serviceTypes.TryGetValue(type.ToLower(), out var serviceType))
            {
                var service = _serviceProvider.GetService(serviceType.Item2) as IBaseService<IDto>;

                if (service != null)
                {
                    var item = service != null ? service.getItem(id) : null;

                    return Ok(item);
                }

                return NotFound($"Сервис для типа '{type}' не найден");
            }

            return BadRequest($"Некорректный тип сервиса: {type}");
        }

        [HttpPost("Update"), ApiVersion("1")]
        //public IActionResult Update(string type, int id, [FromBody] IDto dto)
        public IActionResult Update(string type, int id, [FromBody] dynamic json)
        {

            if (_serviceTypes.TryGetValue(type.ToLower(), out var serviceType))
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    // Добавьте другие настройки по необходимости
                };

                string jsonString = Convert.ToString(json);

                IDto dto = JsonConvert.DeserializeObject<CategoryDTO>(jsonString, settings);

                var service = _serviceProvider.GetService(serviceType.Item2) as IBaseService<IDto>;

                if (service != null)
                {
                    service.update(serviceType.Item1,id, dto);
                    return Ok();
                }

                return NotFound($"Сервис для типа '{type}' не найден");
            }

            return BadRequest($"Некорректный тип сервиса: {type}");
        }

        [HttpPost("Update"), ApiVersion("1")]
        public IActionResult UpdateCommon(string type, int id, [FromBody] dynamic json)
        {
            if (_serviceTypes.TryGetValue(type.ToLower(), out var serviceDetails))
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                };

                try
                {
                    var dto = JsonConvert.DeserializeObject(json.ToString(), serviceDetails.Item3, settings);
                    var service = (IBaseService<IDto>)_serviceProvider.GetService(serviceDetails.Item2);

                    if (service != null)
                    {
                        service.update(serviceDetails.Item1, id, (IDto)dto);
                        return Ok();
                    }

                    return NotFound($"Service for type '{type}' not found.");
                }
                catch (JsonException je)
                {
                    return BadRequest($"JSON parsing error: {je.Message}");
                }
                catch (Exception ex)
                {
                    return BadRequest($"An error occurred: {ex.Message}");
                }
            }

            return BadRequest($"Incorrect service type: {type}.");
        }
        */

    }
}
