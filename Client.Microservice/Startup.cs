using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Client.Microservice.IoC;
using Client.Service.Handlers;

namespace Client.Microservice
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

			services.AddMediatR(typeof(ClientRequestHandler).Assembly);
			services.AddControllers().AddNewtonsoftJson(options =>
			{				
				var settings = options.SerializerSettings;
				settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
				settings.DateFormatString = "dd/MM/yyyy";
				settings.NullValueHandling = NullValueHandling.Ignore;
				settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                var resolver = options.SerializerSettings.ContractResolver as DefaultContractResolver;
                resolver.NamingStrategy = new CamelCaseNamingStrategy();
            });


			services.RegistServices();

			services.AddMvc();						

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Client", Version = "v1" });

				var securityScheme = new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey
				};

				c.AddSecurityDefinition("Bearer", securityScheme);

				var securityRequirement = new OpenApiSecurityRequirement();
				securityRequirement.Add(securityScheme, new List<string>());
				c.AddSecurityRequirement(securityRequirement);
			});

			services.AddSwaggerGenNewtonsoftSupport();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Client");
				c.RoutePrefix = string.Empty;
			});

			app.UseRouting();
			app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});
			app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

		}
	}
}
