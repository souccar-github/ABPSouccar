using Abp.Domain.Entities;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.Advice
{
    public class SouccarAdvice<TEntity> : ISouccarAdvice<TEntity> where TEntity : class, IEntity<int>
    {
        public async virtual Task BeforeReadAsync(IInvocation invocation)
        {
        }

        public virtual void BeforeRead(IInvocation invocation)
        {
        }

        public async virtual Task AfterReadAsync(IInvocation invocation)
        {
        }
        public virtual void AfterRead(IInvocation invocation)
        {
        }
        public async virtual Task BeforeDeleteAsync(IInvocation invocation)
        {
        }
        public async virtual Task AfterDeleteAsync(IInvocation invocation)
        {
        }
        public async virtual Task BeforeInsertAsync(IInvocation invocation)
        {
        }
        public async virtual Task AfterInsertAsync(IInvocation invocation)
        {
        }
        public async virtual Task BeforeUpdateAsync(IInvocation invocation)
        {
        }
        public async virtual Task AfterUpdateAsync(IInvocation invocation)
        {
        }
    }
}
