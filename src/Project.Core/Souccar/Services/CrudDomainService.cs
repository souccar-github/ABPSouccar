using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using AutoMapper.Internal;
using Castle.MicroKernel.Registration;
using Microsoft.AspNetCore.Http.Metadata;
using Project.Souccar.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Souccar.Services
{
    public class CrudDomainService<TEntity, TGetAllInput> : ICrudDomainService<TEntity, TGetAllInput> 
            where TEntity : class, IEntity<int>
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

        public async virtual Task<IQueryable<TEntity>> CreateFilteredQuery(TGetAllInput input)
        {
            var properties = typeof(TEntity).GetProperties(BindingFlags.Public
                                                        | BindingFlags.Instance
                                                        | BindingFlags.DeclaredOnly).Where(x => !x.PropertyType.IsEnum && !x.PropertyType.IsListType() && x.PropertyType == typeof(string));
            Expression lambdaExp = null ;
            var parameter = Expression.Parameter(typeof(TEntity), "x");


            foreach (var item in properties)
            {
                var member = Expression.Property(parameter, item.Name);
                var constant = Expression.Constant(input.GetType().GetProperty("Keyword").GetValue(input).ToString());
                
                var containsCall = Expression.Call(
                    member,
                    typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                    constant);
               
                if (lambdaExp == null)
                {
                    lambdaExp = containsCall;
                }
                else
                {
                    lambdaExp = Expression.Or(lambdaExp, containsCall);
                }
            }
            var expression = Expression.Lambda<Func<TEntity, bool>>(lambdaExp, parameter);

            var list = await GetAllAsync();
            return list.AsQueryable().Where(expression);
        }

        public virtual IQueryable<TEntity> CreateFilteredIncludingQuery(TGetAllInput input)
        {
            var properties = typeof(TEntity).GetProperties(BindingFlags.Public
                                                        | BindingFlags.Instance
                                                        | BindingFlags.DeclaredOnly).Where(x => !x.PropertyType.IsEnum && !x.PropertyType.IsListType() && x.PropertyType == typeof(string));
            Expression lambdaExp = null;
            var parameter = Expression.Parameter(typeof(TEntity), "x");


            foreach (var item in properties)
            {
                var member = Expression.Property(parameter, item.Name);
                var constant = Expression.Constant(input.GetType().GetProperty("Keyword").GetValue(input).ToString());

                var containsCall = Expression.Call(
                    member,
                    typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                    constant);

                if (lambdaExp == null)
                {
                    lambdaExp = containsCall;
                }
                else
                {
                    lambdaExp = Expression.Or(lambdaExp, containsCall);
                }
            }
            var expression = Expression.Lambda<Func<TEntity, bool>>(lambdaExp, parameter);

            var list = GetAllIncluding();
            return list.AsQueryable().Where(expression);
        }

        public virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, TGetAllInput input)
        {
            ISortedResultRequest sortedResultRequest = input as ISortedResultRequest;
            if (sortedResultRequest != null && !sortedResultRequest.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(sortedResultRequest.Sorting);
            }

            if (input is ILimitedResultRequest)
            {
                return query.OrderByDescending((TEntity e) => e.Id);
            }

            return query;
        }

        public virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TGetAllInput input)
        {
            IPagedResultRequest pagedResultRequest = input as IPagedResultRequest;
            if (pagedResultRequest != null)
            {
                return query.PageBy(pagedResultRequest);
            }

            ILimitedResultRequest limitedResultRequest = input as ILimitedResultRequest;
            if (limitedResultRequest != null)
            {
                return query.Take(limitedResultRequest.MaxResultCount);
            }

            return query;
        }
    }
}
