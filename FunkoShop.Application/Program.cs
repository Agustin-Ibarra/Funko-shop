using System.Threading.RateLimiting;
using DotNetEnv;
using FunkoShop.Aplication.Data;
using FunkoShop.Aplication.Logs;
using FunkoShop.Aplication.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

Env.Load();

var builder = WebApplication.CreateBuilder(args);
var connection = Environment.GetEnvironmentVariable("DB_CONNECTION");


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddOutputCache();
builder.Services.AddSwaggerGen();
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.None);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
  options.LoginPath = "/login";
  options.ExpireTimeSpan = TimeSpan.FromDays(30);
  options.SlidingExpiration = true;
  options.Cookie.HttpOnly = true;
  options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
  options.Events = new CookieAuthenticationEvents
  {
    OnRedirectToLogin = context =>
    {
      if (context.Request.Path.StartsWithSegments("/cart/items") || context.Request.Path.StartsWithSegments("/purchase") && context.Response.StatusCode == 200)
      {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
      }
      else
      {
        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
      }
    }
  };
});
if (connection != null)
{
  builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connection));
  builder.Services.AddScoped<IUserRepository, UserRepository>();
  builder.Services.AddScoped<IItemRepository, ItemRepository>();
  builder.Services.AddScoped<ICartRepository, CartRepository>();
  builder.Services.AddScoped<PurchaseRepository>();
}
else
{
  Console.WriteLine("string connection is null");
}
builder.Services.AddRateLimiter(options => // middlewarer limitar numero de peticiones
{
  options.AddPolicy("fixedWindows", context => RateLimitPartition.GetFixedWindowLimiter(
    partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unkown IP addres", // utilizar la ip como identificador
    factory: key => new FixedWindowRateLimiterOptions
    {
      PermitLimit = 60, // limite de 60 solicitudes por minuto
      Window = TimeSpan.FromSeconds(60),
      QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
      QueueLimit = 0
    }
  ));
  options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseOutputCache();
app.UseMiddleware<RequestLoggin>();
app.UseRateLimiter();
app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler(error =>
{
  error.Run(async context =>
  {
    context.Response.StatusCode = 500;
    var errorResponse = new { message = "Ocurrio un error al procesar la peticion" };
    await context.Response.WriteAsJsonAsync(errorResponse);
  });
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
