using Microsoft.AspNetCore.OData.Batch;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace MyWebApp.Api;

public static class EdmModelBuilder
{
    public static IEdmModel Build()
    {
        var builder = new ODataConventionModelBuilder();

        //builder.EntitySet<Person>("Persons");
        //builder.EntitySet<ContactInfo>("ContactInfos");

        //builder.ComplexType<PersonDto>().HasMany(x => x.ContactInfos);
        
        return builder.GetEdmModel();
    }
}

public static class MyODataBatchHandler
{
    public static DefaultODataBatchHandler Get()
    {
        var defaultBatchHandler = new DefaultODataBatchHandler
        {
            //MessageQuotas =
            //{
            //    MaxNestingDepth = 2,
            //    MaxOperationsPerChangeset = 10,
            //    MaxReceivedMessageSize = 100
            //}
        };
        return defaultBatchHandler;
    }
}

public class ODataBatchHttpContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _httpAccessor;

    public ODataBatchHttpContextMiddleware(RequestDelegate next, IHttpContextAccessor httpAccessor)
    {
        _next = next;
        _httpAccessor = httpAccessor;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _httpAccessor.HttpContext ??= context;

        await _next(context);
    }
}