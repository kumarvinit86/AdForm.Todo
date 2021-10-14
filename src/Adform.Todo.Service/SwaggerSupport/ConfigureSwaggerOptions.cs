using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Adform.Todo.Service.SwaggerSupport
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
	{
		private readonly IApiVersionDescriptionProvider _provider;

		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
		/// </summary>
		/// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
		public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

		public void Configure(SwaggerGenOptions options)
		{
			// add a swagger document for each discovered API version
			foreach (var description in _provider.ApiVersionDescriptions)
				options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
		}

		private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
		{
			var info = new OpenApiInfo()
			{
				Title = "Adform.Service",
				Version = description.ApiVersion.ToString(),
				Description = "Through this API, QualityAuditSuite details can be access.",
				Contact = new OpenApiContact { Name = "Adform.Service", Url = new Uri("https://swagger.io") }
			};

			if (description.IsDeprecated)
				info.Description += " This API version has been deprecated.";

			return info;
		}
	}
}
