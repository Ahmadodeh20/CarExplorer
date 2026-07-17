using System.Net.Http.Json;
using CarExplorer.Models.DTOs;
using CarExplorer.Services.Interfaces;

namespace CarExplorer.Services.Service;

public class CarService : ICarService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;


    public CarService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["VehicleApi:BaseUrl"]
            ?? throw new Exception("VehicleApi BaseUrl is missing");
    }


    public async Task<List<CarMakeDto>> GetMakesAsync()
    {
        var response = await _httpClient
            .GetFromJsonAsync<ApiResponse<CarMakeDto>>
            (
                $"{_baseUrl}getallmakes?format=json"
            );

        return response?.Results ?? [];
    }


    public Task<List<CarModelDto>> GetModelsAsync(
        int makeId,
        int year,
        string vehicleType)
    {
        throw new NotImplementedException();
    }


    public Task<List<VehicleTypeDto>> GetVehicleTypesAsync(int makeId)
    {
        throw new NotImplementedException();
    }
}