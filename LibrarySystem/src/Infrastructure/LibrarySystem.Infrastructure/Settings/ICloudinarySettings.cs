namespace LibrarySystem.Infrastructure.Settings;

public interface ICloudinarySettings
{
    public string Name { get; set; }
    public string Key { get; set; }
    public string Secret { get; set; }
}
