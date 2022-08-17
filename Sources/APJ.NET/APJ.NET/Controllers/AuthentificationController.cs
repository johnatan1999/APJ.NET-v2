using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APJ.NET.Core;
using APJ.NET.Models;
using APJ.NET.Models.Dto;
using APJ.NET.Models.User;
using APJ.NET.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APJ.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : BaseController
    {

        AuthentificationSerivce service;

        public AuthentificationController()
        {
            service = new AuthentificationSerivce();
        }

        [HttpPost]
        public ApjResponse Login([FromBody] AuthentificationModel model) 
        {
            return ProcessRequest(() =>
            {
                ClientUserModel u = service.Authenticate(model.Login, model.Password);
                if (u == null)
                    throw new Exception("Authentication failed: please verify your login and password.");
                return u;
            });
        }
    }
}