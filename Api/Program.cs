// using MinimalApi.Infraestrutura.Db;
// using MinimalApi.DTOs;
// using Microsoft.EntityFrameworkCore;
// using MinimalApi.Dominio.Interfaces;
// using MinimalApi.Dominio.Serviços;
// using Microsoft.AspNetCore.Mvc;
// using MinimalApi.Dominio.ModelViwes;
// using MinimalApi.Dominio.Entidades;
// using MinimalApi.Dominio.Enuns;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using Microsoft.OpenApi.Models;
// using Microsoft.AspNetCore.Authorization;
// using MinimalApi.Configuracoes.Swagger;

// #region Biulder


// var builder = WebApplication.CreateBuilder(args);

// var key = builder.Configuration.GetSection("Jwt").ToString();
// if (string.IsNullOrEmpty(key)) key = "123456";

// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(options =>
// {
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateLifetime = true,
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))


//     };
// });

// builder.Services.AddAuthorization();

// builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
// builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(options =>
// {
//     options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//     {
//         Name = "Authorization",
//         Type = SecuritySchemeType.Http,
//         Scheme = "bearer",
//         BearerFormat = "JWT",
//         In = ParameterLocation.Header,
//         Description = "Insira o toke JWT desta maneira: Bearer {Seu token}"
//     });

//     options.AddSecurityRequirement(new OpenApiSecurityRequirement
//     {
//         {
//             new OpenApiSecurityScheme
//             {
//                 Reference = new OpenApiReference
//                 {
//                     Type = ReferenceType.SecurityScheme,
//                     Id = "Bearer"
//                 }
//             },
//             new string[] {}
//         }
//     });
    
//         options.OperationFilter<AllowAnonymousOperationFilter>();
// });


// builder.Services.AddDbContext<DbContexto>(options =>
// {
//     options.UseMySql(
//         builder.Configuration.GetConnectionString("mysql"),
//         ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
//         );
// });

// var app = builder.Build();

// #endregion

// #region Home
// // app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");

// // app.MapGet("/", () => Results.Content(@"
// //     <html>
// //         <body>
// //             <h1>Bem vindo a API de veiculos - Minimal API</h1>
// //             <a href=""/swagger"">Documentacao Swagger</a>
// //         </body>
// //     </html>", "text/html")).WithTags("Home");

// app.MapGet("/", [AllowAnonymous] () => Results.Content(@"
// <!DOCTYPE html>
// <html lang=""pt-BR"">
// <head>
//     <meta charset=""UTF-8"" />
//     <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
//     <title>API de Veículos - Minimal API</title>
//     <style>
//         body {
//             font-family: Arial, sans-serif;
//             background: #f4f6f9;
//             color: #333;
//             margin: 0;
//             padding: 0;
//             display: flex;
//             align-items: center;
//             justify-content: center;
//             height: 100vh;
//         }
//         .container {
//             background: #fff;
//             padding: 2rem 3rem;
//             border-radius: 12px;
//             box-shadow: 0 4px 12px rgba(0,0,0,0.1);
//             text-align: center;
//             max-width: 500px;
//         }
//         h1 {
//             color: #1e40af;
//             margin-bottom: 1rem;
//         }
//         p {
//             margin-bottom: 1.5rem;
//         }
//         a {
//             display: inline-block;
//             padding: 0.75rem 1.5rem;
//             background: #1e40af;
//             color: #fff;
//             border-radius: 8px;
//             text-decoration: none;
//             font-weight: bold;
//             transition: background 0.3s;
//         }
//         a:hover {
//             background: #2563eb;
//         }
//     </style>
// </head>
// <body>
//     <div class=""container"">
//         <h1>API de Veículos</h1>
//         <p>API de veículos desenvolvida com <strong>Minimal API</strong>.</p>
//         <p>Entrar no Swagger:</p>
//         <a href=""/swagger"">Swagger</a>
//     </div>
// </body>
// </html>
// ", "text/html")).WithTags("Home");

// #endregion

// #region  Administradores

// string GerarTokenJwt(Administrador administrador)
// {
//     if (string.IsNullOrEmpty(key)) return string.Empty;

//     var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
//     var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//     var claims = new List<Claim>()
//     {
//         new Claim("Email", administrador.Email),
//         new Claim("Perfil", administrador.Email),
//         new Claim(ClaimTypes.Role, administrador.Perfil),
//     };
//     var token = new JwtSecurityToken(
//         expires: DateTime.Now.AddDays(1),
//         signingCredentials: credentials

//     );

//     return new JwtSecurityTokenHandler().WriteToken(token);
// }
// // Login
// app.MapPost("/administradores/login", [AllowAnonymous]  ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) =>
// {
//     var adm = administradorServico.Login(loginDTO);

//     if (adm != null)
//     {
//         string token = GerarTokenJwt(adm);
//         return Results.Ok(new AdministradorLogado
//         {
//             Email = adm.Email,
//             Perfil = adm.Perfil,
//             Token = token
//         });
//     }

//     // se não logou, devolve 401
//     return Results.Unauthorized();
// }).WithTags("Administrador");


// // Criar administrador
// app.MapPost("/administradores", ([FromBody] AdministradorDTO administradorDTO, IAdministradorServico administradorServico) =>
// {
//     var validacao = new ErrosDeValidacao
//     {
//         Mensagens = new List<string>()
//     };

//     if (string.IsNullOrWhiteSpace(administradorDTO.Email))
//         validacao.Mensagens.Add("O Email não pode ser vazio.");
//     if (string.IsNullOrWhiteSpace(administradorDTO.Senha))
//         validacao.Mensagens.Add("A Senha não pode ser vazia.");
//     if (administradorDTO.Perfil == null)
//         validacao.Mensagens.Add("O Perfil não pode ser vazio.");

//     if (validacao.Mensagens.Count > 0)
//         return Results.BadRequest(validacao);

//     var administrador = new Administrador
//     {
//         Email = administradorDTO.Email,
//         Senha = administradorDTO.Senha,
//         Perfil = administradorDTO.Perfil?.ToString() ?? "Editor"
//     };

//     administradorServico.Incluir(administrador);

//     return Results.Created($"/administradores/{administrador.Id}", administrador);
// }).RequireAuthorization(new AuthorizeAttribute {Roles = "Adm" }).WithTags("Administrador");

// // Listar todos os administradores
// app.MapGet("/administradores", ([FromQuery] int? pagina, IAdministradorServico administradorServico) =>
// {
//     var administradores = administradorServico.Todos(pagina);
//     var adms = administradores.Select(adm => new AdministradorModelView
//     {
//         Id = adm.Id,
//         Email = adm.Email,
//         Perfil = adm.Perfil
//     }).ToList();

//     if (!adms.Any())
//         return Results.NotFound(new { mensagem = "Nenhum administrador encontrado." });

//     return Results.Ok(adms);
// }).RequireAuthorization()
// .RequireAuthorization(new AuthorizeAttribute {Roles = "Adm" })
// .WithTags("Administrador");

// // Buscar administrador por ID
// app.MapGet("/administradores/{id}", ([FromRoute] int id, IAdministradorServico administradorServico) =>
// {
//     var administrador = administradorServico.BuscaPorId(id);
//     if (administrador == null)
//         return Results.NotFound(new { mensagem = "Administrador não encontrado." });

//     return Results.Ok(administrador);
// }).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute {Roles = "Adm" }).WithTags("Administrador");
// #endregion

// #region Veiculos

// ErrosDeValidacao validaDTO(VeiculoDTO veiculoDTO)
// {
//     var validacao = new ErrosDeValidacao
//     {
//         Mensagens = new List<string>()
//     };

//     if (string.IsNullOrEmpty(veiculoDTO.Nome))
//         validacao.Mensagens.Add("O nome não pode ser vazio");

//     if (string.IsNullOrEmpty(veiculoDTO.Marca))
//         validacao.Mensagens.Add("O marca não pode ser vazia");

//     if (veiculoDTO.Ano < 1900)
//         validacao.Mensagens.Add("Veiculo não atende ao criterio mínimo de idade");

//     if (veiculoDTO.Ano > DateTime.Now.Year + 1)
//         validacao.Mensagens.Add("Ano do veículo inválido.");

//     return validacao;
    
    

// }

// // Adicionar veiculo
// app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
// {
//     var validacao = validaDTO(veiculoDTO);
//     if (validacao.Mensagens.Count > 0)
//         return Results.BadRequest(validacao);

//     var veiculo = new Veiculo
//     {
//         Nome = veiculoDTO.Nome,
//         Marca = veiculoDTO.Marca,
//         Ano = veiculoDTO.Ano
//     };

//     veiculoServico.Incluir(veiculo);

//     return Results.Created($"/veiculos/{veiculo.Id}", veiculo);
// }).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute {Roles = "Adm,Editor" }).WithTags("Veiculo");


// app.MapGet("/veiculos", ([FromQuery] int? pagina, IVeiculoServico veiculoServico) =>
// {
//     try
//     {
//         var veiculos = veiculoServico.Todos(pagina);

//         if (veiculos == null || !veiculos.Any())
//             return Results.NotFound(new { mensagem = "Nenhum veículo encontrado." });

//         return Results.Ok(veiculos);
//     }
//     catch (Exception ex)
//     {
//         return Results.Problem(
//             detail: ex.Message,
//             statusCode: 500,
//             title: "Erro interno ao listar veículos"
//         );
//     }
// }).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute {Roles = "Adm,Editor" }).WithTags("Veiculo");



// //Listar veiculo
// app.MapGet("/veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
// {

    
//     try
//     {
//         var veiculo = veiculoServico.BuscaPorId(id);

//         if (veiculo == null)
//             return Results.NotFound(new { mensagem = "Veículo não encontrado." });

//         return Results.Ok(veiculo);
//     }
//     catch (Exception ex)
//     {

//         return Results.Problem(
//             detail: ex.Message,
//             statusCode: 500,
//             title: "Erro interno ao buscar veículo"
//         );
//     }
// }).RequireAuthorization().WithTags("Veiculo");

// //Atualizar veiculo
// app.MapPut("/veiculos/{id}", ([FromRoute] int id, VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
// {
//     try
//     {
//         var veiculo = veiculoServico.BuscaPorId(id);

//         if (veiculo == null)
//             return Results.NotFound(new { mensagem = "Veículo não encontrado." });

//         veiculo.Nome = veiculoDTO.Nome;
//         veiculo.Marca = veiculoDTO.Marca;
//         veiculo.Ano = veiculoDTO.Ano;

//         veiculoServico.Atualizar(veiculo);

//         return Results.Ok(new { mensagem = "Veículo atualizado com sucesso", veiculo }); ;
//     }


//     catch (Exception ex)
//     {

//         return Results.Problem(
//             detail: ex.Message,
//             statusCode: 500,
//             title: "Erro interno ao buscar veículo"
//         );
//     }


// }).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute {Roles = "Adm" }).WithTags("Veiculo");

// //Delete veiculo
// app.MapDelete("/veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
// {
//     try
//     {
//         var veiculo = veiculoServico.BuscaPorId(id);

//         if (veiculo == null)
//             return Results.NotFound(new { mensagem = "Veículo não encontrado." });

//         veiculoServico.Apagar(veiculo);

//         return Results.Ok(new { mensagem = "Veículo Apagado com sucesso", veiculo }); ;
//     }


//     catch (Exception ex)
//     {

//         return Results.Problem(
//             detail: ex.Message,
//             statusCode: 500,
//             title: "Erro interno ao buscar veículo"
//         );
//     }


// }).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute {Roles = "Adm" }).WithTags("Veiculo");


// #endregion

// #region App
// app.UseSwagger();
// app.UseSwaggerUI();

// app.UseAuthentication();
// app.UseAuthorization();

// app.Run();
// #endregion
using MinimalApi;

IHostBuilder CreateHostBuilder(string[] args){
  return Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    });
}

CreateHostBuilder(args).Build().Run();