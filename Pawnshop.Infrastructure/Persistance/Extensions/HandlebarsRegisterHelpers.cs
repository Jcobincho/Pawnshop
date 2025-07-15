using HandlebarsDotNet;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace Pawnshop.Infrastructure.Persistance.Extensions
{
    public static class HandlebarsRegisterHelpers
    {
        public static IServiceCollection HandlebarsRegisterSettings(this IServiceCollection services)
        {
            Handlebars.RegisterHelper("formatDate", (context, arguments) =>
            {
                if (arguments[0] is DateTime dateTime)
                {
                    return dateTime.ToString("dd.MM.yyyy");
                }

                return arguments[0]?.ToString() ?? "";
            });

            Handlebars.RegisterHelper("formatCurrency", (context, arguments) =>
            {
                if (arguments[0] is decimal value)
                {
                    return value.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
                }

                return arguments[0]?.ToString() ?? "";
            });

            Handlebars.RegisterHelper("formatNumber", (context, arguments) =>
            {
                if (arguments[0] is decimal value)
                {
                    return value.ToString("N2");
                }

                return arguments[0]?.ToString() ?? "";
            });

            Handlebars.RegisterHelper("multiply", (context, arguments) =>
            {
                if (arguments.Length >= 2 && arguments[0] is decimal a && arguments[1] is decimal b)
                {
                    return a * b;
                }

                return 0m;
            });

            return services;
        }
    }
}
