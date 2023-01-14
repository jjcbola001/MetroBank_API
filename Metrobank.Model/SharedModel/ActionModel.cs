using Metrobank.Model.RequestModel;
using System;
using System.Collections.Generic;
using System.Text;
using Metrobank.Model.Models;

namespace Metrobank.Model.SharedModel
{
    public class ActionModel
    {
        private readonly ActionInputRequestModel _actionInputRequest;
        public ActionModel(ActionInputRequestModel actionInputRequestModel)
        {
            _actionInputRequest = actionInputRequestModel;
        }


        public AppMain InputInformation()
        {
            return new AppMain
            {
                InputNumber = _actionInputRequest.InputNumber,
                OutputNumber = _actionInputRequest.OutputNumber,
                CreatedDate = DateTime.Now
            };
        }
    }
}
