using Asp.Net.Core.BusinessLayer.Interfaces;
using Asp.Net.Core.DataAccessLayer.Interface;
using Asp.Net.Core.Transverse.Logger.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.BusinessLayer.Services
{


    public class LoggingService : ILoggingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericLogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoggingService(IUnitOfWork unitOfWork,
          IHttpContextAccessor httpContextAccessor,
          IGenericLogger<RoleService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

    }
}
