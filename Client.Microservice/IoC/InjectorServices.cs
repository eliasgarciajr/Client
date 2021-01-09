using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Data;
using Client.Model.Data;
using Client.Services;

namespace Client.Microservice.IoC
{
	public static class InjectorServices
	{
		public static void RegistServices(this IServiceCollection services)
		{
			services.AddDbContext<ClientDbContext>();

			services.AddScoped<IClientService, ClientService>();

		}
	}
}
