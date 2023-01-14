using Metrobank.Model.Models;
using Metrobank.Model.RequestModel;
using Metrobank.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Metrobank.Interface.Interfaces
{
    public interface IMetrobankRepository
    {
        Task<IEnumerable<AppMain>> FetchMainInformation();
        Task<string> FetchOutputResult(InputRequestModel inputRequest);
        Task<int> AddInputInformation(ActionInputRequestModel addInputRequest);
        Task<int> UpdateInputInformation(ActionInputRequestModel updateInputRequest);
        Task<int> DeleteInputInformation(InputRequestModel updateInputRequest);
    }
}
