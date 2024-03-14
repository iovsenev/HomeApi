using AutoMapper;
using HomeApi.Contractss.Models.Devices;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using HomeApi.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebAPISecondASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private IDeviceRepository _devices;
        private IRoomRepository _rooms;
        private IMapper _mapper;

        public DevicesController(IDeviceRepository devices, IRoomRepository rooms, IMapper mapper)
        {
            _devices = devices;
            _rooms = rooms;
            _mapper = mapper;
        }

        /// <summary>
        /// Просмотр списка подключенных устройств
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _devices.GetDevicesAsync();

            var resp = new GetDeviceResponse
            {
                DeviceAmount = devices.Length,
                Devices = _mapper.Map<Device[], DeviceView[]>(devices)
            };

            return StatusCode(200, resp);
        }

        // TODO: Задание: напишите запрос на удаление устройства

        /// <summary>
        /// Добавление нового устройства
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add(AddDeviceRequest request)
        {
            var room = await _rooms.GetRoomByNameAsync(request.RoomLocation);
            if (room == null)
                return StatusCode(400, 
                    $"Ошибка: Комната {request.RoomLocation} не подключена. " +
                    $"Сначала подключите комнату!");

            var device = await _devices.GetDeviceByNameAsync(request.Name);
            if (device != null)
                return StatusCode(400, $"Ошибка: Устройство {request.Name} уже существует.");

            var newDevice = _mapper.Map<AddDeviceRequest, Device>(request);
            await _devices.SaveDeviceAsync(newDevice, room);

            return StatusCode(201, $"Устройство {request.Name} добавлено. Идентификатор: {newDevice.Id}");
        }

        /// <summary>
        /// Обновление существующего устройства
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Edit(
            [FromRoute] Guid id,
            [FromBody] EditDeviceRequest request)
        {
            var room = await _rooms.GetRoomByNameAsync(request.NewRoom);
            if (room == null)
                return StatusCode(400, 
                    $"Ошибка: Комната {request.NewRoom} не подключена. " +
                    $"Сначала подключите комнату!");

            var device = await _devices.GetDeviceByIdAsync(id);
            if (device == null)
                return StatusCode(400, 
                    $"Ошибка: Устройство с идентификатором {id} не существует.");

            var withSameName = await _devices.GetDeviceByNameAsync(request.NewName);
            if (withSameName != null)
                return StatusCode(400, 
                    $"Ошибка: Устройство с именем {request.NewName} уже подключено. " +
                    $"Выберите другое имя!");

            await _devices.UpdateDeviceAsync(
                device,
                room,
                new UpdateDeviceQuery(request.NewName, request.NewSerial)
            );

            return StatusCode(200, 
                $"Устройство обновлено! " +
                $"Имя - {device.Name}, " +
                $"Серийный номер - {device.SerialNumber}, " +
                $" Комната подключения - {device.Room.Name}");
        }

        ///<summary>
        ///Удаляет существующее устройство
        ///</summary>
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> Delete(
            [FromBody] DeleteDeviceRequest request)
        {
            var device = await _devices.GetDeviceByNameAsync(request.Name);
            if (device == null)
                return StatusCode(400, 
                    $"Устройство с названием {request.Name} не найдено! " +
                    $"Введите корректное имя устройства");
            await _devices.DeleteDeviceAsync(device);
            return StatusCode(200, $"Устройство {device.Name} отключено!"); 
        }


    }
}
