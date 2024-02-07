using BuisnessLogicLayer.Profiles;
using FluentValidation.AspNetCore;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(t => t.RegisterValidatorsFromAssemblyContaining<RegisterUserModelValidator>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddCors();

builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

builder.Services.ConfigureSqlContext(configuration);

builder.Services.ConfigureIdentity();

builder.Logging.ConfigureLogger(configuration);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(TaskTrackingProfile).Assembly);

builder.Services.ConfigureServices();

builder.Services.ConfigureOptions(configuration);

builder.Services.ConfigureAuthentication(configuration);

var app = builder.Build();

app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }