// Inline middleware to block GET requests to /api/* endpoints except for allowed URLs
app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Get &&
        context.Request.Path.StartsWithSegments("/api") &&
        !allowedGetUrls.Contains(context.Request.Path.Value, StringComparer.OrdinalIgnoreCase))
    {
        context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
        await context.Response.WriteAsync("Access denied");
        return;
    }

    await next.Invoke();
});
