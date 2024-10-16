using Serilog;
using Serilog.Sinks.Logz.Io;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new LoggerConfiguration()
                .WriteTo.LogzIoDurableHttp(
                    "https://listener-eu.logz.io:8071/?type=PeterLogzIoTest&token=sometoken",
                    logzioTextFormatterOptions: new LogzioTextFormatterOptions
                    {
                        BoostProperties = true,
                        LowercaseLevel = true,
                        IncludeMessageTemplate = true,
                        FieldNaming = LogzIoTextFormatterFieldNaming.CamelCase,
                        EventSizeLimitBytes = 261120,
                    })
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("Application", "Mijn Test applicatie")
                .Enrich.WithProperty("Environment", "Test")
                .Enrich.FromLogContext()
                .CreateLogger();




            for (int i = 0; i < 10; i++)
            {
                logger.Information("Test {testwaarde}", "Een waarde");
                logger.Information("Test {getalwaarde}", i);
                // gives the log enough time to be sent to Logz.io
                Console.WriteLine("Volgende");
            }
            Thread.Sleep(5000);


            Console.WriteLine("Klaar");
            Console.ReadLine();
        }

    }
}