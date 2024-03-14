using AutoMapper;
using HomeApi.Contractss.Models.Devices;
using HomeApi.Contractss.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using WebAPISecondASP.Configurations;

namespace WebAPISecondASP.Maps
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressInfo>();
            CreateMap<HomeOptions, ResponseInfo>()
                .ForMember(m => m.AddressInfo,
                    opt => opt.MapFrom(src => src.Address));

            // Валидация запросов:
            CreateMap<AddDeviceRequest, Device>()
                .ForMember(d => d.Location,
                    opt => opt.MapFrom(r => r.RoomLocation));
            CreateMap<AddRoomRequest, Room>();
            CreateMap<Device, DeviceView>();
            CreateMap<EditRoomRequest, UpdateRoomQuery>();
        }
    }
}
