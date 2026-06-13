using System.Text.Json;
using SnapGuard.MockStation;

MqttStationOptions options;

try
{
    string settingsFilePath = Path.Combine(Environment.CurrentDirectory,
        "settings.json");

    if (File.Exists(settingsFilePath))
    {
        using var stream = File.OpenRead(settingsFilePath);
        options = await JsonSerializer.DeserializeAsync<MqttStationOptions>(
            stream,
            JsonSerializerOptions.Web) ?? throw new Exception(
                "Could not deserialize JSON file");
    }
    else
    {
        throw new Exception("Could not find settings file");
    }
}
catch (Exception exception)
{
    ConsoleLogger.LogError(exception.Message);
    return;
}

var mqtt = new MqttStationService(options);

Console.BackgroundColor = ConsoleColor.Green;
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("SnapGuard.Mock Started");
Console.ResetColor();

try
{
    await mqtt.Start(options);
}
catch (Exception exception)
{
    ConsoleLogger.LogError(exception.Message);
}

Console.WriteLine("\nPress ENTER to exit");
Console.ReadLine();
