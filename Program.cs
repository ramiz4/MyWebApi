using System.Reflection;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.AspNetCore.OData.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using MyWebApi.Infrastructure;
using MyWebApi.Models;


static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Person>("Persons");
    builder.EntitySet<ContactInfo>("ContactInfos");
    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);

var defaultBatchHandler = new DefaultODataBatchHandler();
defaultBatchHandler.MessageQuotas.MaxNestingDepth = 2;
defaultBatchHandler.MessageQuotas.MaxOperationsPerChangeset = 10;
defaultBatchHandler.MessageQuotas.MaxReceivedMessageSize = 100;

IEdmModel model0 = GetEdmModel();

builder.Services.AddControllers()
    .AddODataNewtonsoftJson()
    .AddOData(opt => 
        opt
        .Count().Filter().Expand().Select().OrderBy().SetMaxTop(100)
        .AddRouteComponents("odata", model0, services => services.AddSingleton<ODataBatchHandler, DefaultODataBatchHandler>())
    );


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Person API",
        Description = "An ASP.NET Core Web API for managing ToDo items"
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionConnection")));
}

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


var app = builder.Build();

// Add the OData Batch middleware to support OData $Batch
app.UseODataBatching();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
