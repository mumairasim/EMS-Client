using System;
using System.Collections.Generic;
using DTOPerson = SMS.DTOs.DTOs.Person;

namespace SMS.Services.Infrastructure
{
    public interface IPersonService
    {
        #region SMS Section
        List<DTOPerson> Get();
        DTOPerson Get(Guid? id);
        Guid Create(DTOPerson dtoPerson);
        void Update(DTOPerson dtoPerson);
        void Delete(Guid? id);
        #endregion

    }
}
