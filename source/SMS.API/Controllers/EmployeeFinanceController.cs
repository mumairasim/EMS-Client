using Newtonsoft.Json;
using SMS.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using DTOEmployeeFinanceInfo = SMS.DTOs.DTOs.EmployeeFinanceInfo;

namespace SMS.API.Controllers
{
    [RoutePrefix("api/v1/EmployeeFinance")]
    [EnableCors("*", "*", "*")]
    public class EmployeeFinanceController : ApiController
    {
        #region Props and Init
        public IEmployeeFinanceService _empFinanceService;

        public EmployeeFinanceController(IEmployeeFinanceService empFinanceService)
        {
            _empFinanceService = empFinanceService;
        }
        #endregion

        #region SMS Section
        [HttpGet]
        [Route("GetByFilter/{schoolId}/{SalaryMonth}")]
        public IHttpActionResult GetByFilter(Guid? schoolId = null, Guid? designationId = null, string SalaryMonth = "0")
        {
            if (SalaryMonth == "0")
            {
                SalaryMonth = null;
            }
            try
            {
                var result = _empFinanceService.GetByFilter(schoolId, designationId, SalaryMonth);
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetDetailByFilter/{schoolId}")]
        public IHttpActionResult GetDetailByFilter(Guid? schoolId = null, Guid? designationId = null)
        {
            try
            {
                //var result = _empFinanceService.GetByFilter(schoolId, designationId, string.Empty);

                var result = _empFinanceService.GetDetailByFilter(schoolId, designationId);
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
            var financeInfoList = JsonConvert.DeserializeObject<List<DTOEmployeeFinanceInfo>>(httpRequest.Params["form"]);

            foreach (var item in financeInfoList)
            {
                item.CreatedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
                _empFinanceService.Create(item);
            }

            return Ok();
        }

        #endregion
    }
}
