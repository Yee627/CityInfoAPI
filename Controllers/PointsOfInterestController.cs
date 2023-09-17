using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/[controller]")]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterests);
        }

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x = x => x.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(x => x.Id == pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(
            int cityId,
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x = x => x.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var maxPointOfInterestId = itiesDataStore.Current.Cities.SelectMany(
                c => c.PointsOfInterests).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            }

            city.PointsOfInterests.Add(finalPointOfInterest);
            return CreateAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointofinterestid = finalPointOfInterest.Id
                },
                finalPointOfInterest);
        }

        [HttpPut("{pointofinterestid}")]
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId
            PointOfInterestForCreationDto pointOfInterest)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x = x => x.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }

    }

}


