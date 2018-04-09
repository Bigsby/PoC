using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parsing;
using System;

namespace Serilogging
{
    class Program
    {
        class LogSwitcher : LoggingLevelSwitch
        {

        }

        static void Main(string[] args)
        {
            var logSwitch = new LogSwitcher();

            var config = new LoggerConfiguration();
            config
                .Enrich.FromLogContext()
                .MinimumLevel.ControlledBy(logSwitch)
                .WriteTo.File("log.txt",
                    outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Properties}{NewLine}{Exception}",
                    restrictedToMinimumLevel: LogEventLevel.Verbose)
                .WriteTo.MySink();


            Log.Logger = config.CreateLogger();
            Log.Information("Hello!");

            var logger = Log.ForContext<Program>();
            Log.Logger.Information("Hello again!");
            Serilog.Context.LogContext.PushProperty("defaultProp", "defaultValue");
            logger.Information(new Exception("the exception message"), "an error occurred");

            Serilog.Context.LogContext.PushProperty("prop1", "prop1 value");
            logger.Error(new Exception("2 the exception message"), "2 an error occurred");

            logSwitch.MinimumLevel = LogEventLevel.Verbose;
            Serilog.Context.LogContext.PushProperty("prop1", "new prop1 value");
            logger.Information(new Exception("3 the exception message"), "3 an error occurred");
            logger.ForContext("Code", "codeValue")
                .ForContext("anotherCode", "anotherCodeValue")
                .Error(new Exception("3 the exception message"), "3 an error occurred: {prop1}");

            Serilog.Context.LogContext.PushProperty("prop1", null);
            logger.Information(new Exception("4 the exception message"), "4 an error occurred {Context}");
        }


    }

    public static class Extensions
    {
        public static LoggerConfiguration MySink(this LoggerSinkConfiguration loggerConfiguration)
        {
            return loggerConfiguration.Sink(new MySink());
        }
    }

    class MySink : ILogEventSink
    {
        private readonly MessageTemplateParser _parser = new MessageTemplateParser();
        public MySink()
        {

        }
        public void Emit(LogEvent logEvent)
        {
            var template = _parser.Parse(logEvent.MessageTemplate.ToString());
            var a = "";
        }
    }
}
