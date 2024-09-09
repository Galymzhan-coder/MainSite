
using Asp.Versioning;
using LoggingService;
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
        private readonly ILogService _logService;
        private readonly IServiceFactory _serviceFactory;

        public AdminController(IServiceFactory serviceFactory, ILogService logService)
        {
            _serviceFactory = serviceFactory;
            _logService = logService;
        }

        [HttpGet("GetRequestTypes"), ApiVersion("1")]
        public ActionResult GetRequestTypes() 
        { 
            try
            {
                return Ok(_serviceFactory.GetServiceTypes());
            }catch (Exception ex)
            {
                _logService.LogError($"AdminController.GetRequestTypes() :{ex.Message}");

                return StatusCode(500, $"Internal Server Error! - {ex.Message}");
            }
        }

        [HttpGet("Index"), ApiVersion("1")]
        public IActionResult Index(string type, int lang_id=1)
        {
            try
            {
                var service = _serviceFactory.GetService(type);

                if (service == null)
                    return NotFound($"Service for type '{type}' not found.");

                
                var lst = service.Index(lang_id);
                return Ok(lst);
            }
            catch (Exception ex)
            {
                _logService.LogInfo($"AdminController.Index() :{ex.Message}");

                return StatusCode(500, "Internal Server Error!");
            }



        }

        [HttpGet("IndexHierarchySorted"), ApiVersion("1")]
        public IActionResult IndexHierarchySorted(string type, string indent_symbol, string indent_pre_symbol, int lang_id = 1)
        {
            try
            {
                var service = _serviceFactory.GetService(type);

                if (service == null)
                    return NotFound($"Service for type '{type}' not found.");


                var lst = service.IndexHierarchySorted(indent_symbol, indent_pre_symbol, lang_id);
                return Ok(lst);
            }
            catch (Exception ex)
            {
                _logService.LogInfo($"AdminController.Index() :{ex.Message}");

                return StatusCode(500, "Internal Server Error!");
            }



        }

        [HttpGet("IndexPaginated"), ApiVersion("1")]
        public IActionResult Index(string type, int page_num = 1,int page_size = 20, int lang_id = 1, string filter = "")
        {
            try
            {
                var service = _serviceFactory.GetService(type);

                if (service == null)
                    return NotFound($"Service for type '{type}' not found.");


                var lst = service.IndexPaginated(page_num, page_size, lang_id, filter);
                return Ok(lst);
            }
            catch (Exception ex)
            {
                _logService.LogInfo($"AdminController.Index() :{ex.Message}");

                return StatusCode(500, "Internal Server Error!");
            }



        }

        [HttpGet("GetIerarchyList"), ApiVersion("1")]
        public IActionResult GetIerarchyList(string type)
        {
            try
            {
                var service = _serviceFactory.GetService(type);

                if (service == null)
                    return NotFound($"GetIerarchyList. Service for type '{type}' not found.");

                var lst = service.getHierarchyLst();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                _logService.LogInfo($"AdminController.GetIerarchyList() :{ex.Message}");
                return StatusCode(500, "Internal Server Error!");
            }

        }

        [HttpGet("GetItem"), ApiVersion("1")]
        public IActionResult GetItem(string type, int id, int lang_id = 1)
        {
            try
            {
                var service = _serviceFactory.GetService(type);

                if (service == null)
                    return NotFound($"GetItem. Service for type '{type}' not found.");

                var item = service.getItem(id, lang_id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logService.LogInfo($"AdminController.GetItem() :{ex.Message}");

                return StatusCode(500, "Internal Server Error! ");
            }
        }

        [HttpPost("Update"), ApiVersion("1")]
        public IActionResult Update(string type, int id, [FromBody] dynamic json,  int lang_id=1)
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
                var dto = JsonConvert.DeserializeObject(json.ToString(), _serviceFactory.GetClass(type), settings);

                service.update(id, dto, lang_id);


            }
            catch (JsonException je)
            {
                _logService.LogInfo($"AdminController.Update() JsonException: {je.Message}");

                return BadRequest($"Update. JSON parsing error: {je.Message}");
            }
            catch (Exception ex)
            {
                _logService.LogInfo($"AdminController.Update() Exception: {ex.Message}");

                return BadRequest($"Update. An error occurred: {ex.Message}");
            }
            return Ok(new { success = true, message = "Successful updated!" });
        }

        [HttpPost("Create"), ApiVersion("1")]
        public IActionResult Create(string type, [FromBody] dynamic json, int lang_id=1) 
        {
            var service = _serviceFactory.GetService(type);

            if (service == null)
                return NotFound($"Create. Service for type '{type}'");

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            try
            {
                var dto = JsonConvert.DeserializeObject(json.ToString(), _serviceFactory.GetClass(type), settings);

                service.create(dto,lang_id);
            }
            catch (JsonException je)
            {
                _logService.LogInfo($"AdminController.Create() JsonException: {je.Message}");

                return BadRequest($"Create. JSON parsing error: {je.Message}");
            }
            catch (Exception ex)
            {
                _logService.LogInfo($"AdminController.Create() Exception: {ex.Message}");

                return BadRequest($"Create. An error occured: {ex.Message}");
            }

            return Ok(new { success = true, message = "Successful created!" });  
        }

        [HttpPost("Delete"), ApiVersion("1")]
        public IActionResult Delete(string type, int id)
        {
            var service = _serviceFactory.GetService(type);

            if (service == null)
                return NotFound($"Delete. Service for type '{type}' ");

            var settings  = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            try
            {
                service.delete(id);
            }
            catch(Exception ex)
            {
                _logService.LogError($"AdminController.Delete() Exception: {ex.Message}");

                return BadRequest($"Delete. An error occured: {ex.Message}");
            }

            return Ok(new { success=true, message="Successful deleted!" });
        }

    }
}
