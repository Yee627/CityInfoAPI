using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CityInfoAPI.Profiles;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>();
        CreateMap<Entities.City, Models.CityDto>();
    }
}