namespace Api.Middleware;

public class ValidationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if(!context.Request.Method.Equals("POST")
            && context.Request.Path.Value!.ToLower().Contains("/register"))
        {
            await context.Response.WriteAsJsonAsync(new
            {
                Title = $"Не верный тип метода {context.Request.Method}",
                Status = StatusCodes.Status400BadRequest,
                Instance = context.Request.Path
            });
        }

        await next(context);
    }
}
