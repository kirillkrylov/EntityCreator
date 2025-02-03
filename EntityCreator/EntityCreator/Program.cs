using System;
using System.IO.Abstractions;
using System.Threading.Tasks;
using DotMake.CommandLine;
using EntityCreator.Commands;
using EntityCreator.PgSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EntityCreator;

public static class Program {

	#region Methods: Public

	public static async Task<int> Main(string[] args){
		
		
		
		
		
		IHost host = Host
					.CreateDefaultBuilder(args)
					.ConfigureServices((context, services) => {
						IConfiguration configuration = context.Configuration;
						services.AddSingleton(configuration);
						services.AddSingleton<DataValueType>();
						services.Configure<AppSettings>(configuration);
						services.Configure<Tpl>(configuration.GetSection("Tpl"));
						services.AddSingleton<IFileSystem>(new FileSystem());
						services.AddSingleton<Template>();
						services.Configure<PackageSettings>(configuration.GetSection("PackageSettings"));
						services.Configure<DbSettings>(configuration.GetSection("DbSettings"));
						services.AddSingleton<App>();
						services.AddSingleton<Writer>();
						services.AddSingleton<IPgsql, Pgsql>();
					})
					.ConfigureLogging(logBuilder => { logBuilder.AddConsole(); })
					.UseConsoleLifetime()
					.Build();
		

		using (host) {
			IServiceProvider provider = host.Services;
			Cli.Ext.SetServiceProvider(provider);
			int x = await Cli.RunAsync<RootCliCommand>(args, new CliSettings {Theme = CliTheme.Green});

			ILogger<App> logger = provider.GetRequiredService<ILogger<App>>();
			logger.LogInformation(new EventId(99, "DisposingResources"), "Disposing of resources");
			await host.StopAsync();
			return x;
		}
	}

	#endregion

}