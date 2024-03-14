using HomeApi.Data.Models;
using HomeApi.Data.Queries;

namespace HomeApi.Data.Repositories
{
    public interface IDeviceRepository
    {
        Task<Device[]> GetDevicesAsync();
        Task<Device> GetDeviceByNameAsync(string name);
        Task<Device> GetDeviceByIdAsync(Guid id);
        Task SaveDeviceAsync(Device device, Room room);
        Task UpdateDeviceAsync(Device device, Room room, UpdateDeviceQuery query);
        Task DeleteDeviceAsync(Device device);
    }
}
