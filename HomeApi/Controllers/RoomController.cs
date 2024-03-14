using AutoMapper;
using HomeApi.Contractss.Models.Devices;
using HomeApi.Contractss.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using HomeApi.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository _repository;
        private IMapper _mapper;

        public RoomsController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //TODO: Задание - добавить метод на получение всех существующих комнат

        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _repository.GetRoomByNameAsync(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repository.AddRoomAsync(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }

            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }

        ///<summary>
        ///Обновление комнаты
        ///</summary>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id, 
            EditRoomRequest request)
        {

            var room = await _repository.GetRoomByIdAsync(id);
            if (room == null)
                return StatusCode(400,
                    $"Ошибка комната {id} не существует.");
            if(request.Name != null)
            {
                var roomName = await _repository.GetRoomByNameAsync(request.Name);
                if (roomName != null)
                    return StatusCode(400,
                        $"Комната с именем {request.Name} уже существует!" +
                        $"Введите другое имя.");
            }
            var query = _mapper.Map<EditRoomRequest, UpdateRoomQuery>(request);
            await _repository.UpdateRoomAsync(
                room,
                query);
            return StatusCode(200,
                $"Комната с именем {room.Name} изменена!");
        }
    }
}
