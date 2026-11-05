using RAG.Api.Configuracao;
using RAG.Api.Configuracao.Middleware;
using RAG.Aplicacao.Util;
using RAG.Infraestrutura.Contexto;

var builder = WebApplication.CreateBuilder(args);

var ambiente = builder.Environment.IsDevelopment() ? "appsettings.Development.json" : "appsettings.json";
IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{ambiente}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

PrintaConsole.Info("Carregando AppSettings");
AppSettingsConfiguracao.Carregar(builder.Services, configuration);

PrintaConsole.Info("Carregando injeção dependência");
InjecaoDependenciaConfiguracao.RegistrarServicos(builder);

PrintaConsole.Info("Carregando configuração do Swagger");
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


// Ativa monitor e RateLimiter
app.AtivarAppSettinngsConfiguracao();

////monitor para ativar o OnChange
//app.Services.GetRequiredService<MonitoraAppSettings>();

//var monitor = app.Services.GetRequiredService<IOptionsMonitor<AppSettingsDto>>();
//if (monitor.CurrentValue.RateLimit.Habilitado)
//    app.UseRateLimiter();


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
