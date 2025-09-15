using MinimalApi.Dominio.Enuns;

namespace MinimalApi.Dominio.ModelViwes;

public record AdministradorLogado
{
    
    public string Email { get; set; } = default!;

    public string Perfil { get; set; } = default!;
    public string Token { get; set; } = default!;
    

}