using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace APIProductsVersion
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "My .NET API RestFull",
                Version = description.ApiVersion.ToString(),
                Description = "This is a API for Products",
                Contact = new OpenApiContact()
                {
                    Email = "ivanraul10@gmail.com",
                    Name = "Raul Dev"
                }
            };

            if (description.IsDeprecated)
                info.Description += " This API version has been deprecated";

            return info;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // Add Swagger Documentation for each API version we have
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

    }
}
