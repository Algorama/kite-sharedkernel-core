using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.Repositories.Mock;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace SharedKernel.DependencyInjector
{
    public class Kernel
    {
        private static Container _kernel;

        public static Container GetKernel()
        {
            if (_kernel == null)
                throw new Exception("Kernel não foi inicializado");

            return _kernel;
        }

        public static void StartMockRepository()
        {
            if (_kernel == null) _kernel = new Container();
            if(_kernel.IsLocked()) return;

            _kernel.Register<IHelperRepository, MockHelperRepository>();
        }

        public static void StartNHibernate()
        {
            if(_kernel == null) _kernel = new Container();
            if (_kernel.IsLocked()) return;

            _kernel.Register<IHelperRepository, NHibernate.Repositories.HelperRepository>();
        }

        public static void StartEntityFramework()
        {
            if (_kernel == null) _kernel = new Container();
            if (_kernel.IsLocked()) return;

            _kernel.Register<IHelperRepository, EntityFramework.Repositories.HelperRepository>();
        }

        public static void IntegrateAspNet(IServiceCollection services)
        {
            if (_kernel == null) _kernel = new Container();
            _kernel.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_kernel));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_kernel));
            services.UseSimpleInjectorAspNetRequestScoping(_kernel);
        }

        public static void StartAspNet(IConfiguration configuration, IApplicationBuilder app)
        {
            _kernel.RegisterMvcControllers(app);
        }

        public static T Get<T>() where T : class
        {
            if(_kernel == null)
                throw new Exception("Kernel não foi inicializado");

            return _kernel.GetInstance<T>();
        }

        public static void Bind<TFrom, TTo>() 
            where TTo : class, TFrom
            where TFrom : class
        {
            if(_kernel == null)
                throw new Exception("Kernel não foi inicializado");

            if (_kernel.IsLocked()) return;

            _kernel.Register<TFrom, TTo>();
        }

        public static void Bind(Type type1, Type type2)
        {
            if (_kernel == null)
                throw new Exception("Kernel não foi inicializado");

            if (_kernel.IsLocked()) return;

            _kernel.Register(type1, type2);
        }
    }
}