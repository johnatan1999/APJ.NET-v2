using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using APJ.NET.Models.User;
using System.Diagnostics;
using APJ.NET.Core;
using APJ.NET.Services;
using APJ.NET.Models.Dto;
using APJ.NET.Util;

namespace APJ.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : BaseController
    {

        MenuService service;

        public MenusController() : base()
        {
            service = new MenuService();
        }

        // GET: api/Menu
        [HttpGet(Name = "AllMenu")]
        public ApjResponse Get()
        {
            return ProcessRequest(() =>
            {
                //var _params = GetParams();
                //if(_params["with-meta-data"] != null && bool.Parse(_params["with-meta-data"].ToString()))
                //{

                //}
                var _params = GetParams();
                if(_params.ContainsKey("hierarchical") && bool.Parse(_params["hierarchical"].ToString()))
                {
                    return service.GetHierarchicalMenu();
                }
                return service.GetAllMenu();
            }, typeof(Menu));
        }

        // GET: api/Menu/1/10
        [HttpGet("{page}/{limit}", Name = "SearchMenu")]
        public ApjResponse Get(int page, int limit)
        {
            return ProcessRequest(() =>
            {
                return service.SearchMenu(GetParams(), page, limit);
            }, typeof(Menu));
        }

        // GET: api/Menu/5
        [HttpGet("{id}", Name = "GetMenu")]
        public ApjResponse GetById(string id)
        {
            return ProcessRequest(() =>
            {
                return service.FindMenuById(id);
            });
        }

        // POST: api/Menu
        [HttpPost]
        public ApjResponse Post([FromBody] Menu a)
        {
            return ProcessRequest(() => {
                service.AddMenu(a);
                return a;
            });
        }

        // PUT: api/Menu/5
        [HttpPut("{id}")]
        public ApjResponse Put(string id, [FromBody] Menu a)
        {
            return ProcessRequest(() =>
            {
                a.Id = id;
                service.UpdateMenu(a);
                return a;
            });
        }

        // DELETE: api/Menu/5
        [HttpDelete("{id}")]
        public ApjResponse Delete(string id)
        {
            return ProcessRequest(() =>
            {
                Menu a = service.DeleteMenu(id);                
                return a;
            });
        }

        // DELETE: api/Menu/5
        [HttpDelete]
        public ApjResponse Delete([FromBody]ArrayDataModel<string> model)
        {
            return ProcessRequest(() =>
            {
                return service.DeleteMenu(model.Data);
            });
        }

        [HttpGet("model", Name = "GetMenuModel")]
        public ApjResponse GetModel()
        {
            return ProcessRequest(() =>
            {
                return new
                {
                    objectModel = Utility.GetObjectModel(typeof(Users)),
                    formModel = service.GetFormModel()
                };
            });
        }
    }
}
