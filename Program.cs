using Exo.WebApi;
using Exo.WebApi.Contexts;
using Exo.WebApi.Repositories;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ExoContext, ExoContext>();
builder.Services.AddControllers();



// forma de autenticaçao .
 builder.Services.AddAuthentication(options =>{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
 } )
 //paremetros de validaçao do token.
 .AddJwtBearer("JwtBearer", options =>
 
 {
    options.TokenValidationParameters = new TokenValidationParameters{
        //valida quem esta solicitando.
        ValidateIssuer = true,
        //valida quem esta recebendo.
        ValidateAudience =true,
        //define se o tempo de expiraçao sera valido.
        ValidateLifetime = true,
        // Criptografia e validaçao da chave de autenticaçao.
        IssuerSigningKey = new
        SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao")),
        //valida o tempo de expiraçao do token.
        ClockSkew = TimeSpan.FromMinutes(30),
        //nome do issuer, da origem.
        ValidIssuer = "exoapi.webapi",
        // nome do audience, para o destino.
        ValidAudience = "exoapi.webapi"
    };
 });





builder.Services.AddTransient<ProjetoRepository, ProjetoRepository>();
builder.Services.AddTransient<UsuarioRepository, UsuarioRepository>();

var app = builder.Build();

app.UseRouting();

// habilita a autenticaçao.
app.UseAuthentication();
//habilita  a autorizaçao.
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
