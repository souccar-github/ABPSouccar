﻿using Abp.Application.Services;
using Castle.Core;
using Castle.MicroKernel;

namespace Project.Shared.Interceptor
{
    public static class InterceptorRegistrar
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (typeof(IApplicationService).IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(Interceptor)));
            }
        }
    }
}