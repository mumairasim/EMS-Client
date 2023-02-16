using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DTOs.DTOs;
using SMS.DTOs.ReponseDTOs;
using SMS.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using DBRequestMeta = SMS.DATA.Models.RequestMeta;
using DTORequestMeta = SMS.DTOs.DTOs.RequestMeta;

namespace SMS.Services.Implementation
{
    public class RequestMetaService : IRequestMetaService
    {
        #region Properties
        private readonly IRepository<DBRequestMeta> _repository;
        private const string error_not_found = "Record not found";
        private const string server_error = "Server error";

        private IMapper _mapper;
        #endregion

        #region Init

        public RequestMetaService(IRepository<DBRequestMeta> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #endregion

        #region SMS

        /// <summary>
        /// Service level call : Creates a single record of a RequestMeta
        /// </summary>
        /// <param name="dBRequestMeta"></param>
        public GenericApiResponse Create(DBRequestMeta dBRequestMeta)
        {
            try
            {
                dBRequestMeta.CreatedDate = DateTime.UtcNow;
                dBRequestMeta.IsDeleted = false;

                if (dBRequestMeta.Id == Guid.Empty)
                {
                    dBRequestMeta.Id = Guid.NewGuid();
                }
                _repository.Add(dBRequestMeta);
                return PrepareSuccessResponse("Created", "Instance Created Successfully");

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
        public DTORequestMeta Get(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var result = _repository.Get().FirstOrDefault(x => x.Id == id && (x.IsDeleted == false || x.IsDeleted == null));
            var resultDto = _mapper.Map<DBRequestMeta, DTORequestMeta>(result);

            return resultDto;
        }

        /// <summary>
        /// Service level call : Updates the Single Record of a Worksheet 
        /// </summary>
        /// <param name="dTORequestMeta"></param>
        public GenericApiResponse Update(DTORequestMeta dTORequestMeta)
        {
            try
            {
                var result = Get(dTORequestMeta.Id);
                if (result != null)
                {
                    dTORequestMeta.UpdateDate = DateTime.UtcNow;
                    dTORequestMeta.IsDeleted = false;
                    var updated = _mapper.Map(dTORequestMeta, result);
                    var updatedDbRec = _mapper.Map<DTORequestMeta, DBRequestMeta>(updated);
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
        public ServiceResponse<DTORequestMeta> Get(string searchString, int pageNumber, int pageSize)
        {
            var resultSet = _repository.GetRaw()
             .Where(cl => (string.IsNullOrEmpty(searchString) || cl.School.Name.ToLower().Contains(searchString.ToLower()))
             && cl.IsDeleted == false)
             .OrderByDescending(st => st.CreatedDate).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var resultCount = resultSet.Count;
            var tempList = new List<DTORequestMeta>();
            foreach (var item in resultSet)
            {
                tempList.Add(_mapper.Map<DBRequestMeta, DTORequestMeta>(item));
            }
            var finalList = new ServiceResponse<DTORequestMeta>()
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
