using Microsoft.Extensions.Logging;
using SwiftHR.LeaveManagement.Application.Interfaces.Logging;

namespace SwiftHR.LeaveManagement.Infrastructure.Logging;

public class LoggerAdapter<T> : IAppLogger<T>
{
    private readonly ILogger _logger;

    /// <summary>
    ///     Takes ILoggerFactor as parameter and creates a logger of type <typeparamref name="T" />
    /// </summary>
    /// <param name="loggerFactory"></param>
    public LoggerAdapter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<T>();
    }

    /// <summary>
    ///     Logs Information with message and arguments
    /// </summary>
    public void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }

    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }
}