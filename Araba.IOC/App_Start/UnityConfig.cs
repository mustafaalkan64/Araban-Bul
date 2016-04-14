using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Araba.Data.Repository;
using Araba.Core.Domain.DbTables;
using Araba.Data.UnitOfWork;
using Araba.Service.UserServices;
using Araba.Service;
using Araba.Service.RoleServices;
using Araba.Service.LocationServices;

namespace Araba.IOC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            RegisterTypes(container);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.BindInRequestScope<IGenericRepository<User>, GenericRepository<User>>();
            container.BindInRequestScope<IGenericRepository<Role>, GenericRepository<Role>>();
            container.BindInRequestScope<IGenericRepository<City>, GenericRepository<City>>();
            container.BindInRequestScope<IGenericRepository<District>, GenericRepository<District>>();
            container.BindInRequestScope<IGenericRepository<UserType>, GenericRepository<UserType>>();

            container.BindInRequestScope<IUnitOfWork, UnitOfWork>();

            container.BindInRequestScope<IGenericService<User>, IGenericService<User>>();
            container.BindInRequestScope<IGenericService<Role>, IGenericService<Role>>();
            container.BindInRequestScope<IUserService, UserService>();
            container.BindInRequestScope<IRoleService, RoleService>();
            container.BindInRequestScope<ILocationService, LocationService>();
        }
    }

    /// <summary>
    /// Bind the given interface in request scope
    /// </summary>
    public static class IOCExtensions
    {
        public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
        }

        public static void BindInSingletonScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new ContainerControlledLifetimeManager());
        }
    }
}