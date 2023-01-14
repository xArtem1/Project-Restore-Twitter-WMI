using BikolTwitter.Exceptions;

namespace BikolTwitter.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
		try
		{
			await next.Invoke(context);
		}
		catch (NotFoundException)
		{
			context.Response.StatusCode = 404;
		}
    }
}
