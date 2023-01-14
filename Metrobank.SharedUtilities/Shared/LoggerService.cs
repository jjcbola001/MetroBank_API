using Metrobank.Model.SharedModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Metrobank.SharedUtilities.Shared
{
    public class LoggerService<T> : ILoggerService<T>
    {
        private readonly ILogger<T> _logger;
        private string fileName;
        public LoggerService(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void ErrorLogger(string entity, string fileLocation, string Error)
        {

            _logger.LogError("Method Name: {EntityName} | Error Message: {Error}", entity, Error);

            if (String.IsNullOrWhiteSpace(fileLocation))
                return;

            if (!Directory.Exists(fileLocation))
                Directory.CreateDirectory(fileLocation);

            fileLocation += fileName = $"{DateTime.Now:yyyyddMM}.txt";

            using StreamWriter sw = File.AppendText(fileLocation);
            sw.WriteLine($"[{DateTime.Now}]");
            sw.WriteLine(Error);
        }


        public void InformationLogger(string entity, string error, string fileLocation = null) =>
            _logger.LogInformation(entity, error);        


        private string Errorinformation(Exception ex) {

            throw new NotImplementedException();
        }
    }
}
