using Abp.Domain.Entities;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.Advice
{
    public interface ISouccarAdvice<TEntity> where TEntity : class, IEntity<int>
    {
        Task AfterDeleteAsync(IInvocation invocation);
        Task AfterInsertAsync(IInvocation invocation);
        void AfterRead(IInvocation invocation);
        Task AfterReadAsync(IInvocation invocation);
        Task AfterUpdateAsync(IInvocation invocation);
        Task BeforeDeleteAsync(IInvocation invocation);
        Task BeforeInsertAsync(IInvocation invocation);
        void BeforeRead(IInvocation invocation);
        Task BeforeReadAsync(IInvocation invocation);
        Task BeforeUpdateAsync(IInvocation invocation);
    }
}
