using Domain.Ports;
using Serilog;

namespace LoggerService.Serilog;

public class SerilogLoggerService(ILogger _logger) : ISerilogLoggerService
{
    public void LogInfo(string message)
    {
        _logger.Information(message);
    }

    public void LogError(string message)
    {
        _logger.Error(message);
    }

    public void LogWarning(string message)
    {
       _logger.Warning(message);
    }
}