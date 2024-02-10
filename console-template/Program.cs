using System.Diagnostics;
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
        var env = GetApplicationEnvironment(args);
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder.Build();

    }
    /// <summary>
    /// To specify the environment of the application, we should see if there is an 
    /// environemnt variable and/or a parameter which comes from console arguments 
    /// named '--Environment. The console argument overrides the environment variable
    /// and then the ending result returns.
    /// </summary>
    /// <param name="args"></param>
    /// <returns>string?</returns>
    private static string? GetApplicationEnvironment(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("Environment");
        System.Console.WriteLine(env);
        bool found = false;
        foreach (var item in args)
        {
            if (found == true) {
                env = item;
                break;
            }
            if (item.ToLower() == "--environment")
            {
                found = true;
            }
        }
        System.Console.WriteLine(env);

        return env;
    }
}
