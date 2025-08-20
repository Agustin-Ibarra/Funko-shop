using System.Diagnostics;

namespace FunkoShop.Aplication.Logs;

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
      if (response.StatusCode < 400)
      {
        _logger.LogInformation("Method: {Method} Path: {Path} code:{StatusCode} in {ElapsedMilliseconds}ms ip addres{IP}",
        request.Method,
        request.Path,
        response.StatusCode,
        stopWatch.ElapsedMilliseconds,
        context.Connection.RemoteIpAddress
      );
      }
      else if (response.StatusCode >= 400 && response.StatusCode < 500)
      {
        _logger.LogWarning("Method: {Method} Path: {Path} code:{StatusCode} in {ElapsedMilliseconds}ms ip addres{IP}",
        request.Method,
        request.Path,
        response.StatusCode,
        stopWatch.ElapsedMilliseconds,
        context.Connection.RemoteIpAddress
      );
      }
      else
      {
        _logger.LogError("Method: {Method} Path: {Path} code:{StatusCode} in {ElapsedMilliseconds}ms ip addres{IP}",
        request.Method,
        request.Path,
        response.StatusCode,
        stopWatch.ElapsedMilliseconds,
        context.Connection.RemoteIpAddress
        );
      }
    }
  }
}