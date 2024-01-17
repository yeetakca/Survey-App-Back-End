using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TestApp.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Use \"bearer {token}\" as a template.",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();

    options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}");
});

builder.Services.AddDbContext<SurveyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerExpressConnectionString")));

// Added for role based user auth.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(builder => builder
    .AllowAnyOrigin() // Allow requests from any origin
    .AllowAnyMethod() // Allow any HTTP method (e.g., GET, POST, PUT, DELETE)
    .AllowAnyHeader() // Allow any HTTP request headers
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Added for role based user auth.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();