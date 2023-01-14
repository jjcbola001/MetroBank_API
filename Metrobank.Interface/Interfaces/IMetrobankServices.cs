using Metrobank.Model.Models;
using Metrobank.Model.RequestModel;
using Metrobank.Model.ResponseModel;
using Metrobank.Model.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Metrobank.Interface.Interfaces
{
    public interface IMetrobankServices
    {
        Task<ResponseModel<IEnumerable<AppMain>>> FetchMainInformationAsync();
        Task<ResponseModel<string>> FetchOutputResultAsync(InputRequestModel inputRequestModel);
        Task<ResponseModel<int>> AddInputInformationAsync(ActionInputRequestModel inputRequestModel);
        Task<ResponseModel<int>> UpdateInputInformationAsync(ActionInputRequestModel inputRequestModel);
        Task<ResponseModel<int>> DeleteInputInformationAsync(InputRequestModel inputRequestModel);
    }
}
