using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using MvcMovie.DataAccess.Context;
using MvcMovie.DataAccess.Dals;
using MvcMovie.DataAccess.Interfaces;
using MvcMovie.Service.Interfaces;
using MvcMovie.Service.Services;

namespace MvcMovie
{
    public class DiConfig
    {
        public static void RegisterDependencyInjection()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<MovieService>().As<IMovieService>().InstancePerRequest();
            builder.RegisterType<MovieDal>().As<IMovieDal>().InstancePerRequest();
            builder.RegisterType<MvcMovieDbContext>().As<IMvcMovieDbContext>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}