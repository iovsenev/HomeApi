namespace HomeApi.Contractss.Models.Devices
{
    public class GetDeviceResponse
    {
        public int DeviceAmount { get; set; }
        public DeviceView[] Devices { get; set; }
    }
}
