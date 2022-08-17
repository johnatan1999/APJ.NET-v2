using APJ.NET.Core.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APJ.NET.Core
{

    public class ResponseData
    {
        public object MetaData { get; set; }

        public object Content { get; set; }
    }

    public class ApjResponse
    {
        public bool Success { get; set; }
        public object Message { get; set; }
        public object Content { get; set; }
        public object ObjectModel { get; set; }
        public object FormModel { get; set; }
    
        public ApjResponse() { }
        public ApjResponse(string message, bool success) 
        {
            Message = message;
            Success = success;
        }
        public ApjResponse(string message, object data, bool success) 
        {
            Message = message;
            Content = data;
            Success = success;
        }
        public ApjResponse(string message, object data, bool success, object model) 
        {
            Message = message;
            Content = data;
            Success = success;
            ObjectModel = model;
        }
        public ApjResponse(string message, object data, bool success, object model, object formModel)
        {
            Message = message;
            Content = data;
            Success = success;
            ObjectModel = model;
            FormModel = FormModel;
        }

    }
}
