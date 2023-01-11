using System.IO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<string> wishes = new() {"Happy New Year", "Hello!"};


app.MapGet("/", (context) => 
{
    context.Response.ContentType = "text/html; charset=utf-8";

    string body = "<h1>Dog</h1><div class='imgs'><img src='/html/dog.jpg' height='150'/><img src='/html/dogl.jpg' height='150'/></div><form method='POST'><label for='wish'>Пожелание <input type='text' name='wish'/><input type='submit'/></label></form>";

    if (context.Request.Method == "POST")
    {
        var form = context.Request.Form;
        wishes.Add(form["wish"]);
        context.Response.Redirect("/", false);
    }

    body += "<div class='imgs'>";
    foreach(var wish in wishes)
    {
        body += $"<div>{wish}</div>";
    }
    body += "</div>";
    return context.Response.WriteAsync(body);

});


app.MapGet(
    "/html/{img:regex(\\w+.\\w+)}",
    (HttpContext context, string img) => 
    {
        Console.WriteLine(img);
        return context.Response
        .SendFileAsync(Path.GetFullPath($"html/{img}"));
    }
    );


// app.Run(async (context) => await
// context.Response.SendFileAsync("dog.jpg"));

// app.Run(async (context) => 
// {
//     var path = context.Request.Path.ToString();
//     var fullPath = $"html{path}";
//     var response = context.Response;
//     Console.WriteLine(fullPath);

//     if (File.Exists(fullPath))
//     {
//         await response.SendFileAsync(fullPath);
//     }
//     else
//     {
//         response.StatusCode = 404;
//         await response.WriteAsync("<h2>Not Found</h2>");
//     }
// });

// app.Run(async (context) => 
// {
//     var path = context.Request.Path.ToString();
//     var now = DateTime.Now;
//     var response = context.Response;

//     if (path == "/date")
//     {
//         await response.WriteAsync($"Date: {now.ToShortDateString()}");
//     }
//     else if (path == "/time")
//     {
//         await response.WriteAsync($"Time: {now.ToShortTimeString()}");
//     }
//     else
//     {
//         await response.WriteAsync("Hello World!");
//     }
// });

// app.Run((context) => 
// {
//     context.Response.ContentType = "text/html; charset=utf-8";

//     string body = "<h1>Dog</h1><div class='imgs'><img src='/html/dog.jpg' height='150'/><img src='/html/dogl.jpg' height='150'/></div><form method='POST'><label for='wish'>Пожелание <input type='text' name='wish'/><input type='submit'/></label></form>";

//     if (context.Request.Method == "POST")
//     {
//         var form = context.Request.Form;
//         wishes.Add(form["wish"]);
//         context.Response.Redirect("/", false);
//     }

//     body += "<div class='imgs'>";
//     foreach(var wish in wishes)
//     {
//         body += $"<div>{wish}</div>";
//     }
//     body += "</div>";
//     return context.Response.WriteAsync(body);

// });

app.Run();
