using HybirdCacheVideoTest;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
// builder.Services.AddSingleton<DataService>();
// builder.Services.AddSingleton<DataService, DataServiceWithMemoryCache>();
builder.Services.AddSingleton<DataService, DataServiceWithHybridCache>();
// builder.Services.AddSingleton<IDistributedCache, FileCache>();


#pragma warning disable EXTEXP0018 // experimental (pre-release)
builder.Services.AddHybridCache(
    options =>
    {
        options.DefaultEntryOptions = new()
        {
            Expiration = TimeSpan.FromSeconds(30),
            LocalCacheExpiration = TimeSpan.FromSeconds(10),
        };
    }
    );
#pragma warning restore EXTEXP0018 // experimental (pre-release)


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();