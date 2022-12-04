using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Project.Employees.Entities.Advices
{
    public class ChildrenAdvice : IChildrenAdvice
    {
        public async Task AfterDeleteAsync(IInvocation invocation){}
        public async Task AfterInsertAsync(IInvocation invocation){}
        public async Task AfterReadAsync(IInvocation invocation){}
        public async Task AfterUpdateAsync(IInvocation invocation){}
        public async Task BeforeDeleteAsync(IInvocation invocation){}
        public async Task BeforeInsertAsync(IInvocation invocation){}
        public async Task BeforeReadAsync(IInvocation invocation){}
        public async Task BeforeUpdateAsync(IInvocation invocation){}
        public void BeforeRead(IInvocation invocation){}
        public void AfterRead(IInvocation invocation){}
    }
}

