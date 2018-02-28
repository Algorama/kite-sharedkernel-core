using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.Services;
using SharedKernel.EntityFramework.Repositories;
using SharedKernel.EntityFramework.Mock;
using SimpleInjector;
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

        private static void StartBase()
        {
            _kernel = new Container();
            _kernel.Register(typeof(IQueryService<>), typeof(QueryService<>));
            _kernel.Register(typeof(ICrudService<>), typeof(CrudService<>));
        }

        public static void Start()
        {
            StartBase();
            _kernel.Register<IHelperRepository, HelperRepository>();
        }

        public static void StartMock()
        {
            StartBase();
            _kernel.Register<IHelperRepository, MockHelperRepository>();
        }

        public static void IntegrateAspNet(IServiceCollection services)
        {
            _kernel = new Container();
            _kernel.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_kernel));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_kernel));
            services.UseSimpleInjectorAspNetRequestScoping(_kernel);
        }

        public static void StartAspNet(IConfiguration configuration, IApplicationBuilder app)
        {
            _kernel.RegisterMvcControllers(app);
            _kernel.Register(typeof(IQueryService<>), typeof(QueryService<>));
            _kernel.Register(typeof(ICrudService<>), typeof(CrudService<>));
            _kernel.Register<IHelperRepository, HelperRepository>();
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

            _kernel.Register<TFrom, TTo>();
        }
    }
}