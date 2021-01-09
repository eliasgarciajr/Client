using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Client.Configuration
{
	public static class GlobalSettings
	{
		public static ClientSettings Configuration { get; set; }
	}

	public enum EClientProjects
	{
		Client = 0
	}

	public class ClientSettings
	{
		private static string GetFileName()
		{

			return "ClientSettings.json";			

		}

		public ClientSettings()
		{
			Configs = new List<ClientItemSetting>();
		}

		public List<ClientItemSetting> Configs { get; set; }			

		public class ClientItemSetting
		{
			public EClientProjects Projeto { get; set; } = EClientProjects.Client;
			public string Host { get; set; } = "";
			public string ConnectionString { get; set; } = "";
		}

		/// <summary>
		///     Get a specific setting of ClientProject
		/// </summary>
		/// <param name="project">Name of the project that you need to get the setting</param>
		/// <returns>ClientSettting</returns>
		public static ClientItemSetting GetSetting(EClientProjects project)
		{
			var ClientSettings = LoadJson();
			return ClientSettings.Configs.FirstOrDefault(x => x.Projeto == project);
		}

		/// <summary>
		///     Get the connectionString of a specifi ClientProject
		/// </summary>
		/// <param name="project">Name of the project that you need to get the connectionString</param>
		/// <returns>Connection String</returns>
		public static string GetConnectionString(EClientProjects project)
		{
			return GetSetting(project)?.ConnectionString ?? "";
		}

		/// <summary>
		///     Get the Endpoint of a specifi ClientProject
		/// </summary>
		/// <param name="project">Name of the project that you need to get the Host address</param>
		/// <returns>Host</returns>
		public static string GetHost(EClientProjects project)
		{
			var host = GetSetting(project)?.Host;
			if (!string.IsNullOrEmpty(host) && host.EndsWith("/")) host = host.Remove(host.Length - 1);
			return GetSetting(project)?.Host ?? "";
		}

		//TODO
		private static ClientSettings LoadJson()
		{
			try
			{
				ClientSettings config = null;
				if (GlobalSettings.Configuration == null)
				{
					string file = GetFileName();

					var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

					var configPath = Path.Combine(path, file);

					if (!File.Exists(configPath))
						configPath = Path.Combine(path, "Configuration", file);


					var reader =
						new JsonTextReader(new StringReader(File.ReadAllText(configPath)));
					var serializer = new JsonSerializer();
					config = serializer.Deserialize<ClientSettings>(reader);
					if (config == null)
						throw new Exception("No configuration was found");
					if (config.Configs == null || config.Configs.Count == 0)
						throw new Exception("Client settings was not parametrized");
					GlobalSettings.Configuration = config;
				}

				return GlobalSettings.Configuration;
			}
			catch (Exception ex)
			{
				throw new Exception("A problem occured when loading ClientSettings.json. Error: " + ex.Message);
			}
		}
	}
}