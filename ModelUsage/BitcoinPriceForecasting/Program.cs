using BitcoinPriceForecasting;
using BitcoinPriceForecasting.Components;
using Common;
using Common.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddPredictionEnginePool<HistoricalDataRecord, HictoricalDataPredictionResult>()
    .FromFile(modelName: "FastForest",
    filePath: builder.Configuration["ModelPath:FastForest"],
    watchForChanges: true);

builder.Services.AddScoped<TimeSeriesForecastingService>(serviceProvider =>
{
    var filePath = builder.Configuration["ModelPath:ForecastBySsa"];
    return new TimeSeriesForecastingService(filePath);
});

builder.Services.AddSingleton<CryptoDataFetcher>(provider =>
{
    return new CryptoDataFetcher(new HttpClient(), "sd");
});

builder.Services.AddSingleton<CryptoDataStore>();

builder.Services.AddRadzenComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
