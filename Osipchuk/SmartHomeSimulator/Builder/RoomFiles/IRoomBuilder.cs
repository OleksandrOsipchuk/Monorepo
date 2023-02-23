using SmartHomeSimulator.AdditionalFiles.Handlers.IO;

namespace SmartHomeSimulator.Builder.RoomFiles
{
    public interface IRoomBuilder
    {
        Task<IRoomBuilder> SetNameAsync(IIOHandler handler);
        Task<IRoomBuilder> AddHumidityAsync(IIOHandler handler);
        Task<IRoomBuilder> AddLightStateAsync(IIOHandler handler);
        Task<IRoomBuilder> AddTemperatureAsync(IIOHandler handler);
        Task<IRoomBuilder> AddTVStateAsync(IIOHandler handler);
        Room GetRoom();
    }
}
