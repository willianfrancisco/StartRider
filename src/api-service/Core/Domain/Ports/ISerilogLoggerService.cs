namespace Domain.Ports;

public interface ISerilogLoggerService
{
    void LogInfo(string message);
    void LogError(string message);
    void LogWarning(string message);
}