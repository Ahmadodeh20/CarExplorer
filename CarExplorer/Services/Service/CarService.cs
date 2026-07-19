using CarExplorer.Models.DTOs;
using CarExplorer.Models.Settings;
using CarExplorer.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace CarExplorer.Services.Service;

public class CarService : ICarService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly VehicleApiSettings _apiSettings;
    private readonly CacheSettings _casheSettings;

    public CarService(HttpClient httpClient,IMemoryCache cache, IOptions<VehicleApiSettings> options,IOptions<CacheSettings> casheSettings)
    {
        _httpClient = httpClient;
        _cache = cache;
        _apiSettings = options.Value;
        _casheSettings = casheSettings.Value;
    }


    public async Task<List<CarMakeDto>> GetAllMakesAsync()
    {
        if (_cache.TryGetValue(CacheKeys.CarMakes, out List<CarMakeDto>? cachedMakes))
        {
            return cachedMakes;
        }

            var response = await _httpClient
            .GetFromJsonAsync<ApiResponse<CarMakeDto>>
            (
                $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.AllMakes}"
            );

        var allMakes = response?.Results ?? [];

        _cache.Set(CacheKeys.CarMakes, allMakes, TimeSpan.FromMinutes(_casheSettings.CarMakesDurationMinutes));

        return allMakes;
    }

    public async Task<List<VehicleTypeDto>> GetVehicleTypesAsync(int makeId)
    {
        var cacheKey = $"{CacheKeys.VehicleTypes}_{makeId}";
        if (_cache.TryGetValue(cacheKey,out List<VehicleTypeDto>? cachedVehicleTypes))
        {
            return cachedVehicleTypes;
        }

        var response = await _httpClient
            .GetFromJsonAsync<ApiResponse<VehicleTypeDto>>
            (
                $"{_apiSettings.BaseUrl}{string.Format(_apiSettings.Endpoints.VehicleTypes,makeId)}"
            );

        var vehicleTypes = response?.Results ?? [];

        _cache.Set(cacheKey,vehicleTypes,TimeSpan.FromMinutes(_casheSettings.VehicleTypesDurationMinutes));

        return vehicleTypes;
    }

    public async Task<List<CarModelDto>> GetModelsAsync(int makeId,int year)
    {
        var cacheKey = $"{CacheKeys.Models}_{makeId}_{year}";
        if (_cache.TryGetValue(cacheKey, out List<CarModelDto>? cachedModels))
        {
            return cachedModels;
        }

        var models = await _httpClient.GetFromJsonAsync<ApiResponse<CarModelDto>>
            (
                $"{_apiSettings.BaseUrl}{string.Format(_apiSettings.Endpoints.Models,makeId,year)}"
            );

        _cache.Set(cacheKey, models, TimeSpan.FromMinutes(_casheSettings.ModelsDurationMinutes));

        return models?.Results ?? [];
    }
}