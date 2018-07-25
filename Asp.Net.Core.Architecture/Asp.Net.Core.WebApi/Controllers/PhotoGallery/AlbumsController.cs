using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

namespace Asp.Net.Core.WebApi.Controllers.PhotoGallery
{
    [Route("photoGallery/albums")]
    [Produces("application/json")]
    [SwaggerUITag(SwaggerUI.PhotoGalleryTag)]
    public class AlbumsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlbumsController(IGenericLogger<PhotosController> logger,
      IHttpContextAccessor httpContextAccessor,
      IHostingEnvironment hostingEnvironment,
      IUnitOfWork unitOfWork)
         : base(logger, httpContextAccessor, hostingEnvironment)
        {
            _unitOfWork = unitOfWork;

        }

        /// <summary>
        /// Get List of albums
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Response<List<AlbumDTO>>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(Response<List<AlbumDTO>>))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [CustomSwaggerOperation(typeof(PhotosController))]
        [Route("")]

        public ActionResult Get(int? id = null, int? page = 0, int? pageSize = 12)
        {
            Response<PaginationSet<AlbumDTO>> result = InvokeDataResponse(messagesContainer =>
            {

                PaginationSet<AlbumDTO> pagedSet = null;
                IEnumerable<AlbumDTO> _albumsDto = null;
                List<Album> _albums = new List<Album>();
                try
                {

                    if (id.HasValue)
                    {
                        var model = _unitOfWork.AlbumRepository.GetSingle(id.Value);
                        _albums.Add(model);
                        _albumsDto = Mapper.Map<IEnumerable<Album>, IEnumerable<AlbumDTO>>(_albums);
                        pagedSet = new PaginationSet<AlbumDTO>()
                        {
                            Page = 0,
                            TotalCount = 1,
                            TotalPages = (int)Math.Ceiling((decimal)1 / 12),
                            Items = _albumsDto
                        };
                    }
                    else
                    {
                        int _totalAlbums = new int();
                        int currentPage = page.Value;
                        int currentPageSize = pageSize.Value;
                        _albums = _unitOfWork.AlbumRepository
                        .AllIncluding(a => a.Photos)
                        .OrderBy(a => a.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();
                        _totalAlbums = _unitOfWork.AlbumRepository.GetAll().Count();
                        _albumsDto = Mapper.Map<IEnumerable<Album>, IEnumerable<AlbumDTO>>(_albums);
                        pagedSet = new PaginationSet<AlbumDTO>()
                        {
                            Page = currentPage,
                            TotalCount = _totalAlbums,
                            TotalPages = (int)Math.Ceiling((decimal)_totalAlbums / currentPageSize),
                            Items = _albumsDto
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
        /// Get photos of an album
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet()]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Response<List<PhotoDTO>>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(Response<List<PhotoDTO>>))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [CustomSwaggerOperation(typeof(PhotosController))]
        [Route("{albumId:int}/photos")]

        public ActionResult Get([FromRoute, Required] int albumId, int? page = 0, int? pageSize = 12)
        {
            Response<PaginationSet<PhotoDTO>> result = InvokeDataResponse(messagesContainer =>
            {
                PaginationSet<PhotoDTO> pagedSet = null;
                try
                {
                    int currentPage = page.Value;
                    int currentPageSize = pageSize.Value;
                    List<Photo> _photos = null;
                    int _totalPhotos = new int();
                    Album _album = _unitOfWork.AlbumRepository.GetSingle(a => a.Id == albumId, a => a.Photos);
                    _photos = _album
                                .Photos
                                .OrderBy(p => p.Id)
                                .Skip(currentPage * currentPageSize)
                                .Take(currentPageSize)
                                .ToList();

                    _totalPhotos = _album.Photos.Count();
                    IEnumerable<PhotoDTO> _photosDto = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoDTO>>(_photos);
                    pagedSet = new PaginationSet<PhotoDTO>()
                    {
                        Page = currentPage,
                        TotalCount = _totalPhotos,
                        TotalPages = (int)Math.Ceiling((decimal)_totalPhotos / currentPageSize),
                        Items = _photosDto
                    };
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
    }
}