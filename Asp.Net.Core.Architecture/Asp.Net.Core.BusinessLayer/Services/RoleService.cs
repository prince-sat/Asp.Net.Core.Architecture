using Asp.Net.Core.BusinessLayer.Interfaces;
using Asp.Net.Core.DataAccessLayer.Interface;
using Asp.Net.Core.Models.Models;
using Asp.Net.Core.Transverse.Logger.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.BusinessLayer.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericLogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleService(IUnitOfWork unitOfWork,
          IHttpContextAccessor httpContextAccessor,
          IGenericLogger<RoleService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public Role RolesSelonId(int roleId)
        {
            return _unitOfWork.RoleRepository.GetSingle(roleId);
        }
    }
}
