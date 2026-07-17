using CarExplorer.Models.DTOs;
using CarExplorer.Services.Interfaces;

namespace CarExplorer.Services.Service
{
    public class CarService : ICarService
    {
        public Task<List<CarMakeDto>> GetMakesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<CarModelDto>> GetModelsAsync(int makeId, int year, string vehicleType)
        {
            throw new NotImplementedException();
        }

        public Task<List<VehicleTypeDto>> GetVehicleTypesAsync(int makeId)
        {
            throw new NotImplementedException();
        }
    }
}
