



using Microsoft.EntityFrameworkCore;
using P1WebAppRazor.Data;
using P1WebAppRazor.Interfaces;
using P1WebAppRazor.Services;


// ioc container 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// dependency injection 
builder.Services.AddDbContext<SqlDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("cloud")));

builder.Services.AddSingleton<ITokenService , TokenService>();
builder.Services.AddSingleton<IMailService , MailService>();



var app = builder.Build();



// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }


// middlewares 

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();


app.MapRazorPages()
 .WithStaticAssets();



app.Run();
