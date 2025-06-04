using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TaskManager.Api.Filters;
using TaskManager.Application.UseCases.Project.Create;
using TaskManager.Application.UseCases.Project.Delete;
using TaskManager.Application.UseCases.Project.GetAll;
using TaskManager.Application.UseCases.Project.GetById;
using TaskManager.Application.UseCases.Project.Update;
using TaskManager.Application.UseCases.Task.Create;
using TaskManager.Application.UseCases.User.Create;
using TaskManager.Application.UseCases.User.Login;
using TaskManager.Application.UseCases.User.Profile;
using TaskManager.Application.Validators.ProjectValidator;
using TaskManager.Domain.HttpContext;
using TaskManager.Domain.Repositories.ProjectRepositories;
using TaskManager.Domain.Repositories.TaskRepositories;
using TaskManager.Domain.Repositories.TokenRepositories;
using TaskManager.Domain.Repositories.UserRepositories;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.HttpContext;
using TaskManager.Infrastructure.Repositories.ProjectRepositories;
using TaskManager.Infrastructure.Repositories.TaskRepositories;
using TaskManager.Infrastructure.Repositories.TokenRepositories;
using TaskManager.Infrastructure.Repositories.UserRepositories;

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["SecretKey"];

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
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManager", Version = "v1" });
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter a valid JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new[] { "Bearer" }  }
    });
});
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilters)));

builder.Services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ICreateProjectUseCase, CreateProjectUseCase>();
builder.Services.AddScoped<IUserContext, UserContext>();

builder.Services.AddScoped<IGetAllProjectsUseCase, GetAllProjectsUseCase>();
builder.Services.AddScoped<IGetProjectByIdUseCase, GetProjectByIdUseCase>();
builder.Services.AddScoped<IValidator<Guid>, GetProjectByIdValidator>();
builder.Services.AddScoped<IUpdateProjectUseCase, UpdateProjectUseCase>();
builder.Services.AddScoped<IDeleteProjectUseCase, DeleteProjectUseCase>();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ICreateTaskUseCase, CreateTaskUseCase>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddValidatorsFromAssemblyContaining<UpdateProjectRequestValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// **ADICIONADO: Middleware de autenticação JWT**
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
