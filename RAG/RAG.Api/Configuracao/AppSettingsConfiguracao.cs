using Microsoft.EntityFrameworkCore;
using RAG.Aplicacao.Dto;
using RAG.Infraestrutura.Contexto;

namespace RAG.Api.Configuracao
{
    public static class AppSettingsConfiguracao
    {
        public static void Carregar(this IServiceCollection services, IConfigurationRoot configuration)
        {
            AppSettingsDto appSettingsDto = configuration.Get<AppSettingsDto>() ?? new AppSettingsDto();


            appSettingsDto = CarregaBancoDeDados(services, configuration, appSettingsDto);
            services.RegistrarRateLimit(appSettingsDto);
            services.AddSingleton(appSettingsDto);

        }

        private static AppSettingsDto CarregaBancoDeDados(this IServiceCollection services, IConfigurationRoot configuration, AppSettingsDto appSettingsDto)
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
