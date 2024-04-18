using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.DTO.Interfaces;
using Newtonsoft.Json;
using Services.FND;
using Services.FND.Interfaces;

namespace Administration.Controllers
{
    public class AdminController : Controller
    {
        private readonly CategoriesService _categoriesService;
        private readonly IServiceProvider _serviceProvider;

        private readonly Dictionary<string, (string,Type)> _serviceTypes = new Dictionary<string, (string,Type)>
        {
            { "category", ("categories",typeof(CategoriesService)) }
        };

        public AdminController(IServiceProvider serviceProvider, CategoriesService categoriesService)
        {
            _serviceProvider = serviceProvider;
            _categoriesService = categoriesService;
        }

        [HttpGet("Index"), ApiVersion("1")]
        public IActionResult Index()
        {
            var lst = _categoriesService.Index();
            return Ok(lst);
        }

        [HttpGet("CategoryIerarchyList"), ApiVersion("1")]
        public IActionResult GetCategoryIerarchyList()
        {

           var service = _serviceProvider.GetService(typeof(CategoriesService)) as CategoriesService;


                if (service != null)
                {
                    var lst = service != null ? service.getCategoryHierarchyLst() : null;
                   
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

        [HttpGet("IndexCommon"), ApiVersion("1")]
        public IActionResult IndexCommon(string type)//CategoriesService
        {
            if (_serviceTypes.TryGetValue(type.ToLower(), out var serviceType))
            {
                var service = _serviceProvider.GetService(serviceType.Item2) as IBaseService;


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
                var service = _serviceProvider.GetService(serviceType.Item2) as IBaseService;

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

                var service = _serviceProvider.GetService(serviceType.Item2) as IBaseService;

                if (service != null)
                {
                    service.update(serviceType.Item1,id, dto);
                    return Ok();
                }

                return NotFound($"Сервис для типа '{type}' не найден");
            }

            return BadRequest($"Некорректный тип сервиса: {type}");
        }
    }
}
