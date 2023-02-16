using Newtonsoft.Json;
using SMS.DTOs.DTOs;
using SMS.Services.Infrastructure;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SMS.API.Controllers
{
    [RoutePrefix("api/v1/RequestMeta")]
    public class RequestMetaController : ApiController
    {
        #region Props and Init
        public IRequestMetaService _requestMetaService;

        public RequestMetaController(IRequestMetaService requestMetaService)
        {
            _requestMetaService = requestMetaService;
        }
        #endregion

        #region SMS

        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(Guid id)
        {
            if (id == null)
            {
                return BadRequest("No Id Recieved");
            }

            try
            {
                var result = _requestMetaService.Get(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(string searchString = "", int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var studentList = _requestMetaService.Get(searchString, pageNumber, pageSize);
                return Ok(studentList);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        //[HttpPost]
        //[Route("Create")]
        //public IHttpActionResult Create()
        //{
        //    var httpRequest = HttpContext.Current.Request;
        //    var requestMeta = JsonConvert.DeserializeObject<RequestMeta>(httpRequest.Params["requestMetaModel"]);
        //    requestMeta.CreatedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
        //    try
        //    {
        //        _requestMetaService.Create(requestMeta);
        //    }
        //    catch (Exception e)
        //    {
        //        return InternalServerError();
        //    }
        //    return Ok();
        //}

        //[HttpPut]
        //[Route("Update")]
        //public IHttpActionResult Update()
        //{
        //    var httpRequest = HttpContext.Current.Request;
        //    var requestMeta = JsonConvert.DeserializeObject<RequestMeta>(httpRequest.Params["requestMetaModel"]);
        //    requestMeta.UpdateBy = Request.Headers.GetValues("UserName").FirstOrDefault();

        //    try
        //    {
        //        _requestMetaService.Update(requestMeta);
        //    }
        //    catch (Exception)
        //    {
        //        return InternalServerError();
        //    }
        //    return Ok();
        //}


        #endregion

    }
}
