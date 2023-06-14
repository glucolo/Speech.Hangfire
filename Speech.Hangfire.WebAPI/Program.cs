using Hangfire;
using Hangfire.Server;
using Hangfire.SqlServer;
using Speech.Hangfire.Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(setup =>
//{
//    setup.TagActionsBy(api => new[] { (api.ActionDescriptor as ControllerActionDescriptor)?.ControllerName });
//    //setup.CustomOperationIds(api => (api.ActionDescriptor.DisplayName as ControllerActionDescriptor)?.ActionName);
//    //setup.CustomOperationIds(api => api.ActionDescriptor.DisplayName);
//});
builder.Services.AddSwaggerGen(opt =>
{
    opt.EnableAnnotations();
});


/*****************************************************************************************************/
builder.Services.AddHangfire(cfg =>
{
    cfg.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
       .UseColouredConsoleLogProvider()
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
       .UseSqlServerStorage(builder.Configuration.GetConnectionString("Speech.HangFire"), new SqlServerStorageOptions
       {
           //common configuration to make before v1.8 (for backward compatibility)
           DisableGlobalLocks = true // Migration to Schema 7 is required
       });
});

//If you don’t add this, the jobs will be added to the database but not executed as expected.
//builder.Services.AddHangfireServer();

//Hangfire server uses worker threads to consume jobs
//Math.Min(Environment.ProcessorCount * 5, 20); (5 per processore con un massimo di 20)
//builder.Services.AddHangfireServer(opt => opt.WorkerCount = 30);

//Add continuos BackgroundProcess
//builder.Services.AddSingleton<IBackgroundProcess, Deamon>();
//builder.Services.AddHangfireServer();
/*****************************************************************************************************/


builder.Services.AddSingleton<TextWriter>(Console.Out);
builder.Services.AddTransient<IBusinessService, BusinessService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

/*****************************************************************************************************/
app.UseHangfireDashboard();
/*****************************************************************************************************/

app.MapControllers();

app.Run();
