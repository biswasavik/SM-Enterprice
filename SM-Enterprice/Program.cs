using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SM_Enterprice.Repositories.Interfaces;
using SM_Enterprice.Repositories.Repositories;
using SM_Enterprice.Services.Interfaces;
using SM_Enterprice.Services.Services;
using SM_Enterprice.Utilities.SM_Enter_Context;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});
builder.Services.AddHttpContextAccessor();
//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var uri = builder.WebHost.GetSetting(WebHostDefaults.ServerUrlsKey);

//builder.Services.AddSingleton<>

#region DI
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
#endregion


builder.Services.AddDbContext<CompanyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SM-DB"), b => b.MigrationsAssembly("SM-Enterprice"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.Use((context, next) =>
//{
//    context.Response.WriteAsync("Hello Avik!!");
//    return next();
//});

//app.Map("/Test", (appBuilder) => {
//    appBuilder.Run((context) =>
//    {
//        return context.Response.WriteAsync("Hello Avik!!");
//    });
//});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
