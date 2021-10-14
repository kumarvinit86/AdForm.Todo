using Adform.Todo.Database;
using Adform.Todo.Database.Sql;
using Adform.Todo.Model.Entity;
using Adform.Todo.Wireup.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SimpleInjector;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Adform.Todo.Model.Models;
using SeriLogger.DbLogger;
using System;
using Adform.Todo.DomainService.Default;
using Adform.Todo.DomainService;
using Adform.Todo.Manager;
using Adform.Todo.Manager.Default;

namespace Adform.Todo.Wireup
{
    public static class ApplicationWireup
    {
        public static Container Container = new Container();
        private static IServiceCollection _services;

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

            _services = services;
            JsonConvert.DefaultSettings = ConfigureDefaultJsonSettings;
            services.AddSimpleInjector(Container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();
            });
            InitializeContainer(configuration);
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseSimpleInjector(Container);
            AutoMapperConfiguration.ConfigureAutomapper(Container);
            Container.Verify();
        }

        public static void InitializeContainer(IConfiguration configuration)
        {
            DbLogger dbLogger = new DbLogger();
            dbLogger.Initialize(
                configuration.GetValue<string>("Connection:Data Source"),
                configuration.GetValue<string>("Connection:Initial Catalog"),
                new Tuple<bool, bool, bool>
                (configuration.GetValue<bool>("SeriLog:LogWarning"),
                configuration.GetValue<bool>("SeriLog:LogError"),
                configuration.GetValue<bool>("SeriLog:LogDebug")),
                new Tuple<string, string>(
                    configuration.GetValue<string>("Connection:UserName"),
                    configuration.GetValue<string>("Connection:Password")));
            Container.RegisterInstance<IDbLogger>(dbLogger);
            
            _services.AddSingleton(
                new DatabaseConnection()
                {
                    Server = configuration.GetValue<string>("Connection:Data Source"),
                    Database = configuration.GetValue<string>("Connection:Initial Catalog"),
                    UserName = configuration.GetValue<string>("Connection:UserName"),
                    Password = configuration.GetValue<string>("Connection:Password")
                });

            Container.Register<ILableCommand, LableCommand>(Lifestyle.Transient);
            Container.Register<ILableQuery, LableQuery>(Lifestyle.Transient);
            Container.Register<ITodoItemCommand, TodoItemCommand>(Lifestyle.Transient);
            Container.Register<ITodoItemQuery, TodoItemQuery>(Lifestyle.Transient);
            Container.Register<ITodoListCommand, TodoListCommand>(Lifestyle.Transient);
            Container.Register<ITodoListQuery, TodoListQuery>(Lifestyle.Transient);
            Container.Register<IUserQuery, UserQuery>(Lifestyle.Transient);

            Container.Register<ILableQueryManager, LableQueryManager>(Lifestyle.Transient);
            Container.Register<ILableCommandManager, LableCommandManager>(Lifestyle.Transient);
            Container.Register<ITodoItemCommandManager, TodoItemCommandManager>(Lifestyle.Transient);
            Container.Register<ITodoItemQueryManager, TodoItemQueryManager>(Lifestyle.Transient);
            Container.Register<ITodoListCommandManager, TodoListCommandManager>(Lifestyle.Transient);
            Container.Register<ITodoListQueryManager, TodoListQueryManager>(Lifestyle.Transient);
            Container.Register<IUserQueryManager, UserQueryManager>(Lifestyle.Transient);

            Container.Register<ICommandRepository<ToDoItem>, CommandRepository<ToDoItem>>(Lifestyle.Scoped);
            Container.Register<IQueryRepository<ToDoItem>, QueryRepository<ToDoItem>>(Lifestyle.Scoped);
            Container.Register<ICommandRepository<ToDoList>, CommandRepository<ToDoList>>(Lifestyle.Scoped);
            Container.Register<IQueryRepository<ToDoList>, QueryRepository<ToDoList>>(Lifestyle.Scoped);
            Container.Register<ICommandRepository<TodoLable>, CommandRepository<TodoLable>>(Lifestyle.Scoped);
            Container.Register<IQueryRepository<TodoLable>, QueryRepository<TodoLable>>(Lifestyle.Scoped);
            Container.Register<ICommandRepository<User>, CommandRepository<User>>(Lifestyle.Scoped);
            Container.Register<IQueryRepository<User>, QueryRepository<User>>(Lifestyle.Scoped);



        }

        private static JsonSerializerSettings ConfigureDefaultJsonSettings()
        {
            var settings = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter>
                {
                }
            };

            return settings;
        }
    }
}
