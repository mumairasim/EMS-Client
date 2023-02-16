using Newtonsoft.Json;
using SMS.DTOs.DTOs;
using SMS.Services.Infrastructure;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SMS.API.Controllers
{
    [RoutePrefix("api/v1/Designation")]
    [EnableCors("*", "*", "*")]
    public class DesignationController : ApiController
    {
        public IDesignationService _designationService;
        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }

        #region SMS Section
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(string searchString = "", int pageNumber = 1, int pageSize = 10)
        {
            return Ok(_designationService.Get(searchString, pageNumber, pageSize));
        }
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_designationService.Get(id));
        }
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create()
        {
            var httpRequest = HttpContext.Current.Request;
            var dtoDesignation = JsonConvert.DeserializeObject<Designation>(httpRequest.Params["designationModel"]);
            dtoDesignation.CreatedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            _designationService.Create(dtoDesignation);
            return Ok();
        }
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update()
        {
            var httpRequest = HttpContext.Current.Request;
            var dtoDesignation = JsonConvert.DeserializeObject<Designation>(httpRequest.Params["designationModel"]);
            dtoDesignation.CreatedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            _designationService.Update(dtoDesignation);
            return Ok();
        }
        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(Guid id)
        {
            _designationService.Delete(id);
            return Ok();
        }
        #endregion

    }
}
