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
    public class RolesController : BasicController<Role>
    {

    }
}
