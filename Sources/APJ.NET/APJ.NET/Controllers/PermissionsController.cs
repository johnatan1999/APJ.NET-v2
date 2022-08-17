using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using APJ.NET.Models.User;
using System.Diagnostics;
using APJ.NET.Core;
using APJ.NET.Services;
using APJ.NET.Util;


namespace APJ.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseController
    {

        PermissionService service;

        public PermissionController() : base()
        {
            service = new PermissionService();
        }

        // GET: api/Permission
        [HttpGet(Name = "AllPermission")]
        public ApjResponse Get()
        {
            return ProcessRequest(() =>
            {
                return service.GetAllPermission();
            }, typeof(Permission));
        }

        // GET: api/Permission/1/10
        [HttpGet("{page}/{limit}", Name = "SearchPermission")]
        public ApjResponse Get(int page, int limit)
        {
            return ProcessRequest(() =>
            {
                return service.SearchPermission(GetParams(), page, limit);
            }, typeof(Permission));
        }

        // GET: api/Permission/5
        [HttpGet("{id}", Name = "GetPermission")]
        public ApjResponse GetById(string id)
        {
            return ProcessRequest(() =>
            {
                return service.FindPermissionById(id);
            }, typeof(Permission));
        }

        // POST: api/Permission
        [HttpPost]
        public ApjResponse Post([FromBody] Permission a)
        {
            return ProcessRequest(() => {
                service.AddPermission(a);
                return a;
            }, typeof(Permission));
        }

        // PUT: api/Permission/5
        [HttpPut("{id}")]
        public ApjResponse Put(string id, [FromBody] Permission a)
        {
            return ProcessRequest(() =>
            {
                a.Id = id;
                service.UpdatePermission(a);
                return a;
            }, typeof(Permission));
        }

        // DELETE: api/Permission/5
        [HttpDelete("{id}")]
        public ApjResponse Delete(string id)
        {
            return ProcessRequest(() =>
            {
                Permission a = service.DeletePermission(id);                
                return a;
            }, typeof(Permission));
        }

        
        [HttpGet("model", Name = "GetPermissionModel")]
        public ApjResponse GetModel()
        {
            return ProcessRequest(() =>
            {
                return new { 
                    objectModel = Utility.GetObjectModel(typeof(Permission)), 
                    formModel = service.GetFormModel() 
                };
            });
        }
    }
}
