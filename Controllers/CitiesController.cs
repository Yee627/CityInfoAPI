using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CityInfoAPI.Models;
using CityInfoAPI.Services;

namespace CityInfoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities()
        {
            var cityEntities = await _cityInfoRepository.GetCitiesAsync();
            // Manually map the database city to CityWithoutPointsOfInterestDto
            //=========================================================================
            // var results = new List<CityWithoutPointsOfInterestDto>();
            // foreach (var cityEntity in cityEntities)
            // {
            //     results.Add(new CityWithoutPointsOfInterestDto()
            //     {
            //         Id = cityEntity.Id,
            //         Description = cityEntity.Description,
            //         Name = cityEntity.Name
            //     });
            // }
            //=========================================================================
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity
         // since it has two result options, the return type cannot be like ActionResult<CityDto>
         // or ActionResult<CityWithoutPointsOfInterestDto>, so use the Task<IActionResult> as generic returned type
            (int id, bool includedPointsOfInterest = false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includedPointsOfInterest);
            if (city == null)
            {
                return NotFound();
            }

            if (includedPointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }
            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));
        }
    }
}