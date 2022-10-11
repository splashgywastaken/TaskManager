using Microsoft.AspNetCore.Mvc;

namespace TaskManager;

public class HtmlResult : IActionResult
{
    private readonly string _htmlCode;
    public HtmlResult(string html) => _htmlCode = html;
    public async Task ExecuteResultAsync(ActionContext context)
    {
        string fullHtmlCode = @$"<!DOCTYPE html>
            <html>
                <head>
                    <title>METANIT.COM</title>
                    <meta charset=utf-8 />
                </head>
                <body>{_htmlCode}</body>
            </html>";
        await context.HttpContext.Response.WriteAsync(fullHtmlCode);
    }
}