using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
var builder = WebApplication.CreateBuilder(args);
logger.Info("Starting...");

//自定义Services


//Nlog Services
builder.Logging.ClearProviders();
builder.Host.UseNLog();

//网页后端Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


try
{
    await Task.WhenAll(SetupWebBackend(), SetupTcpSockets());
}
catch (Exception e)
{
    logger.Error(e.Message);
    logger.Error(e.StackTrace);
}
logger.Info("Application Ended");


Task SetupWebBackend()
{
    return Task.Run(() =>
    {
        var app = builder!.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    });
}

Task SetupTcpSockets()
{
    return Task.Run(() =>
    {

    });
}