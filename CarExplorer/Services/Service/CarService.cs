using CarExplorer.Models.DTOs;
using CarExplorer.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;

namespace CarExplorer.Services.Service;

public class CarService : ICarService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly string _baseUrl;
    private readonly int _carMakesDuration;
    private readonly int _vehicleTypesDuration;
    private readonly int _modelsDuration;

    public CarService(HttpClient httpClient,IConfiguration configuration,IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
        _baseUrl = configuration["VehicleApi:BaseUrl"];
        _carMakesDuration = configuration.GetValue<int>("CacheSettings:CarMakesDurationMinutes");
        _vehicleTypesDuration = configuration.GetValue<int>("CacheSettings:VehicleTypesDurationMinutes");
        _modelsDuration = configuration.GetValue<int>("CacheSettings:ModelsDurationMinutes");
    }


    public async Task<List<CarMakeDto>> GetAllMakes()
    {
        if (_cache.TryGetValue("car_makes",out List<CarMakeDto>? cachedMakes))
        {
            return cachedMakes;
        }

        var response = await _httpClient
            .GetFromJsonAsync<ApiResponse<CarMakeDto>>
            (
                $"{_baseUrl}getallmakes?format=json"
            );

        var allMakes = response?.Results ?? [];

        _cache.Set("car_makes", allMakes, TimeSpan.FromMinutes(_carMakesDuration));

        return allMakes;
    }

    public async Task<List<VehicleTypeDto>> GetVehicleTypesAsync(int makeId)
    {
        var cacheKey = $"vehicle_types_{makeId}";

        if (_cache.TryGetValue(cacheKey,out List<VehicleTypeDto>? cachedVehicleTypes))
        {
            return cachedVehicleTypes;
        }

        var response = await _httpClient
            .GetFromJsonAsync<ApiResponse<VehicleTypeDto>>
            (
                $"{_baseUrl}GetVehicleTypesForMakeId/{makeId}?format=json"
            );

        var vehicleTypes = response?.Results ?? [];

        _cache.Set(cacheKey,vehicleTypes,TimeSpan.FromMinutes(_vehicleTypesDuration));

        return vehicleTypes;
    }

    public async Task<List<CarModelDto>> GetModelsAsync(int makeId,int year)
    {
        var cacheKey = $"models_{makeId}_{year}";

        if (_cache.TryGetValue(cacheKey, out List<CarModelDto>? cachedModels))
        {
            return cachedModels;
        }

        var models = await _httpClient.GetFromJsonAsync<ApiResponse<CarModelDto>>
            (
                $"{_baseUrl}GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}?format=json"
            );

        _cache.Set(cacheKey, models, TimeSpan.FromMinutes(_modelsDuration));

        return models?.Results ?? [];
    }



}