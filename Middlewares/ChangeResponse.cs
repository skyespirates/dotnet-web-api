using System.Text.Json; // System.Text.Json namespace
namespace project_service.Middlewares
{
    public class ChangeResponse
    {
        private readonly RequestDelegate _next;
        public ChangeResponse(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            // Create a new memory stream to hold the response body
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context);

            memoryStream.Seek(0, SeekOrigin.Begin);
            var bodyText = await new StreamReader(memoryStream).ReadToEndAsync();

            var responseBodyObject = JsonSerializer.Deserialize<object>(bodyText);

            // Modify the object (example: assuming it's a dictionary and adding a custom property)
            if (responseBodyObject is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Object)
            {
                var responseBodyDict = JsonSerializer.Deserialize<Dictionary<string, object>>(bodyText);
                if (responseBodyDict != null)
                {
                    // Add a custom property to the JSON response
                    responseBodyDict["CustomFooter"] = "This is a custom footer added to the response body.";
                }

                // Serialize the modified object back to JSON
                var modifiedBody = JsonSerializer.Serialize(responseBodyDict, new JsonSerializerOptions { WriteIndented = true });

                // Write the modified response back to the original stream
                context.Response.Body = originalBodyStream;
                await context.Response.WriteAsync(modifiedBody);
            }
            else
            {
                // If it's not an object, reset the body as is
                context.Response.Body = originalBodyStream;
                await context.Response.WriteAsync(bodyText);
            }
        }
    }

    public static class RequestChangeResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UseChangeResponseMiddleware(this IApplicationBuilder app) 
        {
            return app.UseMiddleware<ChangeResponse>();
        }

    }
}


