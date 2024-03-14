using HomeApi.Data.Models;
using HomeApi.Data.Queries;

namespace HomeApi.Data.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> GetRoomByNameAsync(string name);
        Task<Room> GetRoomByIdAsync(Guid id);
        Task AddRoomAsync(Room room);
        Task UpdateRoomAsync(Room room, UpdateRoomQuery query);
    }
}
