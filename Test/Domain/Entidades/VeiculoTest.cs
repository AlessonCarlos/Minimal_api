using MinimalApi.Dominio.Entidades;

namespace Test.Domain.Entidades;

[TestClass]
public sealed class VeiculoTests
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var veiculo = new Veiculo();

        // Act
        veiculo.Id = 10;
        veiculo.Nome = "Civic";
        veiculo.Marca = "Honda";
        veiculo.Ano = 2020;

        // Assert
        Assert.AreEqual(10, veiculo.Id);
        Assert.AreEqual("Civic", veiculo.Nome);
        Assert.AreEqual("Honda", veiculo.Marca);
        Assert.AreEqual(2020, veiculo.Ano);
    }
}
