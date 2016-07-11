using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using MvcMovie.DataAccess.Context;
using MvcMovie.DataAccess.Dals;
using MvcMovie.DataAccess.Interfaces;
using MvcMovie.Service.Interfaces;
using MvcMovie.Service.Services;

namespace MvcMovie.Modules
{
    public class MovieModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MovieService>().As<IMovieService>().InstancePerRequest();
            builder.RegisterType<MovieDal>().As<IMovieDal>().InstancePerRequest();
            builder.RegisterType<MvcMovieDbContext>().As<IMvcMovieDbContext>().InstancePerRequest();
        }
    }
}