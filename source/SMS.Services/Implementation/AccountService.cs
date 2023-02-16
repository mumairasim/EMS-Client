using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DATA.Models;
using SMS.Services.Infrastructure;
using System;
using System.Linq;
using DTOPerson = SMS.DTOs.DTOs.Person;
using DTOUserInfo = SMS.DTOs.DTOs.UserInfo;

namespace SMS.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IPersonService _personService;
        private readonly IRepository<Person> _repositoryPerson;
        private readonly IRepository<File> _repositoryFile;
        private readonly IRepository<AspNetUser> _repositoryAspNetUser;
        private readonly IMapper _mapper;

        public AccountService(IPersonService personService,
            IRepository<Person> repositoryPerson,
            IRepository<File> repositoryFile,
            IMapper mapper,
            IRepository<AspNetUser> repositoryAspNetUser)
        {
            _repositoryAspNetUser = repositoryAspNetUser;
            _personService = personService;
            _repositoryPerson = repositoryPerson;
            _repositoryFile = repositoryFile;
            _mapper = mapper;
        }

        public DTOUserInfo GetUserInfo(string userName)
        {

            var userInfoDto = (from person in _repositoryPerson.Get()
                               join aspNetUser in _repositoryAspNetUser.Get().Where(x => x.UserName.Trim().ToLower() == userName.Trim().ToLower()) on person.AspNetUserId equals aspNetUser.Id
                               join img in _repositoryFile.Get() on person.ImageId equals img.Id into tempImage
                               from img in tempImage.DefaultIfEmpty()
                               select new DTOUserInfo
                               {
                                   ImageId = img != null ? img.Id : Guid.Empty,
                                   ImageName = img.Name,
                                   ImageExtension = img.Extension,
                                   ImagePath = img.Path,
                                   ImageSize = img.Size,
                                   PersonId = person.Id,
                                   FirstName = person.FirstName,
                                   LastName = person.LastName,
                                   Email = aspNetUser.Email,
                                   UserName = aspNetUser.UserName,
                                   PermanentAddress = person.PermanentAddress,
                                   Phone = person.Phone,
                                   CreationDate = person.CreatedDate
                               }).FirstOrDefault();

            if (!string.IsNullOrEmpty(userInfoDto.ImagePath))
            {
                userInfoDto.Image = System.IO.File.ReadAllBytes(userInfoDto.ImagePath);
                userInfoDto.ImageExtension = System.IO.Path.GetExtension(userInfoDto.ImagePath);
            }
            return userInfoDto;
        }
        public void UpdateUserInfo(DTOUserInfo userInfo)
        {
            var person = _mapper.Map<DTOUserInfo, DTOPerson>(userInfo);
            _personService.Update(person);
        }
    }
}
