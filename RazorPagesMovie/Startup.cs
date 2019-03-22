using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie.Data;
using ServiceLayer.MovieServices;

namespace RazorPagesMovie
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<RazorPagesMovieContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("RazorPagesMovieContext")));

            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IGenreService, GenreService>();

            services.AddAutoMapper(typeof(RazorPagesMovie.Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }

    }

    public class MovieProfile : Profile
    {
        // http://docs.automapper.org/en/stable/Queryable-Extensions.html og https://docs.automapper.org/en/stable/Reverse-Mapping-and-Unflattening.html
        public MovieProfile()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Movie, MovieDto>()
                    .ForMember(dto => dto.GenreName, conf => conf.MapFrom(g => g.Genre.GenreName));
                    //.ReverseMap()
                    //.ForPath(s => s.Genre, s => s.Ignore());

                // Med en ekstra regel
                cfg.CreateMap<MovieDto, Movie>()
                .ForPath(s => s.Genre, s => s.Ignore());    // virker ikke. Måtte tilføje dummy Genre property til DTO!!!
            });

                      
        }
    }
}
