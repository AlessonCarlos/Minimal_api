using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Serviços;
using MinimalApi.Infraestrutura.Db;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculoServicoTest
{
    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();
        return new DbContexto(configuration);
    }

    private void LimparTabelaVeiculos(DbContexto context)
    {
        // Desativa chaves estrangeiras, limpa tabela e reativa
        context.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS=0;");
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos;");
        context.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS=1;");
    }

    [TestMethod]
    public void TestandoIncluirVeiculo()
    {
        var context = CriarContextoDeTeste();
        LimparTabelaVeiculos(context);

        var veiculoServico = new VeiculoServico(context);

        veiculoServico.Incluir(new Veiculo { Nome = "Civic", Marca = "Honda", Ano = 2022 });

        Assert.AreEqual(1, context.Veiculos.Count());
    }

    [TestMethod]
    public void TestandoApagarVeiculo()
    {
        var context = CriarContextoDeTeste();
        LimparTabelaVeiculos(context);

        var veiculoServico = new VeiculoServico(context);

        var veiculo = new Veiculo { Nome = "Civic", Marca = "Honda", Ano = 2022 };
        veiculoServico.Incluir(veiculo);

        veiculoServico.Apagar(veiculo);

        Assert.AreEqual(0, context.Veiculos.Count());
    }

    [TestMethod]
    public void TestandoBuscaPorId()
    {
        var context = CriarContextoDeTeste();
        LimparTabelaVeiculos(context);

        var veiculoServico = new VeiculoServico(context);

        var veiculo = new Veiculo { Nome = "Civic", Marca = "Honda", Ano = 2022 };
        veiculoServico.Incluir(veiculo);

        var veiculoDoBanco = veiculoServico.BuscaPorId(veiculo.Id);

        Assert.IsNotNull(veiculoDoBanco);
        Assert.AreEqual("Civic", veiculoDoBanco!.Nome);
    }

    [TestMethod]
    public void TestandoFiltroETodos()
    {
        var context = CriarContextoDeTeste();
        LimparTabelaVeiculos(context);

        var veiculoServico = new VeiculoServico(context);

        veiculoServico.Incluir(new Veiculo { Nome = "Civic", Marca = "Honda", Ano = 2022 });
        veiculoServico.Incluir(new Veiculo { Nome = "Corolla", Marca = "Toyota", Ano = 2021 });
        veiculoServico.Incluir(new Veiculo { Nome = "Fit", Marca = "Honda", Ano = 2020 });
        veiculoServico.Incluir(new Veiculo { Nome = "Camry", Marca = "Toyota", Ano = 2023 });

        // Testando filtro por Marca = "Honda"
        var honda = veiculoServico.Todos(null, marca: "Honda");
        Assert.AreEqual(2, honda.Count);

        // Testando filtro por Nome = "Corolla"
        var corolla = veiculoServico.Todos(null, nome: "Corolla");
        Assert.AreEqual(1, corolla.Count);
    }
}
