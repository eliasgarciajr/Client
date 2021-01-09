using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Client.Model;
using Client.Model.ViewModels;
using Client.Model.Data;
using Client.Configuration;
using Client.Data.Mappings;
using Client.Data.Extensions;

namespace Client.Data
{
	public class ClientDbContext : Microsoft.EntityFrameworkCore.DbContext
		
	{		

		public ClientDbContext(DbContextOptions options) : base(options)
		{

		}		

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(ClientSettings.GetConnectionString(EClientProjects.Client));
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{					
			base.OnModelCreating(modelBuilder);
			modelBuilder.AddConfiguration(new ClientMap());
			modelBuilder.AddConfiguration(new PhoneMap());

		}
	}
}
