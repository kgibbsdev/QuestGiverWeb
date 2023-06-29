using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using QuestGiver.Server.Data;
using System.Net.Http;
using System.Web;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<QuestGiverDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("QuestGiverDBConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	var context = services.GetRequiredService<QuestGiverDbContext>();
	
	DbInitializer.Initialize(context);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
