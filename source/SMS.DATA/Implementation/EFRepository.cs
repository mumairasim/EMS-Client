using Newtonsoft.Json;
using SMS.DATA.Infrastructure;
using SMS.DATA.Models;
using SMS.DATA.Models.Enums;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SMS.DATA.Implementation
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        public EfRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public T Add(T entity)
        {
            var res = _unitOfWork.Context.Set<T>().Add(entity);
            if (entity.ApprovalStatus != RequestStatus.GeneratedInSystem)
            {
                var tempelateJson = JsonConvert.SerializeObject(entity);
                var template = JsonConvert.DeserializeObject<DomainBaseEnitity>(tempelateJson);
                _unitOfWork.Context.Set<RequestMeta>().Add(new RequestMeta
                {
                    Id = Guid.NewGuid(),
                    ModuleId = template.Id,
                    ModuleName = (Module)Enum.Parse(typeof(Module), entity.GetType().Name),
                    SchoolId = template.SchoolId,
                    ApprovalStatus = template.ApprovalStatus,
                    Type = RequestType.Create,
                    CreatedDate = DateTime.Now,
                    CreatedBy = template.CreatedBy
                });
            }
            _unitOfWork.Commit();
            return res;
        }

        public void Delete(T entity)
        {
            T existing = _unitOfWork.Context.Set<T>().Find(entity);
            if (existing != null) _unitOfWork.Context.Set<T>().Remove(existing);
        }

        public IQueryable<T> Get()
        {
            return _unitOfWork.Context.Set<T>().Where(x => x.ApprovalStatus == RequestStatus.Approved || x.ApprovalStatus == RequestStatus.GeneratedInSystem || x.ApprovalStatus == null || x.ApprovalStatus == RequestStatus.Pending);
        }
        public IQueryable<T> GetRaw()
        {
            return _unitOfWork.Context.Set<T>();
        }
        public IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _unitOfWork.Context.Set<T>().Where(predicate);
        }

        public void Update(T entity)
        {
            _unitOfWork.Context.Set<T>().AddOrUpdate(entity);
            if (entity.ApprovalStatus != RequestStatus.GeneratedInSystem)
            {
                var tempelateJson = JsonConvert.SerializeObject(entity);
                var template = JsonConvert.DeserializeObject<DomainBaseEnitity>(tempelateJson);
                _unitOfWork.Context.Set<RequestMeta>().Add(new RequestMeta
                {
                    Id = Guid.NewGuid(),
                    ModuleId = template.Id,
                    ModuleName = (Module)Enum.Parse(typeof(Module), entity.GetType().Name),
                    SchoolId = template.SchoolId,
                    ApprovalStatus = template.ApprovalStatus,
                    Type = RequestType.Update,
                    UpdateDate = DateTime.Now,
                    UpdateBy = template.UpdateBy,
                });
            }
            _unitOfWork.Commit();
        }


        public IQueryable<T> Table => _unitOfWork.Context.Set<T>().AsNoTracking();
        public IQueryable<T> TableNoTracking => _unitOfWork.Context.Set<T>().AsNoTracking();
        #region Methods
        /// <summary>
        /// Soft Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void SoftDelete(T entity)
        {
            if (entity == null)
                throw new NullReferenceException();

            try
            {
                Update(entity);
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw exception;
            }
        }

        [Obsolete]
        public void ExecuteSqlCommand(string sqlQuery)
        {
            _ = _unitOfWork.Context.ExecuteSqlCommand(sqlQuery);
        }

        #endregion
    }
}
