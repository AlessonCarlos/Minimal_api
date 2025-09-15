using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MinimalApi; // Startup da API principal
using MinimalApi.Dominio.Interfaces;
using Test.Mocks;

namespace Test.Helpers
{
    public static class Setup
    {
        public const string PORT = "5009";
        public static WebApplicationFactory<Program> http = default!;
        public static HttpClient client = default!;

        public static void ClassInit()
        {
            http = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("https_port", PORT)
                           .UseEnvironment("Testing");

                    builder.ConfigureServices(services =>
                    {
                        // Mock Administrador
                        var administradorDescriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(IAdministradorServico));
                        if (administradorDescriptor != null) services.Remove(administradorDescriptor);
                        services.AddScoped<IAdministradorServico, AdministradorServicoMock>();

                        // Mock Veículo
                        var veiculoDescriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(IVeiculoServico));
                        if (veiculoDescriptor != null) services.Remove(veiculoDescriptor);
                        services.AddScoped<IVeiculoServico, VeiculoServicoMock>();
                    });
                });

            client = http.CreateClient();
        }

        public static void ClassCleanup()
        {
            http.Dispose();
        }
    }
}
