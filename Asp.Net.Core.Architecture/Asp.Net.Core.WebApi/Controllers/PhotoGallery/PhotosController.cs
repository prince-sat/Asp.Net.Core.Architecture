using Asp.Net.Core.BusinessLayer.Interfaces;
using Asp.Net.Core.DataAccessLayer.Interface;
using Asp.Net.Core.DTO.Common;
using Asp.Net.Core.DTO.PhotoGallery;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Asp.Net.Core.WebApi.Controllers.PhotoGallery
{

    [Route("photoGallery/photos")]
    [Produces("application/json")]
    [SwaggerUITag(SwaggerUI.PhotoGalleryTag)]
    public class PhotosController : BaseController
    {

        private readonly IUnitOfWork _unitOfWork;
        public PhotosController(IGenericLogger<PhotosController> logger,
       IHttpContextAccessor httpContextAccessor,
       IHostingEnvironment hostingEnvironment,
       IUnitOfWork unitOfWork)

          : base(logger, httpContextAccessor, hostingEnvironment)
        {
            _unitOfWork = unitOfWork;

        }


        /// <summary>
        /// Get List of photos
        /// </summary>
        /// <param name="id">identifiant album</param>
        /// <param name="page">number of page</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        [HttpGet()]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Response<List<PhotoDTO>>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(Response<List<PhotoDTO>>))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [CustomSwaggerOperation(typeof(PhotosController))]
        [Route("")]

        public ActionResult Get(int? id = null, int? page = 0, int? pageSize = 12)
        {
            Response<PaginationSet<PhotoDTO>> result = InvokeDataResponse(messagesContainer =>
            {

                PaginationSet<PhotoDTO> pagedSet = null;
                IEnumerable<PhotoDTO> _photosDto = null;
                List<Photo> _photos = new List<Photo>();
                try
                {



                    if (id.HasValue)
                    {
                        var model = _unitOfWork.PhotoRepository.GetSingle(id.Value);
                        _photos.Add(model);
                        _photosDto = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoDTO>>(_photos);
                        pagedSet = new PaginationSet<PhotoDTO>()
                        {
                            Page = 0,
                            TotalCount = 1,
                            TotalPages = (int)Math.Ceiling((decimal)1 / 12),
                            Items = _photosDto
                        };
                    }
                    else
                    {
                        int _totalPhotos = new int();
                        int currentPage = page.Value;
                        int currentPageSize = pageSize.Value;
                        _photos = _unitOfWork.PhotoRepository
                       .AllIncluding(p => p.Album)
                       .OrderBy(p => p.Id)
                       .Skip(currentPage * currentPageSize)
                       .Take(currentPageSize)
                       .ToList();
                        _totalPhotos = _unitOfWork.PhotoRepository.GetAll().Count();
                        _photosDto = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoDTO>>(_photos);
                        pagedSet = new PaginationSet<PhotoDTO>()
                        {
                            Page = currentPage,
                            TotalCount = _totalPhotos,
                            TotalPages = (int)Math.Ceiling((decimal)_totalPhotos / currentPageSize),
                            Items = _photosDto
                        };

                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.LoggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                    _unitOfWork.LoggingRepository.Commit();
                }
                return pagedSet;
            });
            return Ok(result);

        }

        /// <summary>
        /// Delete photo
        /// </summary>
        /// <param name="id">Album identifiant</param>
        /// <returns></returns>
        [HttpDelete]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Response<VoidDTO>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(Response<VoidDTO>))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [CustomSwaggerOperation(typeof(PhotosController))]
        [Route("")]
        public ActionResult Delete(int id)
        {
            Response<VoidDTO> result = InvokeVoidResponse(messagesContainer =>
            {
                try
                {
                    Photo photo = null;
                    photo = _unitOfWork.PhotoRepository.GetSingle(id);
                    _unitOfWork.PhotoRepository.Delete(photo);
                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    _unitOfWork.LoggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                    _unitOfWork.LoggingRepository.Commit();
                }
            });
            return Ok(result);
        }


    }
}
