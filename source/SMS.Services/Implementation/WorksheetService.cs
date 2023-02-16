using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DTOs.DTOs;
using SMS.DTOs.ReponseDTOs;
using SMS.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using DBWorksheet = SMS.DATA.Models.Worksheet;
using DTOWorksheet = SMS.DTOs.DTOs.Worksheet;

namespace SMS.Services.Implementation
{
    public class WorksheetService : IWorksheetService
    {
        #region Properties
        private readonly IRepository<DBWorksheet> _repository;
        private const string error_not_found = "Record not found";
        private const string server_error = "Server error";

        private IMapper _mapper;
        #endregion

        #region Init

        public WorksheetService(IRepository<DBWorksheet> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #endregion

        #region SMS

        /// <summary>
        /// Service level call : Creates a single record of a Worksheet
        /// </summary>
        /// <param name="dTOWorksheet"></param>
        public GenericApiResponse Create(DTOWorksheet dTOWorksheet)
        {
            try
            {
                dTOWorksheet.CreatedDate = DateTime.UtcNow;
                dTOWorksheet.IsDeleted = false;

                //below check is to create request type instances with the same Ids in both DBs, 
                //if request is from front end then assign a new Id
                if (dTOWorksheet.Id == Guid.Empty)
                {
                    dTOWorksheet.Id = Guid.NewGuid();
                }

                _repository.Add(_mapper.Map<DTOWorksheet, DBWorksheet>(dTOWorksheet));
                return PrepareSuccessResponse("Created", "Instance Created Successfully");

            }
            catch (Exception)
            {
                return PrepareFailureResponse("Error", server_error);
            }
        }

        /// <summary>
        /// Service level call : Delete a single record of a Worksheet
        /// </summary>
        /// <param name="id"></param>
        public GenericApiResponse Delete(Guid? id)
        {
            try
            {
                if (id == null)
                    return null;
                var worksheet = Get(id);
                if (worksheet != null)
                {
                    worksheet.IsDeleted = true;
                    worksheet.DeletedDate = DateTime.UtcNow;
                    _repository.Update(_mapper.Map<DTOWorksheet, DBWorksheet>(worksheet));
                    return PrepareSuccessResponse("Deleted", "Instance Deleted Successfully");
                }
                return PrepareFailureResponse("Error", error_not_found);
            }
            catch (Exception)
            {
                return PrepareFailureResponse("Error", server_error);
            }

        }

        /// <summary>
        /// Retruns a Single Record of a Worksheet
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DTOWorksheet Get(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var worksheet = _repository.Get().FirstOrDefault(x => x.Id == id && (x.IsDeleted == false || x.IsDeleted == null));
            var worksheetDto = _mapper.Map<DBWorksheet, DTOWorksheet>(worksheet);

            return worksheetDto;
        }

        /// <summary>
        /// Service level call : Updates the Single Record of a Worksheet 
        /// </summary>
        /// <param name="dtoWorksheet"></param>
        public GenericApiResponse Update(DTOWorksheet dtoWorksheet)
        {
            try
            {
                var worksheet = Get(dtoWorksheet.Id);
                if (worksheet != null)
                {
                    dtoWorksheet.UpdateDate = DateTime.UtcNow;
                    dtoWorksheet.IsDeleted = false;
                    var updated = _mapper.Map(dtoWorksheet, worksheet);
                    var updatedDbRec = _mapper.Map<DTOWorksheet, DBWorksheet>(updated);
                    _repository.Update(updatedDbRec);
                    return PrepareSuccessResponse("Updated", "Instance Updated Successfully");
                }
                return PrepareFailureResponse("Error", error_not_found);
            }
            catch (Exception)
            {
                return PrepareFailureResponse("Error", server_error);
            }
        }

        /// <summary>
        /// Service level call : Return all records of a Worksheet
        /// </summary>
        /// <returns></returns>
        public ServiceResponse<DTOWorksheet> Get(string searchString, int pageNumber, int pageSize)
        {
            var resultSet = _repository.Get()
             .Where(cl => string.IsNullOrEmpty(searchString) || cl.Text.ToLower().Contains(searchString.ToLower()))
             .Union(_repository.Get().Where(cl => string.IsNullOrEmpty(searchString) || cl.School.Name.ToLower().Contains(searchString.ToLower())))
             .Union(_repository.Get().Where(cl => string.IsNullOrEmpty(searchString) || cl.Employee.Person.FirstName.ToLower().Contains(searchString.ToLower())))
             .Union(_repository.Get().Where(cl => string.IsNullOrEmpty(searchString) || cl.Employee.Person.LastName.ToLower().Contains(searchString.ToLower())))
             .Where(cl => cl.IsDeleted == false).OrderByDescending(st => st.Id).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var resultCount = resultSet.Count;
            var tempList = new List<DTOWorksheet>();
            foreach (var item in resultSet)
            {
                tempList.Add(_mapper.Map<DBWorksheet, DTOWorksheet>(item));
            }
            var finalList = new ServiceResponse<DTOWorksheet>()
            {
                Items = tempList,
                Count = resultCount
            };
            return finalList;
        }

        #endregion





        #region Utils
        private GenericApiResponse PrepareFailureResponse(string errorMessage, string descriptionMessage)
        {
            return new GenericApiResponse
            {
                StatusCode = "400",
                Message = errorMessage,
                Description = descriptionMessage
            };
        }
        private GenericApiResponse PrepareSuccessResponse(string message, string descriptionMessage)
        {
            return new GenericApiResponse
            {
                StatusCode = "200",
                Message = message,
                Description = descriptionMessage
            };
        }

        #endregion
    }
}
