using CarExplorer.Models.DTOs;

namespace CarExplorer.Services.Interfaces
{
    public interface ICarService
    {
        Task<List<CarMakeDto>> GetMakesAsync();
        Task<List<VehicleTypeDto>> GetVehicleTypesAsync(int makeId);
        Task<List<CarModelDto>> GetModelsAsync(
            int makeId,
            int year,
            string vehicleType);
    }
}
