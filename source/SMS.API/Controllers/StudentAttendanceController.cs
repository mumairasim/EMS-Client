using Newtonsoft.Json;
using SMS.Services.Configurations;
using SMS.Services.Infrastructure;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using DTOStudentAttendance = SMS.DTOs.DTOs.StudentAttendance;

namespace SMS.API.Controllers
{
    [RoutePrefix("api/v1/StudentAttendance")]
    [EnableCors("*", "*", "*")]
    public class StudentAttendanceController : ApiController
    {
        public IStudentAttendanceService _studentAttendanceService;
        public StudentAttendanceController(IStudentAttendanceService studentAttendanceService)
        {
            _studentAttendanceService = studentAttendanceService;
        }
        #region SMS Section
        /// <summary>
        /// Fetch all the data
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(string searchString = "", int pageNumber = 1, int pageSize = 10)
        {
            var resSet = _studentAttendanceService.Get(searchString, pageNumber, pageSize);
            return Ok(resSet);
        }
        /// <summary>
        /// Fetch data on the basis of class and school 
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="schoolId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(Guid? classId, Guid? schoolId, int pageNumber = 1, int pageSize = 10)
        {
            return Ok(_studentAttendanceService.Get(classId, schoolId, pageNumber, pageSize));
        }
        /// <summary>
        /// Fetch data on the basis of class and school 
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="schoolId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search(Guid? classId, Guid? schoolId, int pageNumber = 1, int pageSize = 10)
        {
            var predicate = PredicateBuilder.True<DATA.Models.StudentAttendance>();
            predicate.And(sa => sa.IsDeleted == false);
            predicate.And(sa => sa.ClassId == classId);
            predicate.And(sa => sa.SchoolId == schoolId);
            return Ok(_studentAttendanceService.Search(predicate, pageNumber, pageSize));
        }
        /// <summary>
        /// Fetch by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>




        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_studentAttendanceService.Get(id));
        }
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create()
        {
            var httpRequest = HttpContext.Current.Request;
            var studentAttendanceDetail = JsonConvert.DeserializeObject<DTOStudentAttendance>(httpRequest.Params["studentAttendanceModel"]);
            studentAttendanceDetail.CreatedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            return Ok(_studentAttendanceService.Create(studentAttendanceDetail));
        }
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update()
        {
            var httpRequest = HttpContext.Current.Request;
            var studentAttendanceDetail = JsonConvert.DeserializeObject<DTOStudentAttendance>(httpRequest.Params["studentAttendanceModel"]);
            studentAttendanceDetail.UpdateBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            _studentAttendanceService.Update(studentAttendanceDetail);
            return Ok();
        }
        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(Guid id)
        {
            var deletedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            _studentAttendanceService.Delete(id, deletedBy);
            return Ok();
        }
        #endregion

    }
}
