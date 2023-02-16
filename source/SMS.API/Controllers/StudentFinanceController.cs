using Newtonsoft.Json;
using SMS.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DTOStudentFinances = SMS.DTOs.DTOs.Student_Finances;
using DTOStudentFinancesInfo = SMS.DTOs.DTOs.StudentFinanceInfo;

namespace SMS.API.Controllers
{
    [RoutePrefix("api/v1/StudentFinance")]
    public class StudentFinanceController : ApiController
    {
        #region Props and Init
        public IStudentFinanceService _studentFinanceService;

        public StudentFinanceController(IStudentFinanceService studentFinanceService)
        {
            _studentFinanceService = studentFinanceService;
        }
        #endregion

        #region API Calls
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
                var result = _studentFinanceService.Get(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            try
            {
                var result = _studentFinanceService.GetAll();
                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetByFilter/{schoolId}/{classId}/{feeMonth}")]
        public IHttpActionResult GetByFilter(Guid? schoolId = null, Guid? classId = null, Guid? studentId = null, string feeMonth = "0")
        {
            if (feeMonth == "0")
            {
                feeMonth = null;
            }
            try
            {
                var result = _studentFinanceService.GetByFilter(schoolId, classId, studentId, feeMonth);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetDetailByFilter")]
        public IHttpActionResult GetDetailByFilter(Guid? schoolId = null, Guid? classId = null, int? regno = null, string month = null, string year = null)
        {
            try
            {
                var result = _studentFinanceService.GetDetailByFilter(schoolId, classId, regno, month, year);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }


        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create()
        {
            var httpRequest = HttpContext.Current.Request;
            var financeInfoList = JsonConvert.DeserializeObject<List<DTOStudentFinancesInfo>>(httpRequest.Params["form"]);

            foreach (var item in financeInfoList)
            {
                item.CreatedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
                _studentFinanceService.Create(item);
            }

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update()
        {
            var httpRequest = HttpContext.Current.Request;
            var dTOStudentFinances = JsonConvert.DeserializeObject<DTOStudentFinancesInfo>(httpRequest.Params["studentFinanceModel"]);
            dTOStudentFinances.CreatedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            try
            {
                _studentFinanceService.Update(dTOStudentFinances);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return BadRequest("No Id Recieved");
            }

            try
            {
                _studentFinanceService.Delete(id);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            return Ok();
        }
        #endregion

    }
}
