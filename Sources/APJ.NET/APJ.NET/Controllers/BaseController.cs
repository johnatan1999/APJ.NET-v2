using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using APJ.NET.Core;
using APJ.NET.Models.User;
using APJ.NET.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APJ.NET.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        
        private Dictionary<string, object> _queryParameters;

        protected delegate object RequestAction();

        public BaseController(): base()
        {
            _queryParameters = new Dictionary<string, object>();
        }

        protected ApjResponse ProcessRequest(RequestAction action, Type type = null, Type formModel=null)
        {
            try
            {
                object response = action();
                return SendSuccessResult("Successful", 
                    response, 
                    Utility.GetObjectModel(type), 
                    Utility.GetObjectModel(formModel)
                    );
            }
            catch(Exception ex)
            {
                Debug.WriteLine("ex="+ex.StackTrace);
                return SendErrorResult(ex.Message, null, Utility.GetObjectModel(type));
            }
        }
        
        protected ApjResponse SendSuccessResult(string message, object data, object model=null, object formModel=null)
        {
            return new ApjResponse(
                    message: message,
                    data: data,
                    success: true,
                    model: model,
                    formModel: formModel
                ); 
        }
        protected ApjResponse SendErrorResult(string message, object data, object model = null)
        {
            return new ApjResponse(
                    message: message,
                    data: data,
                    success: false,
                    model: model
                ); 
        }

        protected Dictionary<string, object> GetParams()
        {
            _queryParameters.Clear();
            var keys = HttpContext.Request.Query.Keys.ToArray();
            foreach (var key in keys)
            {
                _queryParameters[key] = HttpContext.Request.Query[key].ToString();
            }
            return _queryParameters;
        }

        public Users GetLoggedUsers()
        {
            return (Users)HttpContext.Items["User"];
        }

    }
}