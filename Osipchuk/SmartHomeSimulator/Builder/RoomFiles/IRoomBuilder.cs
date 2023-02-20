using SmartHomeSimulator.AdditionalFiles.Handlers;
namespace SmartHomeSimulator.Builder.RoomFiles
{
    public interface IRoomBuilder
    {
        IRoomBuilder SetName(IIOHandler handler);
        IRoomBuilder AddHumidity(IIOHandler handler);
        IRoomBuilder AddLightState(IIOHandler handler);
        IRoomBuilder AddTemperature(IIOHandler handler);
        IRoomBuilder AddTVState(IIOHandler handler);
        Room GetRoom();
    }
}
