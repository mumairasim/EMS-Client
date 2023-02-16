using System;
using SMS.Services.Infrastructure;
using System.Web.Http;
using System.Web.Http.Cors;
using DTOAttendanceStatus = SMS.DTOs.DTOs.AttendanceStatus;

namespace SMS.API.Controllers
{
    [RoutePrefix("api/v1/AttendanceStatus")]
    [EnableCors("*", "*", "*")]
    public class AttendanceStatusController : ApiController
    {
        public IAttendanceStatusService AttendanceStatusService;
        public AttendanceStatusController(IAttendanceStatusService attendanceStatusService)
        {
            AttendanceStatusService = attendanceStatusService;
        }
        #region SMS Section
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get()
        {
            return Ok(AttendanceStatusService.Get());
        }
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(AttendanceStatusService.Get(id));
        }
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create(DTOAttendanceStatus dtoAttendanceStatus)
        {
            AttendanceStatusService.Create(dtoAttendanceStatus);
            return Ok();
        }
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update(DTOAttendanceStatus dtoAttendanceStatus)
        {
            AttendanceStatusService.Update(dtoAttendanceStatus);
            return Ok();
        }
        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(Guid id)
        {
            AttendanceStatusService.Delete(id);
            return Ok();
        }
        #endregion

    }
}
