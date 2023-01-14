using Metrobank.Interface.Interfaces;
using Metrobank.Model.ResponseModel;
using Metrobank.Model.RequestModel;
using Metrobank.Model.SharedModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Metrobank.Model.Models;
using Metrobank.SharedUtilities.Shared;
using Microsoft.Extensions.Options;

namespace Metrobank.Services.Services
{
    public class MetrobankServices : IMetrobankServices
    {
        private readonly IMetrobankRepository _metrobankRepositry;
        private readonly ILoggerService<MetrobankServices> _loggerService;
        private readonly IOptions<FilePath> _filePath;

        public MetrobankServices(
                        IMetrobankRepository metrobankRepository,
                        ILoggerService<MetrobankServices> loggerService,
                        IOptions<FilePath> filePath) {
            _metrobankRepositry = metrobankRepository;
            _loggerService = loggerService;
            _filePath = filePath;
        }


        public async Task<ResponseModel<IEnumerable<AppMain>>> FetchMainInformationAsync() {
            var result = new ResponseModel<IEnumerable<AppMain>>();

            try
            {
                result.Entity = await _metrobankRepositry.FetchMainInformation();
                result.ReturnStatus = true;
                result.ReturnMessage = "Success";
                return result;
            }
            catch (Exception ex) {

                _loggerService.ErrorLogger(nameof(FetchMainInformationAsync), _filePath.Value.LoggerFilePath, ex.Message);
                return new ResponseModel<IEnumerable<AppMain>> {
                    ReturnMessage = "Failed",
                    ReturnStatus = false
                };
            }
        }
        public async Task<ResponseModel<int>> AddInputInformationAsync(ActionInputRequestModel inputRequestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Entity = await _metrobankRepositry.AddInputInformation(inputRequestModel);
                result.ReturnStatus = true;
                result.ReturnMessage = "Success";
                return result;
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLogger(nameof(AddInputInformationAsync), _filePath.Value.LoggerFilePath, ex.Message);
                return new ResponseModel<int>
                {
                    ReturnMessage = "Failed",
                    ReturnStatus = false
                };
            }
        }

        public async Task<ResponseModel<int>> UpdateInputInformationAsync(ActionInputRequestModel inputRequestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Entity = await _metrobankRepositry.UpdateInputInformation(inputRequestModel);
                result.ReturnStatus = true;
                result.ReturnMessage = "Success";
                return result;
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLogger(nameof(UpdateInputInformationAsync), _filePath.Value.LoggerFilePath, ex.Message);
                return new ResponseModel<int>
                {
                    ReturnMessage = "Failed",
                    ReturnStatus = false
                };
            }
        }

        public async Task<ResponseModel<int>> DeleteInputInformationAsync(InputRequestModel inputRequestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Entity = await _metrobankRepositry.DeleteInputInformation(inputRequestModel);
                result.ReturnStatus = true;
                result.ReturnMessage = "Success";
                return result;
            }
            catch (Exception ex)
            {

                _loggerService.ErrorLogger(nameof(DeleteInputInformationAsync), _filePath.Value.LoggerFilePath, ex.Message);
                return new ResponseModel<int>
                {
                    ReturnMessage = "Failed",
                    ReturnStatus = false
                };
            }
        }

        public async Task<ResponseModel<string>> FetchOutputResultAsync(InputRequestModel inputRequestModel)
        {
            var result = new ResponseModel<string>();

            try
            {
                result.Entity = await _metrobankRepositry.FetchOutputResult(inputRequestModel);
                result.ReturnStatus = true;
                result.ReturnMessage = result.Entity ?? "No Result";
                return result;
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLogger(nameof(FetchOutputResultAsync), _filePath.Value.LoggerFilePath, ex.Message);
                return new ResponseModel<string> {
                    ReturnStatus = false,
                    ReturnMessage = "Failed"
                };
            }
        }
    }
}
