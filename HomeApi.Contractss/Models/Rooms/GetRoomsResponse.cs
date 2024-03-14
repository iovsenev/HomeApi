namespace HomeApi.Contractss.Models.Devices
{
    public class GetRoomsResponse
    {
        public int RoomAmount { get; set; }
        public RoomView[] Rooms { get; set; }
    }
}
