using Microsoft.EntityFrameworkCore;
using RAG.Aplicacao.Dto;
using RAG.Infraestrutura.Contexto;

namespace RAG.Api.Configuracao
{
    public static class AppSettingsConfiguracao
    {
        public static void Carregar(this IServiceCollection services, IConfigurationRoot configuration)
        {
            //classe que receber AppSettingsDto via injeção de dependência terá exatamente esse objeto, sem suporte a reload on change.
            AppSettingsDto appSettingsDto = configuration.Get<AppSettingsDto>() ?? new AppSettingsDto();
            services.AddSingleton(appSettingsDto);

            //é atualizado automaticamente se o arquivo appsettings.json mudar em tempo de execução.
            services.Configure<AppSettingsDto>(configuration);

            appSettingsDto = CarregaBancoDeDados(services, appSettingsDto);
            services.RegistrarRateLimit(appSettingsDto);
        }

        private static AppSettingsDto CarregaBancoDeDados(this IServiceCollection services, AppSettingsDto appSettingsDto)
        {
            var conexaoCommand = appSettingsDto.ConnectionStrings.ConexaoServidor;
            var conexaoQuery = string.IsNullOrWhiteSpace(appSettingsDto.ConnectionStrings.ConexaoServidorQuery)
                ? conexaoCommand
                : appSettingsDto.ConnectionStrings.ConexaoServidorQuery;
            var conexaoOllama = string.IsNullOrWhiteSpace(appSettingsDto.ConnectionStrings.ConexaoServidorOllama)
                ? conexaoCommand
                : appSettingsDto.ConnectionStrings.ConexaoServidorOllama;

            services.AddDbContext<CommandContexto>(opt => opt.UseSqlServer(conexaoCommand));
            services.AddDbContext<GenericoContexto>(opt => opt.UseSqlServer(conexaoCommand));
 

            return appSettingsDto;
        }
    }
}
