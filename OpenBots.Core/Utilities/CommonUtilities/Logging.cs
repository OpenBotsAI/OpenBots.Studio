using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.Net;

namespace OpenBots.Core.Utilities.CommonUtilities
{
    /// <summary>
    /// Handles functionality for logging to files
    /// </summary>
    public class Logging
    {
        public Logger CreateFileLogger(string filePath, RollingInterval logInterval,
            LogEventLevel minLogLevel = LogEventLevel.Verbose)
        {
            try
            {
                var levelSwitch = new LoggingLevelSwitch();
                levelSwitch.MinimumLevel = minLogLevel;

                return new LoggerConfiguration()
                        .Enrich.WithProperty("JobId", Guid.NewGuid())
                        .MinimumLevel.ControlledBy(levelSwitch)
                        .WriteTo.File(filePath, rollingInterval: logInterval)
                        .CreateLogger();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Logger CreateHTTPLogger(string projectName, string uri, LogEventLevel minLogLevel = LogEventLevel.Verbose)
        {
            try
            {
                var levelSwitch = new LoggingLevelSwitch();
                levelSwitch.MinimumLevel = minLogLevel;

                return new LoggerConfiguration()
                        .Enrich.WithProperty("JobId", Guid.NewGuid())
                        .Enrich.WithProperty("ProcessName", $"{Dns.GetHostName()}-{projectName}")
                        .Enrich.WithProperty("MachineName", Dns.GetHostName())
                        .MinimumLevel.ControlledBy(levelSwitch)
                        .WriteTo.Http(uri)
                        .CreateLogger();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Logger CreateJsonFileLogger(string jsonFilePath, RollingInterval logInterval,
            LogEventLevel minLogLevel = LogEventLevel.Verbose)
        {
            try
            {
                var levelSwitch = new LoggingLevelSwitch();
                levelSwitch.MinimumLevel = minLogLevel;

                return new LoggerConfiguration()
                        .Enrich.WithProperty("JobId", Guid.NewGuid())
                        .MinimumLevel.ControlledBy(levelSwitch)
                        .WriteTo.File(new CompactJsonFormatter(), jsonFilePath, rollingInterval: logInterval)
                        .CreateLogger();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
