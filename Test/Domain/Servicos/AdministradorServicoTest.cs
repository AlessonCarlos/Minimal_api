using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Serviços;
using MinimalApi.Infraestrutura.Db;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Domain.Entidades;

[TestClass]
[DoNotParallelize] // Garante que os testes que mexem no banco rodem sequencialmente
public class AdministradorServicoTest
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

    private DbContexto PrepararBancoLimpo()
    {
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores"); // Limpa a tabela
        return context;
    }

    [TestMethod]
    public void TestandoSalvarAdministrador()
    {
        // Arrange
        var context = PrepararBancoLimpo();

        var adm = new Administrador
        {
            Email = "teste@teste.com",
            Senha = "teste",
            Perfil = "Adm"
        };

        var administradorServico = new AdministradorServico(context);

        // Act
        administradorServico.Incluir(adm);

        // Assert
        var todos = administradorServico.Todos(1); // ou Todos() se o método aceitar
        Assert.AreEqual(1, todos.Count(), "Deve haver exatamente 1 administrador na tabela.");
        var admSalvo = todos.First();
        Assert.AreEqual("teste@teste.com", admSalvo.Email, "O email do administrador salvo deve corresponder ao enviado.");
        Assert.AreEqual("teste", admSalvo.Senha, "A senha deve corresponder à enviada.");
        Assert.AreEqual("Adm", admSalvo.Perfil, "O perfil deve corresponder ao enviado.");
    }

    [TestMethod]
    public void TestandoBuscaPorId()
    {
        // Arrange
        var context = PrepararBancoLimpo();

        var adm = new Administrador
        {
            Email = "teste@teste.com",
            Senha = "teste",
            Perfil = "Adm"
        };

        var administradorServico = new AdministradorServico(context);

        // Act
        administradorServico.Incluir(adm);
        var admDoBanco = administradorServico.BuscaPorId(adm.Id);

        // Assert
        Assert.IsNotNull(admDoBanco, "Administrador não deve ser nulo após inclusão.");
        Assert.AreEqual(adm.Id, admDoBanco.Id, "O Id do administrador buscado deve corresponder ao inserido.");
        Assert.AreEqual(adm.Email, admDoBanco.Email, "O email deve corresponder ao inserido.");
        Assert.AreEqual(adm.Senha, admDoBanco.Senha, "A senha deve corresponder ao inserido.");
        Assert.AreEqual(adm.Perfil, admDoBanco.Perfil, "O perfil deve corresponder ao inserido.");
    }
}
