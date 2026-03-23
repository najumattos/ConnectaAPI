using Connectamente.API.Data;
using Connectamente.API.Middleware;
using Connectamente.API.Models;
using Connectamente.API.Services.AuthService;
using Connectamente.API.Services.PsicologoService;
using Connectamente.API.Services.PacienteService;
using Connectamente.API.Services.FileService;
using Connectamente.API.Services.JwtService;
using Connectamente.API.Services.UsuarioService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using dotenv.net;
using Connectamente.API.Services.ConsultaService;
using Connectamente.API.Services.ProntuarioService;

DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { "../.env" }));                            //Lê o arquivo .env
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables(); //adiciona variaveis de ambiente

//chama o front
builder.Services.AddCors(options =>
{
    options.AddPolicy("MinhaPolitica", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // URL do seu React
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Isso força a conversão de todos os Enums para String no JSON
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); ;

// Serviço de Conexão com o Banco
string conexao = builder.Configuration.GetConnectionString("DB_CONNECTION_STRING");
if (string.IsNullOrEmpty(conexao))
{    
    throw new Exception("A string de conexão não foi carregada. Verifique o arquivo .env!");
}
var versao = ServerVersion.AutoDetect(conexao);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(conexao, versao)
);

// Serviço de Autenticação e Autorização - Identity
builder.Services.AddIdentity<UsuarioModel, IdentityRole>(options =>
{
    // Configurar Senha
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    // Configurações de Bloqueio
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

    // Configuração Usuário
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Serviço JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];
if (string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("A chave secreta do JWT (SecretKey) não foi configurada no appsettings.json!");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero
    };
   
});

// Adicionar a Autorização
builder.Services.AddAuthorization();

// Serviço de Arquivos
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IFileService, FileService>();

// Registro dos Serviços Customizados
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();
builder.Services.AddScoped<IPsicologoService, PsicologoService>();
builder.Services.AddScoped<IProntuarioService, ProntuarioService>();




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Connectamente API",
        Version = "v1",
        Description = "API de fornecimento de dados Connectamente"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Cabeçalho da Autorização JWT. Exemplo: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer" //Type = SecuritySchemeType.Http, o campo Scheme deve ser escrito em minúsculo
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    // Isso limpa as redes conhecidas para que ele aceite os headers do proxy (comum em Docker/Hospedagens)
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});
var app = builder.Build();

// 1. Deve ser o primeiro para entender o protocolo original
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseForwardedHeaders();
// Garantir que o banco exista ao executar o projeto
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Connectamente v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();  //Se redireciona a autentica��o da errado

app.UseStaticFiles();

app.UseCors("MinhaPolitica"); 




app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();