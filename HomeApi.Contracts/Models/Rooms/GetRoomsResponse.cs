namespace HomeApi.Contracts.Models.Rooms
{
    public class GetRoomsResponse
    {
        public int RoomAmount { get; set; }
        public RoomView[] Rooms { get; set; }
    }
}
