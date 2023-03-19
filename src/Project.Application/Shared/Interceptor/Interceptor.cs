using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using Project.Authorization.Accounts;
using Project.Shared.Advice;
using Project.Shared.Enums;
using Project.Souccar.Core.Extensions;
using Project.Souccar.Core.Fasterflect;

namespace Project.Shared.Interceptor
{
    public class Interceptor : IInterceptor
    {
        public ILogger Logger { get; set; }

        public Interceptor()
        {
            Logger = NullLogger.Instance;
        }

        public void Intercept(IInvocation invocation)
        {
            if (IsAsyncMethod(invocation.Method))
            {
                InterceptAsync(invocation);
            }
            else
            {
                InterceptSync(invocation);
            }
        }
        private static dynamic getAdvice(IInvocation invocation)
        {
            var _type = invocation.TargetType.FullName.Replace(".Services", ".Advices");
            _type = _type.Replace("AppService", "Advice");
            var type = _type.ToType();
            if (type !=null)
                return Activator.CreateInstance(_type.ToType());
            else return null;
        }
        private async void InterceptAsync(IInvocation invocation)
        {
            
            //Before method execution
            var stopwatch = Stopwatch.StartNew();
            
            var advice = getAdvice(invocation);
            if (advice != null)
            {
                var methodName = invocation.Method.Name;
                MethodType methodType = MethodType.None;
                if (methodName.StartsWith("Get"))
                {
                    methodType = MethodType.Read;
                }
                else if (methodName.StartsWith("Insert"))
                {
                    methodType = MethodType.Create;
                }
                else if (methodName.StartsWith("Update"))
                {
                    methodType = MethodType.Update;
                }
                else if (methodName.StartsWith("Delete"))
                {
                    methodType = MethodType.Delete;
                }

                switch (methodType)
                {
                    case MethodType.Read:
                        await advice.BeforeReadAsync(invocation);
                        break;
                    case MethodType.Update:
                        await advice.BeforeUpdateAsync(invocation);
                        break;
                    case MethodType.Delete:
                        await advice.BeforeDeleteAsync(invocation);
                        break;
                    case MethodType.Create:
                        await advice.BeforeInsertAsync(invocation);
                        break;
                    default:
                        break;
                }

                //Calling the actual method, but execution has not been finished yet

                invocation.Proceed();




                //Wait task execution and modify return value
                if (invocation.Method.ReturnType == typeof(Task))
                {
                    invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithPostActionAndFinally(
                        (Task)invocation.ReturnValue,
                        async () => await advice.AfterDeleteAsync(invocation),
                        ex =>
                        {
                            LogExecutionTime(invocation, stopwatch);
                        });
                }
                else //Task<TResult>
                {
                    invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithPostActionAndFinallyAndGetResult(
                        invocation.Method.ReturnType.GenericTypeArguments[0],
                        invocation.ReturnValue,
                        async () =>
                        {
                            switch (methodType)
                            {
                                case MethodType.Read:
                                    await advice.AfterReadAsync(invocation);
                                    break;
                                case MethodType.Update:
                                    await advice.AfterUpdateAsync(invocation);
                                    break;
                                case MethodType.Create:
                                    await advice.AfterInsertAsync(invocation);
                                    break;
                                default:
                                    break;
                            }
                        },
                        ex =>
                        {
                            LogExecutionTime(invocation, stopwatch);
                        });
                }




            }
            else
            {
                invocation.Proceed();
            }

        }

        private void InterceptSync(IInvocation invocation)
        {
            //Before method execution
            var stopwatch = Stopwatch.StartNew();
            var advice = getAdvice(invocation);

            if (advice != null)
            {

                var methodName = invocation.Method.Name;
                MethodType methodType = MethodType.None;
                if (methodName.StartsWith("Get"))
                {
                    methodType = MethodType.Read;
                }

                switch (methodType)
                {
                    case MethodType.Read:
                        advice.BeforeRead(invocation);
                        break;
                    default:
                        break;
                }
                //Executing the actual method
                invocation.Proceed();

                //After method execution
                switch (methodType)
                {
                    case MethodType.Read:
                        advice.AfterRead(invocation);
                        break;
                }
                LogExecutionTime(invocation, stopwatch);
            }
            else
            {
                invocation.Proceed();
            }
        }

        public static bool IsAsyncMethod(MethodInfo method)
        {
            return
                method.ReturnType == typeof(Task) ||
                method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>)
                ;
        }

        private void LogExecutionTime(IInvocation invocation, Stopwatch stopwatch)
        {
            stopwatch.Stop();
            Logger.InfoFormat(
                "MeasureDurationWithPostAsyncActionInterceptor: {0} executed in {1} milliseconds.",
                invocation.MethodInvocationTarget.Name,
                stopwatch.Elapsed.TotalMilliseconds.ToString("0.000")
                );
        }
    }
}