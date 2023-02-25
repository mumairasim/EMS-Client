using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DATA.Models.Enums;
using SMS.DTOs.DTOs;
using SMS.DTOs.ReponseDTOs;
using SMS.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Course = SMS.DATA.Models.Course;
using DTOCourse = SMS.DTOs.DTOs.Course;
using RequestMeta = SMS.DATA.Models.RequestMeta;

namespace SMS.Services.Implementation
{
    public class CourseService : ICourseService
    {
        #region Properties
        private readonly IRepository<Course> _repository;
        public IRequestMetaService _requestMetaService;
        private const string error_not_found = "Record not found";
        private const string server_error = "Server error";
        private IMapper _mapper;
        #endregion

        #region Init

        public CourseService(IRepository<Course> repository, IMapper mapper, IRequestMetaService requestMetaService)
        {
            _repository = repository;
            _mapper = mapper;
            _requestMetaService = requestMetaService;
        }

        #endregion

        #region SMS Section

        /// <summary>
        /// Service level call : Creates a single record of a Course
        /// </summary>
        /// <param name="dtoCourse"></param>
        /// 

        public GenericApiResponse Create(DTOCourse dtoCourse)
        {
            try
            {
                dtoCourse.CreatedDate = DateTime.UtcNow;
                dtoCourse.IsDeleted = false;
                if (dtoCourse.Id == Guid.Empty)
                {
                    dtoCourse.Id = Guid.NewGuid();
                }
                dtoCourse.SchoolId = dtoCourse.School.Id;
                dtoCourse.School = null;
                var dbCourse = _mapper.Map<DTOCourse, Course>(dtoCourse);
                dbCourse.ApprovalStatus = DATA.Models.Enums.RequestStatus.Pending;
                _repository.Add(dbCourse);

                return PrepareSuccessResponse("Created", "Instance Created Successfully");

            }
            catch (Exception ex)
            {
                return PrepareFailureResponse("Error", server_error);
            }
        }

        /// <summary>
        /// Service level call : Delete a single record of a Course
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid? id)
        {
            if (id == null)
                return;
            var course = Get(id);
            if (course != null)
            {
                course.IsDeleted = true;
                course.DeletedDate = DateTime.UtcNow;

                _repository.Update(_mapper.Map<DTOCourse, Course>(course));
            }
        }

        /// <summary>
        /// Retruns a Single Record of a Course
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DTOCourse Get(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var course = _repository.Get().FirstOrDefault(x => x.Id == id && (x.IsDeleted == false || x.IsDeleted == null));
            var courseDto = _mapper.Map<Course, DTOCourse>(course);

            return courseDto;
        }




        /// <summary>
        /// Service level call : Updates the Single Record of a Course 
        /// </summary>
        /// <param name="dtoCourse"></param>
        /// 

        public GenericApiResponse Update(DTOCourse dtoCourse)
        {
            try
            {
                var course = Get(dtoCourse.Id);
                if (course != null)
                {
                    dtoCourse.UpdateDate = DateTime.UtcNow;
                    dtoCourse.SchoolId = dtoCourse.School.Id;
                    dtoCourse.School = null;
                    var updated = _mapper.Map(dtoCourse, course);
                    dtoCourse.IsDeleted = false;

                    _repository.Update(_mapper.Map<DTOCourse, Course>(updated));
                    if (dtoCourse.IsClient == true)
                    {
                        _requestMetaService.Create(new RequestMeta
                        {
                            ModuleId = dtoCourse.Id,
                            SchoolId = dtoCourse.SchoolId,
                            ModuleName = Module.Course,
                            ApprovalStatus = DATA.Models.Enums.RequestStatus.Pending,
                            Type = DATA.Models.Enums.RequestType.Update
                        });
                    }
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
        /// Service level call : Return all records of course
        /// </summary>
        /// <returns></returns>
        List<DTOCourse> ICourseService.GetAll()
        {
            var courses = _repository.Get().Where(x => (x.IsDeleted == false || x.IsDeleted == null)).ToList();
            var courseList = new List<DTOCourse>();
            foreach (var course in courses)
            {
                courseList.Add(_mapper.Map<Course, DTOCourse>(course));
            }
            return courseList;
        }
        public List<DTOCourse> GetAllBySchool(Guid? schoolId)
        {
            var courses = _repository.Get().Where(x => (x.IsDeleted == false || x.IsDeleted == null) && x.SchoolId == schoolId).ToList();
            var courseList = new List<DTOCourse>();
            foreach (var course in courses)
            {
                courseList.Add(_mapper.Map<Course, DTOCourse>(course));
            }
            return courseList;
        }

        #endregion


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

        public CoursesList Get(int pageNumber, int pageSize, string searchString = "")
        {
            var courses = _repository.Get()
                .Where(cl => string.IsNullOrEmpty(searchString) || cl.CourseName.ToLower().Contains(searchString.ToLower()))
                .Union(_repository.Get().Where(cl => string.IsNullOrEmpty(searchString) || cl.CourseCode.ToLower().Contains(searchString.ToLower())))
                .Where(cl => cl.IsDeleted == false).OrderByDescending(st => st.Id).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var courseCount = courses.Count();
            var coursesTempList = new List<DTOCourse>();
            foreach (var course in courses)
            {
                coursesTempList.Add(_mapper.Map<Course, DTOCourse>(course));
            }
            var classesList = new CoursesList()
            {
                Courses = coursesTempList,
                Count = courseCount
            };
            return classesList;
        }
    }
}
