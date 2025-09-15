using MinimalApi.Dominio.Entidades;
using MinimalApi.Dominio.Interfaces;

namespace Test.Mocks
{
    public class VeiculoServicoMock : IVeiculoServico
    {
        private List<Veiculo> veiculos;

        public VeiculoServicoMock()
        {
            veiculos = new List<Veiculo>();; // Inicializa lista padrão
        }

        public void Reset()
        {
            veiculos = new List<Veiculo>(); // Começa vazia para cada teste
        }

        public List<Veiculo> Todos(int? pagina, string? nome = null, string? marca = null)
        {
            IEnumerable<Veiculo> query = veiculos;

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(v => v.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(marca))
                query = query.Where(v => v.Marca.Contains(marca, StringComparison.OrdinalIgnoreCase));

            return query.ToList();
        }

        public Veiculo? BuscaPorId(int id)
        {
            return veiculos.FirstOrDefault(v => v.Id == id);
        }

        public void Incluir(Veiculo veiculo)
        {
            veiculo.Id = veiculos.Count > 0 ? veiculos.Max(v => v.Id) + 1 : 1;
            veiculos.Add(veiculo);
        }

        public void Atualizar(Veiculo veiculo)
        {
            var existente = BuscaPorId(veiculo.Id);
            if (existente != null)
            {
                existente.Nome = veiculo.Nome;
                existente.Marca = veiculo.Marca;
                existente.Ano = veiculo.Ano;
            }
        }

        public void Apagar(Veiculo veiculo)
        {
            veiculos.Remove(veiculo);
        }
    }
}
