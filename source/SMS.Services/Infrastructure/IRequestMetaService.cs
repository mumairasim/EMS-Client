using SMS.DTOs.DTOs;
using SMS.DTOs.ReponseDTOs;
using System;
using DBRequestMeta = SMS.DATA.Models.RequestMeta;
using DTORequestMeta = SMS.DTOs.DTOs.RequestMeta;

namespace SMS.Services.Infrastructure
{
    public interface IRequestMetaService
    {
        GenericApiResponse Create(DBRequestMeta dBRequestMeta);
        DTORequestMeta Get(Guid? id);
        GenericApiResponse Update(DTORequestMeta dTORequestMeta);
        ServiceResponse<DTORequestMeta> Get(string searchString, int pageNumber, int pageSize);
    }
}