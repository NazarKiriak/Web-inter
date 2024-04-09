using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RESTwebAPI.Models.Auth;
using RESTwebAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HealthChecks.UI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddTransient<IExcelService, ExcelService>();
builder.Services.AddSingleton<IMyHealthCheck2, MyHealthCheck2>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WebAPI", Version = "v1" });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
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
                new string[] { }
            }
        });
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
         };
});


builder.Services.AddHealthChecks()
    .AddCheck<MyHealthCheck>("my_health_check")
    .AddCheck<MyHealthCheck>("my_service1_health_check");

//4
builder.Services.AddHealthChecks()
           .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), name: "sql-server-check");
//5
builder.Services.AddDbContext<HealthChecksDb>(options =>
{
    options.UseSqlServer("Server=DESKTOP-0USKCOF\\SQLEXPRESS;;Database=RestWepAPI;Trusted_Connection=True;TrustServerCertificate=True;");
}
);

var app = builder.Build();
app.UseAuthentication();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {

        c.OAuthClientId("swagger");
        c.OAuthAppName("Your API - Swagger");
    });

}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Map("/login/{username}", (string username) =>
{
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
    var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)); 

    return new JwtSecurityTokenHandler().WriteToken(jwt);
});
//2
app.UseHealthChecks("/healthcheck1", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("my_service1_health_check"),
});

//3
app.UseHealthChecks("/health");
//4
app.UseHealthChecks("/sqlserverhealth", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("sql_server_health_check"),
});

//5
app.UseHealthChecksUI(options =>
{
    options.UIPath = "/healthchecks-ui";
});

app.Run();
