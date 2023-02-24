using Newtonsoft.Json;
using SMS.Services.Infrastructure;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using DTOSchool = SMS.DTOs.DTOs.School;

namespace SMS.API.Controllers
{
    [RoutePrefix("api/v1/School")]
    [EnableCors("*", "*", "*")]
    public class SchoolController : ApiController
    {
        public ISchoolService _schoolService;
        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        #region SMS Section
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(_schoolService.Get(pageNumber, pageSize));
        }
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(string searchString, int pageNumber, int pageSize)
        {
            return Ok(_schoolService.Get(searchString, pageNumber, pageSize));
        }
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_schoolService.Get(id));
        }
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create()
        {

            var httpRequest = HttpContext.Current.Request;
            var schoolDetail = JsonConvert.DeserializeObject<DTOSchool>(httpRequest.Params["schoolModel"]);
            schoolDetail.CreatedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            schoolDetail.CreatedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            _schoolService.Create(schoolDetail);
            return Ok();
        }
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update()
        {
            var httpRequest = HttpContext.Current.Request;
            var schoolDetail = JsonConvert.DeserializeObject<DTOSchool>(httpRequest.Params["schoolModel"]);
            schoolDetail.UpdateBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            _schoolService.Update(schoolDetail);
            return Ok();
        }
        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(Guid id)
        {
            var DeletedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            _schoolService.Delete(id, DeletedBy);
            return Ok();
        }

        [HttpPost]
        [Route("Register")]
        public IHttpActionResult Register()
        {
            var httpRequest = HttpContext.Current.Request;
            var schoolDetail = JsonConvert.DeserializeObject<DTOSchool>(httpRequest.Params["schoolModel"]);
            if (_schoolService.IsAlreadyRegistered())
            {
                return BadRequest("School already registered");
            }
            if (_schoolService.Register(schoolDetail))
            {
                return Ok();
            }
            return BadRequest("Can't register your school please contact head office.");
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("IsSchoolRegistered")]

        public IHttpActionResult IsSchoolRegistered()
        {
            return Ok(_schoolService.IsAlreadyRegistered());
        }
        #endregion

    }
}