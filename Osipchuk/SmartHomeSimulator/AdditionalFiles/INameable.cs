namespace SmartHomeSimulator.AdditionalFiles
{
    // used for "where T : INameable" (Room/House) restrictions
    public interface INameable
    {
        string Name { get; set; }
    }
}