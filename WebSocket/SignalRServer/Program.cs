using SignalRServer.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddSignalR();
WebApplication app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseCors(buildera =>
buildera.WithOrigins("null")
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials());

app.MapHub<ChatHub>("/chatHub");
app.Run();
