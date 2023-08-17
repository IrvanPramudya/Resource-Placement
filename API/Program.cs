using API.Contracts;
using API.Data;
using API.Repositories;
using API.Services;
using API.Utilities.Handlers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                        .ConfigureApiBehaviorOptions(options =>
                        {
                            options.InvalidModelStateResponseFactory = _context =>
                            {
                                var errors = _context.ModelState.Values
                                                     .SelectMany(v => v.Errors)
                                                     .Select(v => v.ErrorMessage);

                                return new BadRequestObjectResult(new ResponseValidationHandler
                                {
                                    Code = StatusCodes.Status400BadRequest,
                                    Status = HttpStatusCode.BadRequest.ToString(),
                                    Message = "Validation Error",
                                    Errors = errors.ToArray()
                                });
                            };
                        });

// Add services to the container.

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PlacementDbContext>(option => option.UseSqlServer(connection));

// Add Service Lifetime Repository
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IInterviewRepository, InterviewRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IGradeRepository, GradeRepository>();
builder.Services.AddScoped<IPlacementRepository, PlacementRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();

//Service Lifetime Services
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<InterviewService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<PlacementService>();
builder.Services.AddScoped<PositionService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AccountRoleService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<GradeService>();


//Register Fluent Validation
builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(
            option =>
            {
                option.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyOrigin();
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                    });
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
