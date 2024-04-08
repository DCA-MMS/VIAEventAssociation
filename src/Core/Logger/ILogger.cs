namespace Logger;

public interface ILogger
{
    Task LogAsync(DateTime time, string operation, string details);
}