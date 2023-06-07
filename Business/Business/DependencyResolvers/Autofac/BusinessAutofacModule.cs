using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstracts;
using Business.Concretes;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    // DI Container
    // IoC 


    // AOP => Aspect Oriented Programming
    public class BusinessAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfCarRepository>().As<ICarRepository>().SingleInstance();
            builder.RegisterType<CarManager>().As<ICarService>().SingleInstance();
            builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();
            builder.RegisterType<EfBrandRepository>().As<IBrandRepository>().SingleInstance();
            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<EfUserRepository>().As<IUserRepository>().SingleInstance();



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly)
                   .AsImplementedInterfaces()
                   .EnableInterfaceInterceptors(new ProxyGenerationOptions() { 
                       Selector = new AspectInterceptorSelector()
                       })
                   .SingleInstance();

            base.Load(builder);
        }
    }
}