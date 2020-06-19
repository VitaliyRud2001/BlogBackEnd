using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.MapperProfile;
using Application.Services.Implementation;
using Application.Services.Interface;
using AutoMapper;
using Infrastructure;
using Infrastructure.GenericRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;


namespace Blog_back_end
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
            services.AddControllers();
            string localConnection = Configuration.GetConnectionString("DefaultConnection");
            string azureConnection = Configuration.GetConnectionString("AzureConnection");
            // Please download appsettings.json for connecting to Azure DB

            services.AddDbContext<Infrastructure.BlogContext>(options =>
                options.UseSqlServer(azureConnection, x => x.MigrationsAssembly("Blog-back-end")));

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPaginationService, PaginationService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserService, UserService>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build());
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PostProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SoftServe BookCrossing", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SoftServe BookCrossing");
            });
          
                app.UseDeveloperExceptionPage();
         

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                    ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
                },
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
                RequestPath = new PathString("")
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
