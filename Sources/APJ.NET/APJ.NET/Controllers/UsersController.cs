using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using APJ.NET.Models.User;
using System.Diagnostics;
using APJ.NET.Core;
using APJ.NET.Services;
using APJ.NET.Helper;
using APJ.NET.Util;

namespace APJ.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        readonly UsersService service;

        public UsersController() : base()
        {
            service = new UsersService();
        }

        // GET: api/Users
        [Authorize(Role = "Admin")]
        [HttpGet(Name = "AllUsers")]
        public ApjResponse Get()
        {
            return ProcessRequest(() =>
            {
                return service.GetAllUsers();
            }, typeof(Users));
        }

        // GET: api/Users/1/10
        [HttpGet("{page}/{limit}", Name = "SearchUsers")]
        public ApjResponse Get(int page, int limit)
        {
            return ProcessRequest(() =>
            {
                return service.SearchUsers(GetParams(), page, limit);
            }, typeof(Users));
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUsers")]
        public ApjResponse GetById(string id)
        {
            return ProcessRequest(() =>
            {
                return service.FindUsersById(id);
            }, typeof(Users));
        }

        // POST: api/Users
        [HttpPost]
        public ApjResponse Post([FromBody] Users a)
        {
            return ProcessRequest(() => {
                service.AddUsers(a);
                return a;
            }, typeof(Users));
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public ApjResponse Put(string id, [FromBody] Users a)
        {
            return ProcessRequest(() =>
            {
                a.Id = id;
                service.UpdateUsers(a);
                return a;
            }, typeof(Users));
        }

        // PUT: api/Users/5
        [HttpPut("{id}/{state}")]
        public ApjResponse PutChangeState(string id, int state)
        {
            return ProcessRequest(() =>
            {
                Users a = service.ChangeUsersState(id, state);
                return a;
            }, typeof(Users));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public ApjResponse Delete(string id)
        {
            return ProcessRequest(() =>
            {
                Users a = service.DeleteUsers(id, false);                
                return a;
            }, typeof(Users));
        }

        [HttpGet("model", Name = "GetUsersModel")]
        public ApjResponse GetModel()
        {
            return ProcessRequest(() =>
            {
                return new { 
                    objectModel = Utility.GetObjectModel(typeof(Users)), 
                    formModel = service.GetFormModel() 
                };
            });
        }
    }
}
