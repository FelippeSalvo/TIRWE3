using FitnessTracker.Config;
using FitnessTracker.Data;
using FitnessTracker.Helpers;
using FitnessTracker.Middleware;
using FitnessTracker.Repositories;
using FitnessTracker.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase);
builder.Services.AddEndpointsApiExplorer();

// Swagger Configuration com suporte a JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FitnessTracker API",
        Version = "v1",
        Description = "API para rastreamento de atividades físicas. Use o botão 'Authorize' para inserir o token JWT obtido no login."
    });

    // JWT Authentication - permite testar endpoints protegidos no Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header. Insira: Bearer {seu_token}. Token obtido em POST /api/auth/login",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

// MongoDB Configuration
var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
builder.Services.AddSingleton(mongoDbSettings!);
builder.Services.AddSingleton<IMongoContext>(sp => new MongoContext(mongoDbSettings!));

// AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(Program));

// JWT Authentication Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings!);
var key = Encoding.UTF8.GetBytes(jwtSettings!.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

// Dependency Injection - Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddScoped<IWorkoutLogRepository, WorkoutLogRepository>();
builder.Services.AddScoped<ITreinoRepository, TreinoRepository>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();

// Dependency Injection - Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<IWorkoutLogService, WorkoutLogService>();
builder.Services.AddScoped<IMetabolismoService, MetabolismoService>();
builder.Services.AddScoped<ITreinoService, TreinoService>();
builder.Services.AddScoped<ITreinoAnalysisService, TreinoAnalysisService>();
builder.Services.AddScoped<ISplitTemplateService, SplitTemplateService>();
builder.Services.AddScoped<IAdvancedTrainingAnalysisService, AdvancedTrainingAnalysisService>();

// Helpers
builder.Services.AddScoped<JwtHelper>();

// Seed de exercícios na inicialização (se collection vazia)
builder.Services.AddHostedService<FitnessTracker.Data.ExerciseSeedHostedService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitnessTracker API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

// Servir front-end estático da pasta Views (HTML/CSS/JS para testar a API)
var viewsPath = Path.Combine(app.Environment.ContentRootPath, "Views");
if (Directory.Exists(viewsPath))
{
    var provider = new PhysicalFileProvider(viewsPath);
    app.UseDefaultFiles(new DefaultFilesOptions { FileProvider = provider, DefaultFileNames = new List<string> { "index.html" } });
    app.UseStaticFiles(new StaticFileOptions { FileProvider = provider, RequestPath = "" });
}

// Global Exception Handler Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
