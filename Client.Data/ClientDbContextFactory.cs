using Client.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Data
{

	public class ClientDbContextFactory : IDesignTimeDbContextFactory<ClientDbContext>
	{
		public ClientDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ClientDbContext>();			
			optionsBuilder.UseSqlServer(ClientSettings.GetConnectionString(EClientProjects.Client));


			return new ClientDbContext(optionsBuilder.Options);
		}
	}
}
