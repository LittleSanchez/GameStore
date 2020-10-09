using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using GameStore.Client.Controllers;
using GameStore.DAL;
using GameStore.DAL.Repository.Abstraction;
using GameStore.DAL.Repository.Implemention;
using GameStore.DLL.Services.Abstraction;
using GameStore.DLL.Services.Implemention;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Client.Utils
{
    public static class AutofacConfiguration
    {
        public static void ConfigurateAutofac()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<ApplicationContext>().As<DbContext>().SingleInstance();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterType<GameService>().As<IGameService>();

            var mapperConfig = new MapperConfiguration(config => config.AddProfile(new MapperProfile()));
            builder.RegisterInstance<IMapper>(mapperConfig.CreateMapper());

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}