using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Net.Core.WebApi.Attributes;
using Asp.Net.Core.WebApi.Constantes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.Net.Core.WebApi.Controllers.Habilitation
{
    [Route("habilitation/roles")]
    [Produces("application/json")]
    [SwaggerUITag(SwaggerUI.HabilitationTag)]
    public class RoleController : Controller
    {
    }
}