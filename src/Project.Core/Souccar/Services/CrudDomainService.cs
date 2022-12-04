using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Http.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Souccar.Services
{
    public class CrudDomainService<TEntity> : ICrudDomainService<TEntity> where TEntity : class, IEntity<int>
    {
        private readonly IRepository<TEntity> _repository;
        public CrudDomainService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        [UnitOfWork]
        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllListAsync();
        }

        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        [UnitOfWork]
        public virtual Task<TEntity> GetAsync(int id)
        {
            return _repository.GetAsync(id);
        }

        [UnitOfWork]
        public virtual Task<TEntity> InsertAsync(TEntity entity)
        {
            return _repository.InsertAsync(entity);
        }

        [UnitOfWork]
        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            return _repository.UpdateAsync(entity);
        }

        public virtual IList<TEntity> GetAllIncluding()
        {
            var refProperties = typeof(TEntity).GetProperties()
                .Where(x => x.PropertyType.BaseType != null
                    && x.PropertyType.BaseType.BaseType != null
                    && x.PropertyType.BaseType.BaseType.FullName.Contains("Entities")).ToList();
            var listProperties = typeof(TEntity).GetProperties()
                .Where(x =>  x.PropertyType != null
                    && x.PropertyType.FullName.Contains("List"))
                .ToList();
            var lambdaExp = new List<Expression<Func<TEntity, object>>> ();
            foreach (var item in refProperties)
            {
                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var member = Expression.Property(parameter, item.Name);
                var finalExpression = Expression.Lambda<Func<TEntity, object>>(member,parameter);
                lambdaExp.Add(finalExpression);
            }
            foreach (var item in listProperties)
            {
                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var member = Expression.Property(parameter, item.Name);
                var finalExpression = Expression.Lambda<Func<TEntity, object>>(member, parameter);
                lambdaExp.Add(finalExpression);
            }
            var items = _repository.GetAllIncluding(lambdaExp.ToArray());
            return items.ToList();
        }

        public virtual TEntity GetIncluding(int id)
        {
            var refProperties = typeof(TEntity).GetProperties()
                .Where(x => x.PropertyType.BaseType != null
                    && x.PropertyType.BaseType.BaseType != null
                    && x.PropertyType.BaseType.BaseType.FullName.Contains("Entities")).ToList();
            var listProperties = typeof(TEntity).GetProperties()
                .Where(x => x.PropertyType != null
                    && x.PropertyType.FullName.Contains("List"))
                .ToList();
            var lambdaExp = new List<Expression<Func<TEntity, object>>>();
            foreach (var item in refProperties)
            {
                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var member = Expression.Property(parameter, item.Name);
                var finalExpression = Expression.Lambda<Func<TEntity, object>>(member, parameter);
                lambdaExp.Add(finalExpression);
            }
            foreach (var item in listProperties)
            {
                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var member = Expression.Property(parameter, item.Name);
                var finalExpression = Expression.Lambda<Func<TEntity, object>>(member, parameter);
                lambdaExp.Add(finalExpression);
            }
            var result = _repository.GetAllIncluding(lambdaExp.ToArray()).FirstOrDefault(x=>x.Id == id);
            return result;
        }
    }
}
