using Microsoft.EntityFrameworkCore;
using RAG.Api.Configuracao;
using RAG.Api.Middleware;
using RAG.Aplicacao.Dto;
using RAG.Infraestrutura.Contexto;

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
ApiConfiguracao.ConfiguracaoSwagger(builder.Services);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CommandContexto>();
    dbContext.Database.EnsureCreated();
}

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html");
        return;
    }
    await next();
});
app.UseSwagger();
app.UseSwaggerUI();
app.ConfigurarMiddlewaresApi();
app.UseCors("AllowAll");

var appSettings = app.Services.GetRequiredService<AppSettingsDto>();
if (appSettings.RateLimit.Habilitado)
    app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
