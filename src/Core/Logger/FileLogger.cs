namespace VIAEventAssociation.Core.Logger;

public class FileLogger(string filePath) : ILogger
{
    public async Task LogAsync(DateTime time, string operation, string details)
    {
        string logMessage = $"[{time:hh:mm:ss dd/MM/yyyy}] {operation.ToUpper()}: {details}";

        try
        {
            await using StreamWriter writer = new StreamWriter(filePath, true);
            await writer.WriteLineAsync(logMessage);
        }
        catch (Exception ex)
        {
            // Handle exception or log it to another source
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }
}