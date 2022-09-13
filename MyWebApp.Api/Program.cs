using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using MyWebApp.Core.Entities;
using MyWebApp.Core.Repositories;
using MyWebApp.Persistence;
using MyWebApp.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Person>("Persons");
    builder.EntitySet<ContactInfo>("ContactInfos");
    return builder.GetEdmModel();
}

//builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var defaultBatchHandler = new DefaultODataBatchHandler();
defaultBatchHandler.MessageQuotas.MaxNestingDepth = 2;
defaultBatchHandler.MessageQuotas.MaxOperationsPerChangeset = 10;
defaultBatchHandler.MessageQuotas.MaxReceivedMessageSize = 100;
builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("v1", GetEdmModel(), defaultBatchHandler).Filter().Select().Expand().OrderBy().SetMaxTop(100));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Person API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Add the OData Batch middleware to support OData $Batch
app.UseODataBatching();
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
