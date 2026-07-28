using RAG.Api.Configuracao;
using RAG.Aplicacao.Dto;

var builder = WebApplication.CreateBuilder(args);

string environmentName = builder.Environment.EnvironmentName;
IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();


AppSettingsConfiguracao.Carregar(builder.Services, configuration);
InjecaoDependenciaConfiguracao.RegistrarServicos(builder);
 
builder.Services.RegistrarCqrs();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");

var appSettings = app.Services.GetRequiredService<AppSettingsDto>();
if (appSettings.RateLimit.Habilitado)
    app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
