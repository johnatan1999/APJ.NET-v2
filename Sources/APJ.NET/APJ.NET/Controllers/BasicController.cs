using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apj.Net.Dao.Model;
using APJ.NET.Core;
using APJ.NET.Models.User;
using APJ.NET.Services;
using APJ.NET.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APJ.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicController<M> : BaseController where M : BaseModel, new()
    {
        ApjService<M> service;
        public BasicController() : base()
        {
            service = new ApjService<M>();
        }

        // GET: api/Role
        [HttpGet(Name = "AllRole")]
        public ApjResponse Get()
        {
            return ProcessRequest(() =>
            {
                return service.GetAll();
            }, typeof(M));
        }

        // GET: api/Role/1/10
        [HttpGet("{page}/{limit}")]
        public ApjResponse Get(int page, int limit)
        {
            return ProcessRequest(() =>
            {
                return service.Search(GetParams(), page, limit);
            }, typeof(M));
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public ApjResponse GetById(string id)
        {
            return ProcessRequest(() =>
            {
                return service.FindById(id);
            }, typeof(M));
        }

        // POST: api/Role
        [HttpPost]
        public ApjResponse Post([FromBody] M a)
        {
            return ProcessRequest(() => {
                service.Add(a);
                return a;
            }, typeof(M));
        }

        // PUT: api/Role/5
        [HttpPut("{id}")]
        public ApjResponse Put(string id, [FromBody] M a)
        {
            return ProcessRequest(() =>
            {
                a.Id = id;
                service.Update(a);
                return a;
            }, typeof(Role));
        }


        // DELETE: api/Role/5
        [HttpDelete("{id}")]
        public ApjResponse Delete(string id)
        {
            return ProcessRequest(() =>
            {
                M a = service.Delete(id);
                return a;
            }, typeof(Role));
        }

        [HttpGet("model")]
        public ApjResponse GetModel()
        {
            return ProcessRequest(() =>
            {
                return new { objectModel = Utility.GetObjectModel(typeof(Role)), formModel = service.GetFormModel() };
            });
        }
    }
}