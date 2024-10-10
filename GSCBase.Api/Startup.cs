using GSCBase.Application.Exceptions.Logs;
using GSCBase.Application.IServices.Base;
using GSCBase.Application.IServices.Cadastro;
using GSCBase.Application.ISevices.Auth;
using GSCBase.Application.Service.Base;
using GSCBase.Application.Services.Auth;
using GSCBase.Application.Services.Base;
using GSCBase.Application.Services.Cadastro;
using GSCBase.Domain.Entities.Auth;
using GSCBase.Domain.Models.Configs;
using GSCBase.Infrastructure.Contexts;
using GSCBase.Infrastructure.IRepositories.Auth;
using GSCBase.Infrastructure.IRepositories.Base;
using GSCBase.Infrastructure.IRepositories.Cadastro;
using GSCBase.Infrastructure.Repositories.Auth;
using GSCBase.Infrastructure.Repositories.Base;
using GSCBase.Infrastructure.Repositories.Cadastro;
using Hangfire;
using Hangfire.SqlServer;
using JobMonitor.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace GSCBase.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<Context>(options => options.UseSqlServer(connection));
            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connection, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true,
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("pt-BR"), new CultureInfo("pt-BR") };
            });
            ConfigurarTokenJWT(services, connection);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/";
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
           .AddEntityFrameworkStores<Context>()
           .AddDefaultTokenProviders();

            #region ServiceMapper
            //Services
            #region AUTH
            services.AddTransient<IAuthorizeService, AuthorizeService>();

            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            #endregion

            #region BASE
            services.AddTransient<IPessoaService, PessoaService>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IUnidadeService, UnidadeService>();
            services.AddTransient<IEstadoService, EstadoService>();
            services.AddTransient<ICidadeService, CidadeService>();
            services.AddTransient<IBairroService, BairroService>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<ITipoUsuarioService, TipoUsuarioService>();


            services.AddTransient<IPessoaRepository, PessoaRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IUnidadeRepository, UnidadeRepository>();
            services.AddTransient<IEstadoRepository, EstadoRepository>();
            services.AddTransient<ICidadeRepository, CidadeRepository>();
            services.AddTransient<IBairroRepository, BairroRepository>();
            services.AddTransient<ICommonRepository, CommonRepository>();
            services.AddTransient<ITipoUsuarioRepository, TipoUsuarioRepository>();
            #endregion        

            #region UTILS SERVICES
            services.AddTransient<IEmailService, EmailService>();
            #endregion

            services.AddTransient<IPublicidadeService, ProdutoService>();
            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<ICampanhaService, CampanhaService>();


            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<ICampanhaRepository, CampanhaRepository>();





            #endregion

            services.AddCors();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.MaxIAsyncEnumerableBufferLimit = 10000000;
            }).AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Context context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Configurar o CORS de maneira correta
            app.UseCors(cors =>
                cors.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseExceptionHandling();


            //HANGFIRE
            app.UseHangfireDashboard(
              "/hangfire", new DashboardOptions
              {
                  Authorization = new[] { new AuthFilter() }
              });

            //REGISTER ALL JOBS

            ////SGPR
            //RecurringJob.AddOrUpdate<GatewayService>("POLLING", m => m.SearchForOrder(1), "*/15 * * * * *", TimeZoneInfo.Local);
            //RecurringJob.AddOrUpdate<GatewayService>("SENDTOMERCHANT", m => m.SendAllOrders(), "*/15 * * * * *", TimeZoneInfo.Local);
            ////RecurringJob.AddOrUpdate<GatewayService>("SENDTOMERCHANT", m => m.SendAllOrders(), Cron.DayInterval(1), TimeZoneInfo.Local);

            app.UseMvc();

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            //string t = context.Database.GenerateCreateScript();
            context.Database.EnsureCreated();
        }

        private void ConfigurarTokenJWT(IServiceCollection services, string connection)
        {
            // Ativando o uso de cache em mem�ria
            services.AddMemoryCache();
            // Ativando o uso de cache via SqlServer
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = connection;
                options.SchemaName = "dbo";
                options.TableName = "AspNetUsersRefreshToken";
            });
            var tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfiguration"))
                .Configure(tokenConfigurations);

            var signingConfigurations = new SigningConfiguration(tokenConfigurations);
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddSingleton(tokenConfigurations);
            services.AddSingleton(signingConfigurations);
        }
    }
}
