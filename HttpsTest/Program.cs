using System.Net;
using HttpsTest;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(opt =>
    {
        var host = Dns.GetHostEntry("weather.io");
        opt.Listen(host.AddressList[0], 5000);
        opt.Listen(host.AddressList[0], 5001, listOpt =>
            {
                listOpt.UseHttps(builder.Configuration["CertPath"], builder.Configuration["CertPassword"]);
            }
        );
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();