using AspNetCoreRateLimit;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        async public static Task Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //async public static Task Main(string[] args)
        //{
        //    using IHost webHost = CreateHostBuilder(args).Build();
        //    IIpPolicyStore IpPolicy = webHost.Services.GetRequiredService<IIpPolicyStore>();
        //    await IpPolicy.SeedAsync();
        //    await webHost.RunAsync();
        //}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder=>
            {
                builder.RegisterModule(new AutofacBusinessModule());
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
