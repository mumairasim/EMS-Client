
using System;
using System.IO;
using System.Linq;
using System.Web;
using SMS.Services.Infrastructure;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using SMS.DTOs.DTOs;
using DTOLessonPlan = SMS.DTOs.DTOs.LessonPlan;


namespace SMS.API.Controllers
{
    [RoutePrefix("api/v1/LessonPlan")]
    [EnableCors("*", "*", "*")]
    public class LessonPlanController : ApiController
    {
        public ILessonPlanService _lessonplanService;
        public LessonPlanController(ILessonPlanService lessonplanService)
        {
            _lessonplanService = lessonplanService;
        }

        #region SMS Section
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(_lessonplanService.Get(pageNumber, pageSize));
        }

        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_lessonplanService.Get(id));
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create()
        {
            var httpRequest = HttpContext.Current.Request;
            var lessonPlanDetail = JsonConvert.DeserializeObject<DTOLessonPlan>(httpRequest.Params["lessonPlanModel"]);
            lessonPlanDetail.CreatedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            return Ok(_lessonplanService.Create(lessonPlanDetail));

        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update()
        {
            var httpRequest = HttpContext.Current.Request;
            var lessonPlanDetail = JsonConvert.DeserializeObject<DTOLessonPlan>(httpRequest.Params["lessonPlanModel"]);
            lessonPlanDetail.UpdateBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            return Ok(_lessonplanService.Update(lessonPlanDetail));
        }

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(Guid id)
        {
            var DeletedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            _lessonplanService.Delete(id, DeletedBy);
            return Ok();
        }
        #endregion

    }
}

