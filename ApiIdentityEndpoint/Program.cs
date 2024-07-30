using ApiIdentityEndpoint.Data;
using ApiIdentityEndpoint.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDbContext<AppDbContext>(options => 
                                    options.UseSqlServer("Server=localhost;Database=IdentityEndpoints;Trusted_Connection=True;encrypt=false")
    );

// builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services
    .AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapSwagger().RequireAuthorization();

app.MapGet("/", () => "Hello World!");
app.MapIdentityApi<User>();

app.Run();
