using AlquilaFacilPlatform.Booking.Application.CommandServices;
using AlquilaFacilPlatform.Booking.Application.OutBoundService;
using AlquilaFacilPlatform.Booking.Application.QueryServices;
using AlquilaFacilPlatform.Booking.Domain.Repositories;
using AlquilaFacilPlatform.Booking.Domain.Services;
using AlquilaFacilPlatform.Booking.Infrastructure.Persistence.EFC.Repositories;
using AlquilaFacilPlatform.IAM.Application.Internal.CommandServices;
using AlquilaFacilPlatform.IAM.Application.Internal.OutboundServices;
using AlquilaFacilPlatform.IAM.Application.Internal.QueryServices;
using AlquilaFacilPlatform.IAM.Domain.Model.Commands;
using AlquilaFacilPlatform.IAM.Domain.Respositories;
using AlquilaFacilPlatform.IAM.Domain.Services;
using AlquilaFacilPlatform.IAM.Infrastructure.Hashing.BCrypt.Services;
using AlquilaFacilPlatform.IAM.Infrastructure.Persistence.EFC.Respositories;
using AlquilaFacilPlatform.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using AlquilaFacilPlatform.IAM.Infrastructure.Tokens.JWT.Configuration;
using AlquilaFacilPlatform.IAM.Infrastructure.Tokens.JWT.Services;
using AlquilaFacilPlatform.IAM.Interfaces.ACL;
using AlquilaFacilPlatform.IAM.Interfaces.ACL.Service;
using AlquilaFacilPlatform.Locals.Application.Internal.CommandServices;
using AlquilaFacilPlatform.Locals.Application.Internal.OutboundServices;
using AlquilaFacilPlatform.Locals.Application.Internal.QueryServices;
using AlquilaFacilPlatform.Locals.Domain.Model.Commands;
using AlquilaFacilPlatform.Locals.Domain.Repositories;
using AlquilaFacilPlatform.Locals.Domain.Services;
using AlquilaFacilPlatform.Locals.Infraestructure.Persistence.EFC.Repositories;
using AlquilaFacilPlatform.Locals.Interfaces.ACL;
using AlquilaFacilPlatform.Locals.Interfaces.ACL.Services;
using AlquilaFacilPlatform.Notifications.Application.CommandServices;
using AlquilaFacilPlatform.Notifications.Application.QueryServices;
using AlquilaFacilPlatform.Notifications.Domain.Repositories;
using AlquilaFacilPlatform.Notifications.Domain.Services;
using AlquilaFacilPlatform.Notifications.Infrastructure.Persistence.EFC.Repositories;
using AlquilaFacilPlatform.Profiles.Application.Internal.CommandServices;
using AlquilaFacilPlatform.Profiles.Application.Internal.OutboundServices;
using AlquilaFacilPlatform.Profiles.Application.Internal.QueryServices;
using AlquilaFacilPlatform.Profiles.Domain.Repositories;
using AlquilaFacilPlatform.Profiles.Domain.Services;
using AlquilaFacilPlatform.Profiles.Infrastructure.Persistence.EFC.Repositories;
using AlquilaFacilPlatform.Profiles.Interfaces.ACL;
using AlquilaFacilPlatform.Profiles.Interfaces.ACL.Services;
using AlquilaFacilPlatform.Shared.Domain.Repositories;
using AlquilaFacilPlatform.Shared.Infrastructure.Interfaces.ASP.Configuration;
using AlquilaFacilPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using AlquilaFacilPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using AlquilaFacilPlatform.Subscriptions.Application.Internal.CommandServices;
using AlquilaFacilPlatform.Subscriptions.Application.Internal.OutBoundServices;
using AlquilaFacilPlatform.Subscriptions.Application.Internal.QueryServices;
using AlquilaFacilPlatform.Subscriptions.Domain.Model.Commands;
using AlquilaFacilPlatform.Subscriptions.Domain.Repositories;
using AlquilaFacilPlatform.Subscriptions.Domain.Services;
using AlquilaFacilPlatform.Subscriptions.Infrastructure.Persistence.EFC.Repositories;
using AlquilaFacilPlatform.Subscriptions.Interfaces.ACL;
using AlquilaFacilPlatform.Subscriptions.Interfaces.ACL.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var developmentString = builder.Configuration.GetConnectionString("DevelopmentConnection");

// Configure Database Context and Logging Levels

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if (builder.Environment.IsDevelopment())
        {
            options.UseMySql(developmentString, ServerVersion.AutoDetect(developmentString))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        else if (builder.Environment.IsProduction())
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .LogTo(Console.WriteLine, LogLevel.Error)
                .EnableDetailedErrors();
        }
    });
// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "AlquilaFacil.API",
                Version = "v1",
                Description = "Alquila Facil API",
                TermsOfService = new Uri("https://alquila-facil.com/tos"),
                Contact = new OpenApiContact
                {
                    Name = "Alquila Facil",
                    Email = "contact@alquilaf.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            }
        });
    });

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
builder.Services.AddScoped<ISubscriptionQueryServices, SubscriptionQueryService>();
builder.Services.AddScoped<ISubscriptionContextFacade, SubscriptionContextFacade>();
builder.Services.AddScoped<ISubscriptionInfoExternalService,SubscriptionInfoExternalService>();
builder.Services.AddScoped<IExternalUserWithSubscriptionService, ExternalUserWithSubscriptionService>();

builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IPlanCommandService, PlanCommandService>();
builder.Services.AddScoped<IPlanQueryService, PlanQueryService>();


        

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Profiles Bounded Context Injection Configuration


builder.Services.AddScoped<ILocalCommandService, LocalCommandService>();
builder.Services.AddScoped<ILocalQueryService, LocalQueryService>();
builder.Services.AddScoped<ILocalsContextFacade, LocalsContextFacade>();
builder.Services.AddScoped<ILocalRepository, LocalRepository>();
builder.Services.AddScoped<ILocalCategoryRepository, LocalCategoryRepository>();

builder.Services.AddScoped<ILocalCategoryCommandService, LocalCategoryCommandService>();
builder.Services.AddScoped<ILocalCategoryQueryService, LocalCategoryQueryService>();

builder.Services.AddScoped<ICommentCommandService, CommentCommandService>();
builder.Services.AddScoped<ICommentQueryService, CommentQueryService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<IUserCommentExternalService, UserCommentExternalService>();


// Booking Bounded Context Injection Configuration
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationCommandService, ReservationCommandService>();
builder.Services.AddScoped<IReservationQueryService, ReservationQueryService>();
builder.Services.AddScoped<IReservationLocalExternalService, ReservationLocalExternalService>();
builder.Services.AddScoped<IUserReservationExternalService, UserReservationExternalService>();


builder.Services.AddScoped<ISubscriptionStatusRepository, SubscriptionStatusRepository>();
builder.Services.AddScoped<ISubscriptionStatusCommandService, SubscriptionStatusCommandService>();


// Profiles Bounded Context Injection Configuration
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();
builder.Services.AddScoped<IUserExternalService, UserExternalService>();
builder.Services.AddScoped<ISubscriptionExternalService, SubscriptionExternalService>();


builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<ISeedUserRoleCommandService, SeedUserRoleCommandService>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationCommandService, NotificationCommandService>();
builder.Services.AddScoped<INotificationQueryService, NotificationQueryService>();

// IAM Bounded Context Injection Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();


builder.Services.AddScoped<IReportCommandService, ReportCommandService>();
builder.Services.AddScoped<IReportQueryService, ReportQueryService>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    var planCommandService = services.GetRequiredService<IPlanCommandService>();
    await planCommandService.Handle(new CreatePlanCommand("Plan Premium", "El plan premium te permitira acceder a funcionalidades adicionales en la aplicacion", 20));
    
    var subscriptionStatusCommandService = services.GetRequiredService<ISubscriptionStatusCommandService>();
    await subscriptionStatusCommandService.Handle(new SeedSubscriptionStatusCommand());
    
    var userRoleCommandService = services.GetRequiredService<ISeedUserRoleCommandService>();
    await userRoleCommandService.Handle(new SeedUserRolesCommand());
    
    var localCategoryTypeCommandService = services.GetRequiredService<ILocalCategoryCommandService>();
    await localCategoryTypeCommandService.Handle(new SeedLocalCategoriesCommand());
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
