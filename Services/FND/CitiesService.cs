using Models.DTO;
using Models.DTO.Interfaces;
using Models.DTO.ResultDTOs;
using Services.FND.Interfaces;
using Services.FND.PgBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FND
{
    public class CitiesService : BasePageExtIndexPaginatedService<CitiesDTO>, ICitiesService
    {
        public CitiesService(IServiceProvider serviceProvider):base("cities", serviceProvider) { }
        public override IEnumerable<CitiesDTO> Index(int lang_id)
        {
            try
            {
                var lst = Index("cities c, cities_translations ct", @"c.id, ct.city_id, COALESCE(ct.title , c.title) title, c.is_active, c.latitude, c.longitude, c.phone, c.phone_secondary, c.geoip_city, c.create_date, 
                                c.update_date", $"c.id = ct.city_id and ct.lang_id={lang_id}");
                return lst;
            }
            catch
            {
                throw;
            }
        }

        public override PaginatedResult<CitiesDTO> IndexPaginated(int pageNumber, int pageSize, int lang_id, string whereCond = "")
        {
            try
            {
                var ret = IndexPaginated("cities c, cities_translations ct", @"c.id, ct.city_id, COALESCE(ct.title , c.title) title, c.is_active, c.latitude, c.longitude, c.phone, c.phone_secondary, c.geoip_city, c.create_date, 
                                c.update_date", pageNumber, pageSize, $"c.id = ct.city_id and ct.lang_id={lang_id}", "");

                return ret;
            }
            catch
            {
                throw;
            }
        }

        public override CitiesDTO? getItem(int id, int lang_id)
        {
            try
            {
                var item = getItem("cities c, cities_translations ct", @"cities c, cities_translations ct"", @""c.id, ct.city_id, COALESCE(ct.title , c.title) title, c.is_active, c.latitude, c.longitude, c.phone, c.phone_secondary, c.geoip_city, c.create_date, 
                                c.update_date", $"c.id = ct.city_id and c.id={id} and ct.lang_id={lang_id}");

                return item;
            }
            catch
            {
                throw;
            }
        }

        public override void update(int id, IDto dto, int lang_id)
        {
            var cities = dto as CitiesDTO;

            if (cities == null)
                return;

            try
            {

                update<CitiesDTO>(id, "cities", cities, "title,geoip_city,phone,phone_secondary,latitude,longitude,is_active", lang_id);

                var citiesTranslationsDTO = new CitiesTranslationsDTO() { city_id = id, lang_id = lang_id, title = cities.title ?? "" };

                update<CitiesTranslationsDTO>("cities_translations", $" city_id ={id} and lang_id={lang_id}", citiesTranslationsDTO, "");
            }
            catch
            {
                throw;
            }
        }

        public override void create(CitiesDTO dto, int lang_id)
        {
            if (dto == null)
                return;

            var cities = dto as CitiesDTO;


            var idContent = create<CitiesDTO>("cities", dto, "id,title,geoip_city,phone,phone_secondary,latitude,logitude,is_active", "id");


            CitiesTranslationsDTO citiesDTO = new CitiesTranslationsDTO()
            {
                city_id = Convert.ToInt32(idContent["id"]),
                lang_id = lang_id,
                title = cities.title ?? "",
            };

            create<CitiesTranslationsDTO>("cities_translations", citiesDTO, "", "city_id,lang_id");

        }

        public override bool delete(int id)
        {
            try
            {

                if (id < 0)
                    return false;

                if (delete("cities_translations", $"city_id={id}"))
                {
                    return delete("cities", $"id={id}");
                }

                return false;

            }
            catch
            {
                throw;
            }

        }

        public override string BuildFilterConditions(string filter)
        {
            throw new NotImplementedException();
        }
    }
}
