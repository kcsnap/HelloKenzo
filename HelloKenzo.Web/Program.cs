var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", () => Results.Content(
    "<html><head><link rel=\"stylesheet\" href=\"/css/site.css\"></head><body class=\"centered\"><h1>Hello Kenzo!</h1></body></html>",
    "text/html"));

app.Run();
