using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest
{
    public class Result<T>
    {
        public Result(int StatusCode, string Message, bool HasError, List<T> Data)
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
            this.HasError = HasError;
            this.Data = Data;
        }
        

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool HasError { get; set; }
        public List<T> Data { get; set; }
       
    }
}
