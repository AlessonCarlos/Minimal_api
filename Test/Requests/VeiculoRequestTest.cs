using System.Net;
using System.Text.Json;
using MinimalApi.Dominio.Entidades;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public class VeiculoRequestTest
{
    [ClassInitialize]
    public static void ClassInit(TestContext testContext)
    {
        Setup.ClassInit();
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        Setup.ClassCleanup();
    }

    [TestMethod]
    public async Task TestarGetVeiculos()
    {
        // GET /veiculos
        var response = await Setup.client.GetAsync("/veiculos");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        var veiculos = JsonSerializer.Deserialize<List<Veiculo>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(veiculos);
        Assert.IsTrue(veiculos!.Count > 0);

        foreach (var v in veiculos)
        {
            Assert.IsNotNull(v.Nome);
            Assert.IsNotNull(v.Marca);
        }
    }
}
