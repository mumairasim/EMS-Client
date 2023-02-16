using System;
using System.Collections.Generic;
using System.Web;
using DTOFile = SMS.DTOs.DTOs.File;

namespace SMS.Services.Infrastructure
{
    public interface IFileService
    {
        /// <summary>
        /// Service level call : Return all records of a File
        /// </summary>
        /// <returns></returns>
        List<DTOFile> GetAll();

        /// <summary>
        /// Retruns a Single Record of a File
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DTOFile Get(Guid? id);

        /// <summary>
        /// Service level call : Creates a single record of a File
        /// </summary>
        /// <param name="dtoFile"></param>
        Guid Create(DTOFile dTOFile);

        /// <summary>
        /// Service level call : Creates a single record of a File and save the file in directory
        /// </summary>
        /// <param name="file"></param>
        Guid? Create(HttpPostedFile file);

        /// <summary>
        /// Service level call : Updates the Single Record of a File and actual image
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileId"></param>
        Guid? Update(HttpPostedFile file, Guid fileId);

        /// <summary>
        /// Service level call : Delete a single record of a File
        /// </summary>
        /// <param name="id"></param>
        void Delete(Guid? id);
    }
}
