using Microsoft.Extensions.Configuration;

namespace console_template;

class Program
{
    static void Main(string[] args)
    {
        IConfigurationRoot configuration = BuildConfiguration(args);

        System.Console.WriteLine(configuration.GetSection("Weather").Value?.ToString());
    }

    private static IConfigurationRoot BuildConfiguration(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("Environment");
        System.Console.WriteLine(env);
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
        
        return builder.Build();

    }
}
