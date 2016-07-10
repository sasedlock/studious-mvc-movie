using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
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

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}