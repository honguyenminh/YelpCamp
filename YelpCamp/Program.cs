using YelpCamp.Components;
using YelpCamp.Models;
using YelpCamp.Service;

var builder = WebApplication.CreateBuilder(args);

// Use API hosting
builder.Services.AddControllers();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.Configure<MongoDbSetting>(
    builder.Configuration.GetSection("CampgroundDatabase"));
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddSingleton<CampgroundsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days.
    // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapControllers();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Services.GetService<CampgroundsService>();

app.Run();