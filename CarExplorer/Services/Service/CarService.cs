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


    public async Task<List<CarMakeDto>> GetAllMakes()
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<CarMakeDto>>
            (
            $"{_baseUrl}getallmakes?format=json"
            );

        return response?.Results ?? [];
    }

    public async Task<List<VehicleTypeDto>> GetVehicleTypesAsync(int makeId)
    {
        var response = await _httpClient
            .GetFromJsonAsync<ApiResponse<VehicleTypeDto>>
            (
                $"{_baseUrl}GetVehicleTypesForMakeId/{makeId}?format=json"
            );

        return response?.Results ?? [];
    }

    public async Task<List<CarModelDto>> GetModelsAsync(int makeId,int year)
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<CarModelDto>>
            (
                $"{_baseUrl}GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{year}?format=json"
            );

        return response?.Results ?? [];
    }



}