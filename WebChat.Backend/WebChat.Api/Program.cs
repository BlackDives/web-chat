using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using WebChat.Api.Hubs;
using WebChat.Infrastructure.DataAccess;
using WebChat.Api.Extensions.ServiceExtensions;
using WebChat.Api.Policies;
using WebChat.Shared.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.Configure<GoogleAuthConfig>(builder.Configuration.GetSection("Google"));
builder.Services.AddSingleton<JsonWebTokenHandler>();
builder.Services.AddAppServices();
builder.Services.AddAppRepositories();
builder.Services.AddAuthorization();
builder.Services.AddAppAuthentication(configuration);
builder.Services.AddPersistence(configuration);
builder.Services.AddAppIdentity();
builder.Services.AddAppCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<WebChatDbContext>();
    dbContext.Database.Migrate();
    app.UseCors(CorsPolicies.WebChatCorsPolicy);
}

app.MapControllers();
app.MapHub<ChatHub>("/chatHub");

app.UseCors(CorsPolicies.WebChatCorsPolicy);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();