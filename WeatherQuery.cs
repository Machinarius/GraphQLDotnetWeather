using GQLWeatherDemo.Controllers;
using GraphQL.Types;
using System;
using System.Collections.Generic;

namespace GQLWeatherDemo
{
    public class WeatherQuery : ObjectGraphType
    {
        private readonly WeatherForecastController forescastsDataSource;

        public WeatherQuery(WeatherForecastController forescastsDataSource)
        {
            this.forescastsDataSource = forescastsDataSource ?? throw new ArgumentNullException(nameof(forescastsDataSource));

            SetupFields();
        }

        private void SetupFields()
        {
            Field<ListGraphType<WeatherForecastNode>>("forecasts", resolve: (context) => this.GetForecasts());
        }

        private IEnumerable<WeatherForecast> GetForecasts()
        {
            return forescastsDataSource.Get();
        }
    }
}
