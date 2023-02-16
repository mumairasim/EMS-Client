using Newtonsoft.Json;
using SMS.Services.Infrastructure;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using DTOFinanceType = SMS.DTOs.DTOs.FinanceType;

namespace SMS.API.Controllers
{
    [RoutePrefix("api/v1/FinanceType")]
    [EnableCors("*", "*", "*")]
    public class FinanceTypeController : ApiController
    {
        private readonly IFinanceTypeService _financeTypeService;
        public FinanceTypeController(IFinanceTypeService financeTypeService)
        {
            _financeTypeService = financeTypeService;
        }
        #region SMS Section
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(string searchString = "", int pageNumber = 1, int pageSize = 10)
        {
            var reseltSet = _financeTypeService.Get(searchString, pageNumber, pageSize);
            return Ok(reseltSet);
        }

        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_financeTypeService.Get(id));
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Create()
        {
            var httpRequest = HttpContext.Current.Request;
            var financeTypeDetail = JsonConvert.DeserializeObject<DTOFinanceType>(httpRequest.Params["financeTypeModel"]);
            financeTypeDetail.CreatedBy = Request.Headers.GetValues("UserName").FirstOrDefault();

            _financeTypeService.Create(financeTypeDetail);
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update()
        {
            var httpRequest = HttpContext.Current.Request;
            var financeTypeDetail = JsonConvert.DeserializeObject<DTOFinanceType>(httpRequest.Params["financeTypeModel"]);
            financeTypeDetail.UpdateBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            _financeTypeService.Update(financeTypeDetail);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(Guid id)
        {
            var deletedBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            _financeTypeService.Delete(id, deletedBy);
            return Ok();
        }
        #endregion

    }
}
