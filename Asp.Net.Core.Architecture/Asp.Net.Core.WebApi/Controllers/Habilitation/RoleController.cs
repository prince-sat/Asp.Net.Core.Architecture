using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Asp.Net.Core.DTO.Habilitation;
using Asp.Net.Core.BusinessLayer.Interfaces;
using Asp.Net.Core.DataAccessLayer.Interface;
using Asp.Net.Core.Models.Models;
using Asp.Net.Core.Transverse.Logger.Interface;
using Asp.Net.Core.WebApi.Attributes;
using Asp.Net.Core.WebApi.Constantes;
using Asp.Net.Core.WebApi.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Asp.Net.Core.WebApi.Controllers.Habilitation
{
    [Route("habilitation/roles")]
    [Produces("application/json")]
    [SwaggerUITag(SwaggerUI.HabilitationTag)]
    public class RoleController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleService _roleService = null;

        public RoleController(IGenericLogger<RoleController> logger,
        IHttpContextAccessor httpContextAccessor,
        IHostingEnvironment hostingEnvironment,
        IUnitOfWork unitOfWork,
        IRoleService roleService)
           : base(logger, httpContextAccessor, hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _roleService = roleService;
        }


        /// <summary>
        /// Get List of roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Response<List<RoleDTO>>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(Response<List<RoleDTO>>))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [CustomSwaggerOperation(typeof(RoleController))]
        [Route("")]
        public ActionResult Get([FromQuery]int? id = null)
        {
            Response<List<RoleDTO>> result = InvokeDataResponse(messagesContainer =>
            {
                List<RoleDTO> dtos = new List<RoleDTO>();

                if (id.HasValue)
                {
                    var model = _unitOfWork.RoleRepository.GetSingle(id.Value);
                    var dto = Mapper.Map<Role, RoleDTO>(model);
                    dtos.Add(dto);
                }
                else
                {
                    var models = _unitOfWork.RoleRepository.GetAll().ToList();
                    dtos = Mapper.Map<List<Role>, List<RoleDTO>>(models);
                }

                return dtos;
            });

            return Ok(result);
        }


    }
}