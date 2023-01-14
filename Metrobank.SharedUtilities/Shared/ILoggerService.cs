using System;
using System.Collections.Generic;
using System.Text;

namespace Metrobank.SharedUtilities.Shared
{
    public interface ILoggerService<T>
    {
        void ErrorLogger(string entity, string fileLocation, string Error);

        void InformationLogger(string entity, string error, string fileLocation = null);
    }
}
