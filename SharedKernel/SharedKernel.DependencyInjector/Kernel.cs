using System;
using SharedKernel.Domain.Repositories;
using SharedKernel.Domain.Services;
using SharedKernel.EntityFramework.Repositories;
using SharedKernel.EntityFramework.Mock;
using SimpleInjector;

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
            //_kernel.Options.AllowOverridingRegistrations = false;
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