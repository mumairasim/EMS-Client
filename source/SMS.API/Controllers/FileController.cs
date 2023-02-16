using SMS.Services.Infrastructure;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Http;
using Newtonsoft.Json;
using SMS.DTOs.DTOs;
using SMS.Services.Implementation;
using DTOFile = SMS.DTOs.DTOs.File;


namespace SMS.API.Controllers
{
    [RoutePrefix("api/v1/File")]
    public class FileController : ApiController
    {

        #region Props and Init
        public IFileService FileService;

        public FileController(IFileService fileService)
        {
            FileService = fileService;
        }

        #endregion

        #region API Calls
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult Get(Guid id)
        {
            try
            {
                var result = FileService.Get(id);
                result.Path = string.Empty;
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
                var result = FileService.GetAll();
                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("Save")]
        public IHttpActionResult SaveFile()
        {
            string message, fileName, actualFileName, path;
            message = fileName = actualFileName = path = string.Empty;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files != null)
            {
                var file = httpRequest.Files[0];
                actualFileName = file.FileName;
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                int size = file.ContentLength;

                try
                {

                    path = Path.Combine(HostingEnvironment.MapPath(WebConfigurationManager.AppSettings["FileUploadFolder"]));

                    bool exists = Directory.Exists(path);
                    if (!exists)
                        Directory.CreateDirectory(path);

                    path = path + fileName;
                    file.SaveAs(path);
                    DTOFile newFile = new DTOFile
                    {
                        Name = fileName,
                        Path = path,
                        Size = size,
                        IsDeleted = false
                    };

                    FileService.Create(newFile);


                }
                catch (Exception)
                {

                    throw;
                }
            }
            return Ok();
        }
        [HttpPost]
        [Route("Update")]
        public IHttpActionResult Update()
        {
            var httpRequest = HttpContext.Current.Request;
            var fileDetail = JsonConvert.DeserializeObject<DTOFile>(httpRequest.Params["fileModel"]);
            fileDetail.UpdateBy = Request.Headers.GetValues("UserName").FirstOrDefault();
            if (httpRequest.Files.Count > 0)
            {
                var file = httpRequest.Files[0];
                FileService.Update(file, fileDetail.Id);
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
                FileService.Delete(id);
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
