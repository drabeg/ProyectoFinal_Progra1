var builder = WebApplication.CreateBuilder(args);

// Registra MVC con vistas Razor
builder.Services.AddControllersWithViews();

// HttpClient para consumir la API REST
builder.Services.AddHttpClient("HotelAPI", client =>
{
    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5000/api";
    
    // Aseguramos que si la URL de Railway ya trae "/api", se lo removemos para dejar la base limpia
    if (baseUrl.EndsWith("/api"))
    {
        baseUrl = baseUrl.Substring(0, baseUrl.Length - 4);
    }
    
    // Ahora la URL base siempre terminará limpiamente en .app/ o localhost:5000/
    client.BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/");
});

// Sesión para guardar el usuario logueado
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
