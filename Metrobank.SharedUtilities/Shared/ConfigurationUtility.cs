using Metrobank.Model.SharedModel;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Metrobank.SharedUtilities.Shared
{
    public static class ConfigurationUtility
    {
        public static ConnectionStrings GetConnectionStrings()
        {

            var builder = new ConfigurationBuilder();
            IConfigurationRoot configuration = builder.Build();

            ConnectionStrings connectionStrings = new ConnectionStrings();
            configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
            return connectionStrings;
        }
    }
}
