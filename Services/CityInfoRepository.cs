using System.Diagnostics;
using CityInfoAPI.DbContexts;
using CityInfoAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfoAPI.Services;

public class CityInfoRepository : ICityInfoRepository
{
    private readonly CityInfoContext _context;
    private readonly ILogger<CityInfoRepository> _logger;
    private readonly ILogger _factoryLogger;

    public CityInfoRepository(CityInfoContext context, ILogger<CityInfoRepository> logger,
        ILoggerFactory loggerFactory)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger;
        _factoryLogger = loggerFactory.CreateLogger("DataAccessLayer");
    }
    
    public async Task<IEnumerable<City>> GetCitiesAsync()
    {
        return await _context.Cities.OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<bool> CityExistsAsync(int cityId)
    {
        return await _context.Cities.AnyAsync(c => c.Id == cityId);
    }

    public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
    {
        if (includePointsOfInterest)
        {
            var timer = new Stopwatch();
            timer.Start();
            var city = await _context.Cities.Include(c => c.PointsOfInterest)
                .Where(c => c.Id == cityId).FirstOrDefaultAsync();
            timer.Stop();
            
            _logger.LogDebug("Querying city for {id} finished in {milliseconds} milliseconds", cityId,timer.ElapsedMilliseconds);
            _factoryLogger.LogInformation("(F)Querying city for {id} finished in {ticks} milliseconds", cityId,timer.ElapsedTicks);
            return city;
        }

        return await _context.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
    {
        return await _context.PointOfInterests.Where(p => p.CityId == cityId).ToListAsync();
    }

    public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
    {
        return await _context.PointOfInterests
            .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
            .FirstOrDefaultAsync();
    }

    public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
    {
        var city = await GetCityAsync(cityId, false);
        if (city != null)
        {
            city.PointsOfInterest.Add(pointOfInterest);
        }
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }

    public void DeletePointOfInterest(PointOfInterest pointOfInterest)
    {
        _context.PointOfInterests.Remove(pointOfInterest);
    }
    
}