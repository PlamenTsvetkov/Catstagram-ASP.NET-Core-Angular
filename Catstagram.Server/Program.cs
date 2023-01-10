using Catstagram.Server.Data;
using Catstagram.Server.Infrastructure;
using Catstagram.Server.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CatstagramDbContext>(options =>options
                .UseSqlServer(builder.Configuration.GetDefaultConnectionString()))
                .AddIdentity()
                .AddJwtAuthentication(builder.Services.GetApplicationSettings(builder.Configuration))
                .AddApplicationServices()
                .AddSwagger()
                .AddApiControllers();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//    app.UseDatabaseErrorPage();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

app
    .UseSwaggerUI()
    .UseRouting()
    .UseCors(options => options
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod())
    .UseAuthentication()
    .UseAuthorization()
    .ApplyMigrations();

app.MapControllers();

app.Run();
