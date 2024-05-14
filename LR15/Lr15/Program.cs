using LR14.BackgroundServices;
using LR14.Database;
using LR14.Hubs;
using LR14.Interfaces;
using LR14.Jobs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Quartz;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var mysqlConnection = builder.Configuration.GetConnectionString("MySQLConnection");

builder.Services.AddRazorPages();

builder.Services.AddHostedService<WebsiteAvailabilityService>();
builder.Services.AddHttpClient();

builder.Services.AddSendGrid(options =>
            options.ApiKey = builder.Configuration.GetValue<string>("SendGridApiKey"));

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    string jobSchedule = builder.Configuration.GetValue<string>("JobSchedule");
    q.AddJob<SendMailJob>(j => j
        .WithIdentity("SendRecurringMailJob")
        .WithDescription("This trigger will run every 30 seconds to send emails.")
        .Build()
    );

    q.AddTrigger(t => t
        .WithIdentity("SendRecurringMailJobTrigger")
        .ForJob("SendRecurringMailJob")
        .StartNow()
        .WithCronSchedule(jobSchedule)
    );
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

builder.Services.AddMemoryCache();
builder.Services.AddSignalR();

builder.Services.AddHostedService<ExchangeRateService>();
builder.Services.AddHostedService<NotifyService>();
builder.Services.AddLogging();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<ISendMail, SendMail>();

builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<DbChangeData>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();

    MySqlConnection connection = new MySqlConnection(mysqlConnection);
    connection.Open();

    MySqlCommand createTableFootballers = new MySqlCommand("create table Footballers (ID INT PRIMARY KEY NOT NULL, FullName varchar(255) NOT NULL, " +
        "Club varchar(255) NOT NULL, Age INT NOT NULL)", connection);
    createTableFootballers.ExecuteNonQuery();

    MySqlCommand insertFootballers = new MySqlCommand("insert into Footballers (ID, FullName, Club, Age) values (1, 'Cristiano Ronaldo', 'Al-Nasr', 39), " +
        "(2, 'Lionel Messi', 'Inter Miami', 36)", connection);
    insertFootballers.ExecuteNonQuery();

    MySqlCommand selectFootballers = new MySqlCommand("select ID, FullName, Club, Age from Footballers", connection);
    MySqlDataReader reader = selectFootballers.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine($"FootballerID: {reader.GetInt32(0)}, FullName: {reader.GetString(1)}, Club: {reader.GetString(2)} and Age: {reader.GetInt32(3)}");
    }
}

app.UseWebSockets();
app.MapHub<NotifyHub>("/notifyHub");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
