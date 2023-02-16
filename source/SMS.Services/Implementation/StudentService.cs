using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DTOs.DTOs;
using SMS.DTOs.ReponseDTOs;
using SMS.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DTOStudent = SMS.DTOs.DTOs.Student;
using DTOStudentFinanceDetail = SMS.DTOs.DTOs.StudentFinanceDetail;
using Student = SMS.DATA.Models.Student;
using RequestMeta = SMS.DATA.Models.RequestMeta;

namespace SMS.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _repository;
        private readonly IPersonService _personService;
        private readonly IStudentFinanceDetailsService _studentFinanceDetailsService;
        private readonly IFinanceTypeService _financeTypeService;
        private readonly IMapper _mapper;
        public IRequestMetaService _requestMetaService;
        public StudentService(IRepository<Student> repository, IPersonService personService, IFinanceTypeService financeTypeService, IStudentFinanceDetailsService studentFinanceDetailsService, IMapper mapper, IRequestMetaService requestMetaService)
        {
            _repository = repository;
            _personService = personService;
            _studentFinanceDetailsService = studentFinanceDetailsService;
            _financeTypeService = financeTypeService;
            _mapper = mapper;
            _requestMetaService = requestMetaService;
        }

        #region SMS Section
        public StudentsList Get(int pageNumber, int pageSize)
        {
            var students = _repository.Get().Where(st => st.IsDeleted == false).OrderByDescending(st => st.RegistrationNumber).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var studentCount = _repository.Get().Count(st => st.IsDeleted == false);
            var studentTempList = new List<DTOStudent>();
            foreach (var student in students)
            {
                studentTempList.Add(_mapper.Map<Student, DTOStudent>(student));
            }

            var studentsList = new StudentsList()
            {
                Students = studentTempList,
                StudentsCount = studentCount
            };

            return studentsList;
        }
        public StudentsList Get(string searchString, int pageNumber, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return Get(pageNumber, pageSize);
            var students = _repository.Get().Where(st =>
                (
                    st.RegistrationNumber.ToString().Equals(searchString) ||
                    st.Person.Cnic.Contains(searchString) ||
                    st.Person.Phone.Equals(searchString) ||
                    st.Person.FirstName.Contains(searchString) ||
                    st.Person.LastName.Contains(searchString) ||
                    st.Person.Phone.Contains(searchString)
                ) &&
                st.IsDeleted == false
                ).OrderByDescending(st => st.RegistrationNumber).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var studentCount = _repository.Get().Count(st => (
                                                                 st.RegistrationNumber.ToString().Equals(searchString) ||
                                                                 st.Person.Cnic.Contains(searchString) ||
                                                                 st.Person.Phone.Equals(searchString) ||
                                                                 st.Person.FirstName.Contains(searchString) ||
                                                                 st.Person.LastName.Contains(searchString) ||
                                                                 st.Person.Phone.Contains(searchString)
                                                             ) && st.IsDeleted == false);
            var studentTempList = new List<DTOStudent>();
            foreach (var student in students)
            {
                studentTempList.Add(_mapper.Map<Student, DTOStudent>(student));
            }

            var studentsList = new StudentsList()
            {
                Students = studentTempList,
                StudentsCount = studentCount
            };

            return studentsList;
        }
        public StudentsList Get(int registrationNumber, int pageNumber, int pageSize)
        {
            var students = _repository.Get().Where(st => st.RegistrationNumber.Equals(registrationNumber) && st.IsDeleted == false).OrderByDescending(st => st.RegistrationNumber).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var studentCount = _repository.Get().Count(st => st.RegistrationNumber.Equals(registrationNumber) && st.IsDeleted == false);
            var studentTempList = new List<DTOStudent>();
            foreach (var student in students)
            {
                studentTempList.Add(_mapper.Map<Student, DTOStudent>(student));
            }

            var studentsList = new StudentsList()
            {
                Students = studentTempList,
                StudentsCount = studentCount
            };

            return studentsList;
        }

        public DTOStudent Get(Guid? id)
        {
            if (id == null) return null;
            var studentRecord = _repository.Get().FirstOrDefault(st => st.Id == id && st.IsDeleted == false);
            var student = _mapper.Map<Student, DTOStudent>(studentRecord);

            student.MonthlyFee = _studentFinanceDetailsService.GetByFeeType(student.Id, "Monthly") != null ? _studentFinanceDetailsService.GetByFeeType(student.Id, "Monthly").Fee : 0;
            student.AdmissionFee = _studentFinanceDetailsService.GetByFeeType(student.Id, "Admission") != null ? _studentFinanceDetailsService.GetByFeeType(student.Id, "Admission").Fee : 0;
            return student;
        }
        public StudentsList Get(Guid classId, Guid schoolId)
        {
            var students = _repository.Get().Where(st => st.IsDeleted == false && st.SchoolId == schoolId && st.ClassId == classId).OrderByDescending(st => st.RegistrationNumber).ToList();
            var studentCount = _repository.Get().Count(st => st.IsDeleted == false);
            var studentTempList = new List<DTOStudent>();
            foreach (var student in students)
            {
                studentTempList.Add(_mapper.Map<Student, DTOStudent>(student));
            }

            var studentsList = new StudentsList()
            {
                Students = studentTempList,
                StudentsCount = studentCount
            };

            return studentsList;
        }

        public StudentResponse Create(DTOStudent dtoStudent)
        {
            var validationResult = Validation(dtoStudent);
            if (validationResult.IsError)
            {
                return validationResult;
            }
            dtoStudent.CreatedDate = DateTime.UtcNow;
            dtoStudent.IsDeleted = false;
            if (dtoStudent.Id == Guid.Empty)
            {
                dtoStudent.Id = Guid.NewGuid();
            }
            dtoStudent.PersonId = _personService.Create(dtoStudent.Person);
            HelpingMethodForRelationship(dtoStudent);
            _repository.Add(_mapper.Map<DTOStudent, Student>(dtoStudent));
            InsertStudentFinanceDetail(dtoStudent);
            if (dtoStudent.IsClient == true)
            {
                _requestMetaService.Create(new RequestMeta
                {
                    ModuleId = dtoStudent.Id,
                    SchoolId = dtoStudent.SchoolId,
                    ModuleName = DATA.Models.Enums.Module.Student,
                    ApprovalStatus = DATA.Models.Enums.RequestStatus.Pending,
                    Type = DATA.Models.Enums.RequestType.Create
                });
            }
            return validationResult;
        }

        public StudentResponse Update(DTOStudent dtoStudentUpdatedState)
        {
            var validationResult = Validation(dtoStudentUpdatedState);
            if (validationResult.IsError)
            {
                return validationResult;
            }
            var dtoStudentCurrentState = Get(dtoStudentUpdatedState.Id);
            dtoStudentUpdatedState.UpdateDate = DateTime.UtcNow;
            var mergedStudent = _mapper.Map(dtoStudentUpdatedState, dtoStudentCurrentState);
            _personService.Update(mergedStudent.Person);
            HelpingMethodForRelationship(dtoStudentUpdatedState);
            _repository.Update(_mapper.Map<DTOStudent, Student>(mergedStudent));
            UpsertFinanceDetailsAgainstStudent(dtoStudentUpdatedState, dtoStudentCurrentState);
            if (dtoStudentUpdatedState.IsClient == true)
            {
                _requestMetaService.Create(new RequestMeta
                {
                    ModuleId = dtoStudentUpdatedState.Id,
                    SchoolId = dtoStudentUpdatedState.SchoolId,
                    ModuleName = DATA.Models.Enums.Module.Student,
                    ApprovalStatus = DATA.Models.Enums.RequestStatus.Pending,
                    Type = DATA.Models.Enums.RequestType.Update
                });
            }
            return validationResult;
        }

        private void UpsertFinanceDetailsAgainstStudent(DTOStudent dtoStudentUpdatedState, DTOStudent dtoStudentCurrentState)
        {
            var finances = _studentFinanceDetailsService.GetByStudentId(dtoStudentCurrentState.Id);
            var typeNames = new string[] { "Admission", "Monthly" };
            if (finances.Count == 0)
            {
                InsertStudentFinanceDetail(dtoStudentUpdatedState);
            }
            else
            {
                foreach (var finance in finances)
                {
                    var financeType = _financeTypeService.Get(finance.FinanceTypeId);
                    switch (financeType.Type)
                    {
                        case "Admission":
                            finance.Fee = dtoStudentUpdatedState.AdmissionFee;
                            break;
                        case "Monthly":
                            finance.Fee = dtoStudentUpdatedState.MonthlyFee;
                            break;
                    }
                    _studentFinanceDetailsService.Update(finance);
                }
            }
        }

        public void Delete(Guid? id, string deletedBy)
        {
            if (id == null)
                return;
            var student = Get(id);
            student.IsDeleted = true;
            student.DeletedBy = deletedBy;
            student.DeletedDate = DateTime.UtcNow;
            _personService.Delete(student.PersonId);
            _repository.Update(_mapper.Map<DTOStudent, Student>(student));
        }
        private void InsertStudentFinanceDetail(DTOStudent dtoStudent)
        {
            var listOfTypeIds = new[] { new { _financeTypeService.GetByName("Admission").Id, FeeAmount = dtoStudent.AdmissionFee },
                new { _financeTypeService.GetByName("Monthly").Id, FeeAmount = dtoStudent.MonthlyFee }
            }.ToList();

            foreach (var type in listOfTypeIds)
            {
                var stdFinance = new DTOStudentFinanceDetail
                {
                    StudentId = dtoStudent.Id,
                    IsDeleted = false,
                    Fee = type.FeeAmount,
                    CreatedDate = dtoStudent.CreatedDate,
                    CreatedBy = dtoStudent.CreatedBy,
                    FinanceTypeId = type.Id
                };
                if (stdFinance.Id == Guid.Empty)
                {
                    stdFinance.Id = Guid.NewGuid();
                }
                _studentFinanceDetailsService.Create(stdFinance);
            }


        }

        private void HelpingMethodForRelationship(DTOStudent dtoStudent)
        {
            dtoStudent.SchoolId = dtoStudent.School.Id;
            dtoStudent.ClassId = dtoStudent.Class.Id;
            dtoStudent.Person = null;
            dtoStudent.Class = null;
            dtoStudent.School = null;
            dtoStudent.Image = null;
        }
        private StudentResponse Validation(DTOStudent dtoStudent)
        {
            var alphaRegex = new Regex("^[a-zA-Z ]+$");
            var numericRegex = new Regex("^[0-9]*$");
            if (string.IsNullOrWhiteSpace(dtoStudent.Person.FirstName) || dtoStudent.Person.FirstName.Length > 100)
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "InvalidName",
                    "Name may null or exceed than 100 characters"
                    );
            }
            if (!alphaRegex.IsMatch(dtoStudent.Person.FirstName))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                   "InvalidName",
                   "Text Field doesn't contain any numbers"
                   );
            }
            if (string.IsNullOrWhiteSpace(dtoStudent.Person.LastName) || dtoStudent.Person.LastName.Length > 100)
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "InvalidName",
                    "Name may null or exceed than 100 characters"
                    );
            }
            if (!alphaRegex.IsMatch(dtoStudent.Person.LastName))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                   "InvalidName",
                   "Text Field doesn't contain any numbers"
                   );
            }
            if (dtoStudent.Person.Cnic == null || dtoStudent.Person.Cnic.Length != 13)
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "CnicLimitError",
                    "Cnic must be of 13 digits"
                    );
            }
            if (!numericRegex.IsMatch(dtoStudent.Person.Cnic))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                   "InvalidInput",
                   "This field contains only digits"
                   );
            }
            if (string.IsNullOrWhiteSpace(dtoStudent.Person.Phone) || dtoStudent.Person.Phone.Length > 15)
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "PhoneLimitError",
                    "Phone cannot exceed from 15 digits"
                    );
            }
            if (!numericRegex.IsMatch(dtoStudent.Person.Phone))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                   "InvalidInput",
                   "This field contains only digits"
                   );
            }
            if (string.IsNullOrWhiteSpace(dtoStudent.Person.Nationality))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "InvalidNationality",
                    "Nationality cannot be null"
                    );
            }
            if (!alphaRegex.IsMatch(dtoStudent.Person.Nationality))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                   "InvalidText",
                   "Text Field doesn't contain any numbers"
                   );
            }
            if (!string.IsNullOrWhiteSpace(dtoStudent.Person.Religion) && !alphaRegex.IsMatch(dtoStudent.Person.Religion))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                   "InvalidText",
                   "Text Field doesn't contain any numbers"
                   );
            }
            if (dtoStudent.School == null)
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "InvalidSchool",
                    "School cannot be null"
                    );
            }
            if (dtoStudent.Class == null)
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "InvalidClass",
                    "Class cannot be null"
                    );
            }
            if (string.IsNullOrWhiteSpace(dtoStudent.Person.ParentName) || dtoStudent.Person.ParentName.Length > 100)
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "InvalidName",
                    "Name may null or exceed than 100 characters"
                    );
            }
            if (!alphaRegex.IsMatch(dtoStudent.Person.ParentName))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                   "InvalidName",
                   "Text Field doesn't contain any numbers"
                   );
            }
            if (dtoStudent.Person.ParentCnic != null && dtoStudent.Person.ParentCnic.Length != 13 && !numericRegex.IsMatch(dtoStudent.Person.ParentCnic))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "CnicLimitError",
                    "Cnic must be of 13 digits"
                    );
            }
            if (string.IsNullOrWhiteSpace(dtoStudent.Person.ParentRelation))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "InvalidRelation",
                    "Relation cannot be null"
                    );
            }
            if (!alphaRegex.IsMatch(dtoStudent.Person.ParentRelation))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                   "InvalidText",
                   "Text Field doesn't contain any numbers"
                   );
            }
            if (!string.IsNullOrWhiteSpace(dtoStudent.Person.ParentOccupation) && dtoStudent.Person.ParentOccupation.Length > 100 && !alphaRegex.IsMatch(dtoStudent.Person.ParentOccupation))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "InvalidText",
                    "Text Field doesn't contain any numbers"
                    );
            }
            if (!string.IsNullOrWhiteSpace(dtoStudent.Person.ParentNationality) && !alphaRegex.IsMatch(dtoStudent.Person.ParentNationality))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "InvalidText",
                    "Text Field doesn't contain any numbers"
                    );
            }
            if (string.IsNullOrWhiteSpace(dtoStudent.Person.ParentMobile1) || dtoStudent.Person.ParentMobile1.Length > 15)
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "InvalidNumber",
                    "Number cannot be null"
                    );
            }
            if (!numericRegex.IsMatch(dtoStudent.Person.ParentMobile1))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                   "InvalidInput",
                   "This field contains only digits"
                   );
            }
            if (string.IsNullOrWhiteSpace(dtoStudent.Person.ParentEmergencyName) || dtoStudent.Person.ParentEmergencyName.Length > 100)
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "InvalidName",
                    "Name may null or exceed than 100 characters"
                    );
            }
            if (!alphaRegex.IsMatch(dtoStudent.Person.ParentEmergencyName))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                   "InvalidName",
                   "Text Field doesn't contain any numbers"
                   );
            }
            if (string.IsNullOrWhiteSpace(dtoStudent.Person.ParentEmergencyRelation))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "InvalidRelation",
                    "Relation cannot be null"
                    );
            }
            if (!alphaRegex.IsMatch(dtoStudent.Person.ParentEmergencyRelation))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                   "InvalidText",
                   "Text Field doesn't contain any numbers"
                   );
            }
            if (string.IsNullOrWhiteSpace(dtoStudent.Person.ParentEmergencyMobile) || dtoStudent.Person.ParentEmergencyMobile.Length > 15)
            {
                return PrepareFailureResponse(dtoStudent.Id,
                    "InvalidNumber",
                    "Number cannot be null"
                    );
            }
            if (!numericRegex.IsMatch(dtoStudent.Person.ParentEmergencyMobile))
            {
                return PrepareFailureResponse(dtoStudent.Id,
                   "InvalidInput",
                   "This field contains only digits"
                   );
            }
            return RequestPrepareSuccessResponse(dtoStudent.Id,
                    "NoError",
                    "No Error Found"
                    );
        }
        private StudentResponse RequestPrepareSuccessResponse(Guid id, string message, string descriptionMessage)
        {
            return new StudentResponse
            {
                Id = id,
                IsError = false,
                StatusCode = "200",
                Message = message,
                Description = descriptionMessage
            };
        }
        #endregion

        #region RequestSMS Section

        private StudentResponse PrepareFailureResponse(Guid id, string errorMessage, string descriptionMessage)
        {
            return new StudentResponse
            {
                Id = id,
                IsError = true,
                StatusCode = "400",
                Message = errorMessage,
                Description = descriptionMessage
            };
        }
        #endregion
    }
}
