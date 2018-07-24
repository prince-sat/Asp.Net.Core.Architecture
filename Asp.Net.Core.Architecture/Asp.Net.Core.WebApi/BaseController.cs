using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Net.Core.DTO.Common;
using Asp.Net.Core.DTO.Enums;
using Asp.Net.Core.Transverse.Logger.Interface;
using Asp.Net.Core.WebApi.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp.Net.Core.WebApi
{
    /// <summary>
    /// Classe de base pour les controllers
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Containe d'accès à la log
        /// </summary>
        protected IGenericLogger _logger;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IHostingEnvironment _hostingEnvironment;
        private IGenericLogger pLogger;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="logger">Container d'accès à la log</param>
        /// <param name="httpContextAccessor">Contexte d'execution</param>
        /// <param name="hostingEnvironment">Environnement</param>
        public BaseController(IGenericLogger logger,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Méthode générique pour invoquer une méthode web avec une réponse spécifique
        /// </summary>
        /// <typeparam name="T">Type de l'objet de réponse</typeparam>
        /// <param name="serviceCallback">Callback</param>
        /// <returns></returns>
        protected Response<T> InvokeDataResponse<T>(Func<MessagesListDTO, T> serviceCallback)
        {
            Response<T> response = new Response<T>();

            //Execution du service
            try
            {
                MessagesListDTO messages = new MessagesListDTO();
                response.Result = serviceCallback(messages);
                response.Messages = messages;
                _logger.Debug("Response: {@Response}", response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Invoke error");
                response.Messages.AddErrorMessage(ex.Message);
            }

            return response;
        }

        /// <summary>
        /// Méthode générique pour invoquer une méthode web avec une réponse spécifique
        /// </summary>
        /// <typeparam name="T">Type de l'objet de réponse</typeparam>
        /// <param name="serviceCallback">Callback</param>
        /// <returns></returns>
        protected async Task<Response<T>> InvokeDataResponseAsync<T>(Func<MessagesListDTO, Task<T>> serviceCallback)
        {
            Response<T> response = new Response<T>();

            //Execution du service
            try
            {
                MessagesListDTO messages = new MessagesListDTO();
                response.Result = await serviceCallback(messages);
                response.Messages = messages;
                _logger.Debug("Response: {@Response}", response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Invoke error");
                response.Messages.AddErrorMessage(ex.Message);
            }

            return response;
        }

        /// <summary>
        /// Méthode générique pour invoquer une méthode web avec une réponse vide
        /// </summary>
        /// <param name="serviceCallback">Callback</param>
        /// <returns></returns>
        protected Response<VoidDTO> InvokeVoidResponse(Action<MessagesListDTO> serviceCallback)
        {
            Response<VoidDTO> response = new Response<VoidDTO>();

            //Execution du service
            try
            {
                MessagesListDTO messages = new MessagesListDTO();
                serviceCallback(messages);
                response.Result = new VoidDTO();
                response.Messages = messages;
                _logger.Debug("Response: {@Response}", response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Invoke error");
                response.Messages.AddErrorMessage(ex.Message);
            }

            return response;
        }

        /// <summary>
        /// Créer le résultat à retourner.
        /// isAllowed = false => Forbidden (403)
        /// response = null => BadRequest (500) sans résultat avec un message d'erreur spécifique
        /// response.HasErrors => BadRequest (500)
        /// Sinon Ok (200)
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="response"></param>
        /// <param name="isAllowed"></param>
        /// <returns></returns>
        //protected ActionResult CreateActionResultFromResponse<TResponse>(Response<TResponse> response, bool isAllowed = true)
        //{
        //    if (!isAllowed)
        //    {
        //        return Forbid();
        //    }
        //    else if (response == null)
        //    {
        //        return CreateBadRequestWithErrorMessage(CommonErrorMessagesEnum.ERREUR_REPONSE_NULL);
        //    }
        //    else if (response.HasErrors)
        //        return BadRequest(response);
        //    else
        //        return Ok(response);
        //}

        //private ObjectResult CreateBadRequestWithErrorMessage<TEnum>(TEnum enumValue)
        //{
        //    MessagesListDTO messageContainer = new MessagesListDTO();
        //    messageContainer.AddErrorMessageFromEnum(enumValue);
        //    return BadRequest(new Response<VoidDTO>
        //    {
        //        Messages = messageContainer
        //    });
        //}


        /// <summary>
        /// Evènement levé lorsqu'une action du controller est en cours d'exécution
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string actionName = context.RouteData.Values["action"].ToString();
            string controllerName = context.Controller.GetType().Name;
            _logger.Information("[{CorrelationId}] - {ControllerName}/{ActionName} en cours d'exécution", "correlationId Valeur", controllerName, actionName);

            base.OnActionExecuting(context);
        }

        /// <summary>
        /// Evènement levé lorsqu'une action du controller est en cours d'exécution
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string actionName = context.RouteData.Values["action"].ToString();
            string controllerName = context.Controller.GetType().Name;
            _logger.Information("[{CorrelationId}] - {ControllerName}/{ActionName} en cours d'exécution", "_correlationId val", controllerName, actionName);

            return base.OnActionExecutionAsync(context, next);
        }

        /// <summary>
        /// Evènement levé lorsqu'une action du controller est exécuté
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string actionName = context.RouteData.Values["action"].ToString();
            string controllerName = context.Controller.GetType().Name;
            _logger.Information("[{CorrelationId}] - {ControllerName}/{ActionName} terminé", "_correlationId val", controllerName, actionName);

            base.OnActionExecuted(context);
        }

    }
}
