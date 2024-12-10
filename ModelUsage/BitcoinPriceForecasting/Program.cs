using BitcoinPriceForecasting.Components;
using Common;
using Common.Entities;
using Microsoft.Extensions.ML;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddPredictionEnginePool<HistoricalDataRecord, HictoricalDataPredictionResult>()
    .FromFile(modelName: "FastTree",
    filePath: builder.Configuration["ModelPath:SDCA"],
    watchForChanges: true);

builder.Services.AddScoped<CryptoDataFetcher>(provider =>
{
    return new CryptoDataFetcher(new HttpClient(), "sd");
});

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
