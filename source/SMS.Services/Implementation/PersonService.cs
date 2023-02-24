using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DATA.Models;
using SMS.Services.Infrastructure;
using DTOPerson = SMS.DTOs.DTOs.Person;


namespace SMS.Services.Implementation
{
    public class PersonService : IPersonService
    {
        private readonly IRepository<Person> _repository;
        private readonly IRepository<AspNetUser> _aspNetUserrepository;
        private IMapper _mapper;
        public PersonService(IRepository<Person> repository, IMapper mapper, IRepository<AspNetUser> aspNetUserrepository)
        {
            _repository = repository;
            _mapper = mapper;
            _aspNetUserrepository = aspNetUserrepository;
        }
        #region SMS Section
        public List<DTOPerson> Get()
        {
            var people = _repository.Get().ToList();
            var peopleList = new List<DTOPerson>();
            foreach (var person in people)
            {
                peopleList.Add(_mapper.Map<Person, DTOPerson>(person));
            }
            return peopleList;
        }
        public DTOPerson Get(Guid? id)
        {
            if (id == null) return null;
            var personRecord = _repository.Get().FirstOrDefault(p => p.Id == id);
            if (personRecord == null) return null;

            return _mapper.Map<Person, DTOPerson>(personRecord);
        }

        public Guid Create(DTOPerson dtoPerson)
        {
            dtoPerson.CreatedDate = DateTime.UtcNow;
            dtoPerson.IsDeleted = false;

            if (dtoPerson.Id == Guid.Empty)
            {
                dtoPerson.Id = Guid.NewGuid();
            }
            _repository.Add(_mapper.Map<DTOPerson, Person>(dtoPerson));
            return dtoPerson.Id;
        }
        public void Update(DTOPerson dtoPerson)
        {
            var person = Get(dtoPerson.Id);
            dtoPerson.UpdateDate = DateTime.UtcNow;
            var mergedPerson = _mapper.Map(dtoPerson, person);
            _repository.Update(_mapper.Map<DTOPerson, Person>(mergedPerson));
        }
        public void Delete(Guid? id)
        {
            if (id == null)
                return;
            var person = Get(id);
            person.IsDeleted = true;
            person.DeletedDate = DateTime.UtcNow;
            _repository.Update(_mapper.Map<DTOPerson, Person>(person));
        }
        public bool GetAspNetUserByUsername(string username)
        {
            return _aspNetUserrepository.Get().Any(x => x.UserName.Equals(username));
        }
        #endregion

    }
}
