using GQLWeatherDemo.Controllers;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Transports.AspNetCore.SystemTextJson;
using GraphQL.SystemTextJson;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GQLWeatherDemo
{
    public class WeatherSchema : GraphQL.Types.Schema
    {
        public WeatherSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<WeatherQuery>();
        }

    }

    public static class ServiceCollectionExtensions
    {
        public static void AddWeatherSchema(this IServiceCollection services)
        {
            services.AddSingleton<IGraphQLRequestDeserializer>(new GraphQLRequestDeserializer(options => { }));
            services.AddSingleton<IGraphQLExecuter<WeatherSchema>, DefaultGraphQLExecuter<WeatherSchema>>();
            services.AddTransient<WeatherForecastController>();

            GraphQL.MicrosoftDI.GraphQLBuilderExtensions.AddGraphQL(services)
                .AddSystemTextJson()
                .AddGraphTypes()
                .AddSchema<WeatherSchema>();
        }
    }
}
