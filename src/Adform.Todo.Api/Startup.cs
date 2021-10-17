using Adform.Todo.Api.GraphQl.Model;
using Adform.Todo.Api.GraphQl.Mutation;
using Adform.Todo.Api.GraphQl.Query;
using Adform.Todo.Api.Middleware;
using Adform.Todo.Api.SwaggerSupport;
using Adform.Todo.Dto;
using Adform.Todo.Wireup;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Text;

namespace Adform.Todo.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services
               .AddDataLoaderRegistry()
               .AddGraphQL(s => SchemaBuilder.New()
               .AddServices(s)
               .AddType<LabelType>()
               .AddType<ToDoListType>()
               .AddQueryType<Query>()
               .AddMutationType<Mutation>()
               .AddAuthorizeDirectiveType()
               .Create());

            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration.GetValue<string>("ValidAudience"),
                    ValidIssuer = Configuration.GetValue<string>("Issuer"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Secret_Key"))),
                    RequireExpirationTime = false
                };
            });

            services.AddControllers(setupaction =>
            {
                setupaction.ReturnHttpNotAcceptable = true;
                setupaction.OutputFormatters.Add(new XmlSerializerOutputFormatter());

                var jsonOutputFormatter = setupaction.OutputFormatters
                    .OfType<SystemTextJsonOutputFormatter>().FirstOrDefault();

                if (jsonOutputFormatter != null && jsonOutputFormatter.SupportedMediaTypes.Contains("text/json"))
                {
                    jsonOutputFormatter.SupportedMediaTypes.Remove("text/json");
                }

            });
            services.AddApiVersioning(setupaction =>
            {
                setupaction.AssumeDefaultVersionWhenUnspecified = true;
                setupaction.DefaultApiVersion = new ApiVersion(1, 0);
                setupaction.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(setupaction =>
            {
                setupaction.GroupNameFormat = "'v'VV";
                setupaction.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = "Authentication and Authorization in ASP.NET 5 with JWT and Swagger"
                });
                swagger.ExampleFilters();
                swagger.OperationFilter<AddResponseHeadersFilter>();
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter Bearer [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                            new OpenApiSecurityScheme
                                {
                                Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                            new string[] {}
                            }
                        });

            });
            services.AddSwaggerExamplesFromAssemblyOf<AppUser>();
            ApplicationWireup.ConfigureServices(services, Configuration);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSwagger();
            app.UseSwagger(c => c.SerializeAsV2 = true);
            app.UseSwaggerUI(setupAction =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    setupAction.SwaggerEndpoint(
                        $"/swagger/" + $"{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }

                setupAction.DefaultModelExpandDepth(2);
                setupAction.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
                setupAction.EnableDeepLinking();
                setupAction.DisplayOperationId();
            });

            app.UseGraphQL();
            app.UsePlayground();
            ApplicationWireup.Configure(app);
            app.UseRequestResponseLogging();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
