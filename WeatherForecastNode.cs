using GraphQL;
using GraphQL.Types;
using System;

namespace GQLWeatherDemo
{
    public class WeatherForecastNode : ObjectGraphType<WeatherForecast>
    {
        public enum TemperatureUnit
        {
            Celsius,
            Fahrenheit
        }

        public WeatherForecastNode()
        {
            Field(wf => wf.Date).Description("Date of the forecast");
            Field(wf => wf.TemperatureC).Description("Temperature in C");
            Field(wf => wf.TemperatureF).Description("Temperature in F");
            Field(wf => wf.Summary).Description("Summary");

            Field<FloatGraphType>("temperature", arguments: new QueryArguments(
                new QueryArgument<EnumerationGraphType<TemperatureUnit>> { Name = "unit" }
            ), resolve: (context) =>
            {
                var unit = context.GetArgument<TemperatureUnit>("unit");
                var temperatureField = unit switch
                {
                    TemperatureUnit.Celsius => context.ParentType.GetField("temperatureC"),
                    TemperatureUnit.Fahrenheit => context.ParentType.GetField("temperatureF"),
                    _ => throw new Exception("Invalid temperature unit")
                };

                var temperature = temperatureField.Resolver.Resolve(context);
                return temperature;
            });
        }
    }
}
