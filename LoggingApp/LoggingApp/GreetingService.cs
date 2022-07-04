
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

partial class Program
{
    public class GreetingService : IGreetingService
    {

        private readonly ILogger<GreetingService> _log;
        private readonly IConfiguration _config;

        public GreetingService(ILogger<GreetingService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }
        public void Run()
        {
            for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
            {
                Log.Information("Log number " + i);
            }
        }

        public void Information(string description)
        {
            if (_config.GetSection("Serilog").GetSection("MinimunLevel").GetValue<string>("Default") == "Information")
            {
                Log.Information(description);
            }
           
            
        }

        public void Error(string description)
        {
            if (_config.GetSection("Serilog").GetSection("MinimunLevel").GetValue<string>("Default") == "Error")
            {
                Log.Error(description);
            }
            
        }
    }
}
