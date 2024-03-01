using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrobank.Model.SharedModel
{
    public class ResponseModel<T>
    {
        public string Token { get; set; }
        public bool ReturnStatus { get; set; }
        public string ReturnMessage { get; set; }
        public List<string> Errors { get; set; }
        public int TotalPages { get; set; }
        public int TotalRows { get; set; }
        public int PageSize { get; set; }
        public Boolean IsAuthenicated { get; set; }
        public T Entity { get; set; }

        public ResponseModel()
        {
            ReturnMessage = "An error occur while processing!";
            ReturnStatus = false;
            Errors = new List<string>();
            TotalPages = 0;
            PageSize = 0;
            IsAuthenicated = false;
        }
    }
}
