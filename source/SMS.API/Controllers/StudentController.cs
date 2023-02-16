using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using SMS.Services.Infrastructure;
using DTOStudent = SMS.DTOs.DTOs.Student;

namespace SMS.API.Controllers
{
    [RoutePrefix("api/v1/Student")]
    [EnableCors("*", "*", "*")]
    public class StudentController : ApiController
    {
        private readonly IStudentService _studentService;
        private readonly IFileService _fileService;
        public StudentController(IStudentService studentService, IFileService fileService)
        {
            _studentService = studentService;
            _fileService = fileService;
        }
        #region SMS Section

        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(int pageNumber = 1, int pageSize = 10)
        {
            return Ok(_studentService.Get(pageNumber, pageSize));
        }
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(string searchString, int pageNumber = 1, int pageSize = 10)
        {
            return Ok(_studentService.Get(searchString, pageNumber, pageSize));
        }
        [HttpGet]
        [Route("GetRegNo")]
        public IHttpActionResult GetRegNo(int registrationNumber, int pageNumber = 1, int pageSize = 10)
        {
            return Ok(_studentService.Get(registrationNumber, pageNumber, pageSize));
        }
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_studentService.Get(id));
        }
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(Guid classId, Guid schoolId)
        {
            return Ok(_studentService.Get(classId, schoolId));
        }
        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create()
        {
            var httpRequest = HttpContext.Current.Request;
            var studentDetail = JsonConvert.DeserializeObject<DTOStudent>(httpRequest.Params["studentModel"]);
            studentDetail.CreatedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            studentDetail.Person.CreatedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            if (httpRequest.Files.Count > 0)
            {
                var file = httpRequest.Files[0];
                studentDetail.ImageId = _fileService.Create(file);
            }
            return Ok(_studentService.Create(studentDetail));
        }
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update()
        {
            var httpRequest = HttpContext.Current.Request;
            var studentDetail = JsonConvert.DeserializeObject<DTOStudent>(httpRequest.Params["studentModel"]);
            studentDetail.UpdateBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            studentDetail.Person.UpdateBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            if (httpRequest.Files.Count > 0)
            {
                var file = httpRequest.Files[0];
                studentDetail.ImageId = _fileService.Update(file, studentDetail.Image.Id);
            }
            return Ok(_studentService.Update(studentDetail));
        }
        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(Guid id)
        {
            var deletedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            _studentService.Delete(id, deletedBy);
            return Ok();
        }
        #endregion

    }
}