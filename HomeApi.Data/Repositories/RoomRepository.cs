using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace HomeApi.Data.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HomeApiContext _context;

        public RoomRepository(HomeApiContext context)
        {
            _context = context;
        }

        /// <summary>
        ///  Найти комнату по имени
        /// </summary>
        public async Task<Room> GetRoomByNameAsync(string name)
        {
            return await _context.Rooms
                .Where(r => r.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<Room> GetRoomByIdAsync(Guid id)
        {
            return await _context.Rooms
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        ///  Добавить новую комнату
        /// </summary>
        public async Task AddRoomAsync(Room room)
        {
            var entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
                await _context.Rooms.AddAsync(room);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoomAsync(Room room, UpdateRoomQuery query)
        {
            if (!string.IsNullOrEmpty(query.Name))
                room.Name = query.Name;
            if (!(query.Area == null))
                room.Area =(int) query.Area;
            if (!(query.GasConnected == null))
                room.GasConnected = (bool) query.GasConnected;
            if (!(query.Voltage == null))
                room.Voltage =(int) query.Voltage;

            var entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
                _context.Rooms.Update(room);

            await _context.SaveChangesAsync();
        }
    }
}
