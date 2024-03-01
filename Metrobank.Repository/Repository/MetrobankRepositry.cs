using Metrobank.DataAccessLibrary.DataAccess;
using Metrobank.Interface.Interfaces;
using Metrobank.Model.RequestModel;
using Metrobank.Model.SharedModel;
using Metrobank.SharedUtilities.Shared;
using Metrobank.Model.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Metrobank.DataAccessLibrary.Interfaces;

namespace Metrobank.Repository.Repository
{
    public class MetrobankRepositry : IMetrobankRepository
    {
        private readonly MetrobankDataAccess _dbAccess;
        private readonly IMetrobankDataAccess _dataAcccess;
        private readonly ILoggerService<MetrobankRepositry> _loggerService;
        private readonly IOptions<FilePath> _filePath;
        private readonly IOptions<LogResult> _logResult;
        private readonly string LoggerFilePath;
        private readonly string OutputFilePath;
        private string outputString;
        private AppMain _appMain;
    
        public MetrobankRepositry(MetrobankDataAccess db,
                                  IOptions<FilePath> filePath,
                                  ILoggerService<MetrobankRepositry> loggerservice,
                                  IOptions<LogResult> logResult,
                                  IMetrobankDataAccess dataAccess)
        {
            _dbAccess = db;
            _filePath = filePath;
            _loggerService = loggerservice;
            _logResult = logResult;
            _dataAcccess = dataAccess;

            LoggerFilePath = _logResult.Value.Ans ? _filePath.Value.LoggerFilePath : "";
            OutputFilePath = _logResult.Value.Ans ? _filePath.Value.OutputFilePath : "";
        }

        public async Task<IEnumerable<AppMain>> FetchMainInformation()
        {
            try
            {
                var res = await _dbAccess
                  .AppMains
                  .AsNoTracking()
                  .ToListAsync();

                return res;
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLogger(nameof(FetchMainInformation), LoggerFilePath, ex.Message);
                return new List<AppMain>();
            }
        }
        public async Task<int> AddInputInformation(ActionInputRequestModel addInputRequest)
        {
         ActionModel _actionModel;
            try
            {
                _actionModel = new ActionModel(addInputRequest);
                _appMain = _actionModel.InputInformation();
                _dbAccess.AttachRange(_appMain);

                return await _dbAccess.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLogger(nameof(AddInputInformation), LoggerFilePath, ex.Message);
                return 0;
            }
        }

        public async Task<int> UpdateInputInformation(ActionInputRequestModel updateInputRequest)
        {
            try
            {
                _appMain = await _dbAccess.AppMains
                    .SingleOrDefaultAsync(param => param.InputNumber.Equals(updateInputRequest.InputNumber));

                if (_appMain == null) return 0;

                _appMain.OutputNumber = updateInputRequest.OutputNumber;
                _dbAccess.UpdateRange(_appMain);

                return await _dbAccess.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLogger(nameof(UpdateInputInformation), LoggerFilePath, ex.Message);
                return 0;
            }
        }
        public async Task<int> DeleteInputInformation(InputRequestModel updateInputRequest)
        {
            try
            {
                _appMain = await _dbAccess.AppMains
                    .SingleOrDefaultAsync(param => param.InputNumber.Equals(updateInputRequest.InputNumber));

                if (_appMain == null) return 0;

                _dbAccess.RemoveRange();

                return await _dbAccess.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLogger(nameof(DeleteInputInformation), LoggerFilePath, ex.Message);
                return 0;
            }
        }
        public async Task<string> FetchOutputResult(InputRequestModel inputRequest)
        {
            try
            {
                var res = await _dbAccess.AppMains
                                .Where(param => param.InputNumber == inputRequest.InputNumber)
                                .FirstOrDefaultAsync();


                if (res == null)
                    return outputString;
                int n = res.InputNumber;

                for (int i = 1; i <= n; i++)
                {
                    for (int j = 0; j < (n - i); j++)
                        outputString += " ";
                    for (int j = 1; j <= i; j++)
                        outputString += "*";
                    for (int k = 1; k < i; k++)
                        outputString += "*";
                    outputString += "\n";
                }

                for (int i = n - 1; i >= 1; i--)
                {
                    for (int j = 0; j < (n - i); j++)
                        outputString += " ";
                    for (int j = 1; j <= i; j++)
                        outputString += "*";
                    for (int k = 1; k < i; k++)
                        outputString += "*";
                    outputString += "\n";
                }

                var filepath = OutputFilePath;
                if (String.IsNullOrWhiteSpace(filepath))
                    return outputString;

                if (!Directory.Exists(filepath))
                    Directory.CreateDirectory(filepath);

                filepath += "Fileoutput.txt";

                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine($"[{DateTime.Now}]");
                    sw.WriteLine(outputString);
                }

                return outputString;
            }
            catch (Exception ex)
            {
                _loggerService.ErrorLogger(nameof(FetchOutputResult), LoggerFilePath, ex.Message);
                return null;
            }
        }
    }
}