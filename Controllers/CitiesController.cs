using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            var cities = CitiesDataStore.Current.Cities;
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var city = CitiesDataStore.Current.City.FirstOrDefault(x => x.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }
    }
}