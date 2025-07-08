using System.Diagnostics;

namespace Funko_shop.Logs;

public class RequestLoggin
{
  private readonly RequestDelegate? _next;
  private readonly ILogger<RequestLoggin> _logger;
  public RequestLoggin(RequestDelegate next, ILogger<RequestLoggin> logger)
  {
    _next = next;
    _logger = logger;
  }

  public async Task Invoke(HttpContext context)
  {
    var stopWatch = Stopwatch.StartNew();
    if (_next != null && _logger != null)
    {
      await _next(context);
      stopWatch.Stop();
      var request = context.Request;
      var response = context.Response;
      _logger.LogInformation("{Method} {Path} code {StatusCode} in {ElapsedMilliseconds}ms ip addres{IP}",
        request.Method,
        request.Path,
        response.StatusCode,
        stopWatch.ElapsedMilliseconds,
        context.Connection.RemoteIpAddress
      );
    }
  }
}