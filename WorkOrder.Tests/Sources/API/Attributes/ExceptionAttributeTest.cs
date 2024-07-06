using WorkOrder.API.Attributes;
using WorkOrder.Domain.Enums;
using WorkOrder.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace WorkOrder.Tests.Sources.API.Attributes;

public class ExceptionAttributeTest
{
    private readonly ActionContext _actionContext;
    private readonly List<IFilterMetadata> _filterMetadata;

    public ExceptionAttributeTest()
    {
        _actionContext = new ActionContext
        {
            ActionDescriptor = new ActionDescriptor(),
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData()
        };
        _filterMetadata = new List<IFilterMetadata>();
    }

    [Fact(DisplayName = "Activate exception information")]
    public async Task OnExceptionInformationExceptionAsync()
    {
        var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
        {
            Exception = new InformationException(StatusException.NotFound, "No data found")
        };

        var exceptionFilter = new ExceptionAttribute();

        var result = await Record.ExceptionAsync(() => exceptionFilter.OnExceptionAsync(exceptionContext));
        Assert.Null(result);
    }

    [Fact(DisplayName = "Activate exception")]
    public async Task OnExceptionIsExceptionAsync()
    {
        var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
        {
            Exception = new Exception("Erro inesperado.")
        };

        var exceptionFilter = new ExceptionAttribute();

        var result = await Record.ExceptionAsync(() => exceptionFilter.OnExceptionAsync(exceptionContext));
        Assert.Null(result);
    }

    [Fact(DisplayName = "Information exception inner exception")]
    public async Task OnExceptionInformationExceptionInnerExceptionAsync()
    {
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };

        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new InformationException(StatusException.NotFound, "No data found", new Exception("Inner Exception Error"))
        };
        var exception = new ExceptionAttribute();

        var result = await Record.ExceptionAsync(() => exception.OnExceptionAsync(exceptionContext));
        Assert.Null(result);
    }

    [Fact(DisplayName = "Exception Inner Exception")]
    public async Task OnExceptionInnerExceptionAsync()
    {
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };

        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new Exception("Generic error", new Exception("Inner Exception Error"))
        };
        var exception = new ExceptionAttribute();

        var result = await Record.ExceptionAsync(() => exception.OnExceptionAsync(exceptionContext));
        Assert.Null(result);
    }

    [Fact(DisplayName = "Information exception null")]
    public async Task OnExceptionInformationExceptionNullAsync()
    {
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };

        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new InformationException(StatusException.NotFound, new List<string>())
        };
        var exception = new ExceptionAttribute();

        var result = await Record.ExceptionAsync(() => exception.OnExceptionAsync(exceptionContext));
        Assert.Null(result);
    }

    [Fact(DisplayName = "Exception null")]
    public async Task OnExceptionNullAsync()
    {
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };

        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = null
        };
        var exception = new ExceptionAttribute();

        var result = await Record.ExceptionAsync(() => exception.OnExceptionAsync(exceptionContext));
        Assert.Null(result);
    }
}
