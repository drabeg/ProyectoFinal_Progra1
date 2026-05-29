using Microsoft.AspNetCore.Mvc;
using HotelMVC.Models;
using System.Text;
using System.Text.Json;

namespace HotelMVC.Controllers;

public class UsuarioController : Controller
{
    private readonly IHttpClientFactory _http;
    private readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

    public UsuarioController(IHttpClientFactory http) => _http = http;

    private IActionResult RedirigirSiNoHaySesion()
    {
        if (HttpContext.Session.GetString("Username") == null)
            return RedirectToAction("Login", "Home");
        return null!;
    }

    private async Task CargarEmpleadosAsync()
    {
        var client = _http.CreateClient("HotelAPI");
        // CORREGIDO: Se quita "api/" porque la URL base ya lo incluye
        var res = await client.GetAsync("empleado"); 
        var empleados = new List<Empleado>();
        if (res.IsSuccessStatusCode)
        {
            var body = await res.Content.ReadAsStringAsync();
            empleados = JsonSerializer.Deserialize<List<Empleado>>(body, _json) ?? new();
        }
        ViewBag.Empleados = empleados;
        ViewBag.Roles = new List<string> { "Administrador", "Supervisor", "Recepcionista" };
    }

    public async Task<IActionResult> Index()
    {
        var redir = RedirigirSiNoHaySesion(); if (redir != null) return redir;

        var client = _http.CreateClient("HotelAPI");
        var lista = new List<Usuario>();
        
        try
        {
            // CORREGIDO: Cambiado de "api/usuario" a "usuario" para evitar duplicar rutas
            var res = await client.GetAsync("usuario");
            
            if (res.IsSuccessStatusCode)
            {
                var body = await res.Content.ReadAsStringAsync();
                lista = JsonSerializer.Deserialize<List<Usuario>>(body, _json) ?? new();
                
                ViewBag.MensajeDiagnostico = $"🟢 API Conectada con éxito. Registros detectados: {lista.Count}";
            }
            else
            {
                ViewBag.MensajeDiagnostico = $"🔴 Error de la API: Código {(int)res.StatusCode} ({res.StatusCode}). Buscando en: {client.BaseAddress}usuario";
            }
        }
        catch (Exception ex)
        {
            ViewBag.MensajeDiagnostico = $"💥 Error crítico en MVC: {ex.Message}";
        }

        // Reactivamos la carga de empleados de forma segura
        try
        {
            await CargarEmpleadosAsync();
        }
        catch
        {
            ViewBag.MensajeDiagnostico += " | ⚠️ Nota: No se pudieron mapear los empleados.";
        }

        return View(lista);
    }

    public async Task<IActionResult> Crear()
    {
        var redir = RedirigirSiNoHaySesion(); if (redir != null) return redir;
        await CargarEmpleadosAsync(); 
        return View(new Usuario());
    }

    [HttpPost]
    public async Task<IActionResult> Crear(Usuario usuario)
    {
        if (!ModelState.IsValid) { await CargarEmpleadosAsync(); return View(usuario); }

        var client = _http.CreateClient("HotelAPI");
        var json = JsonSerializer.Serialize(usuario);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        // CORREGIDO: Cambiado a "usuario" a secas
        await client.PostAsync("usuario", content); 
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Editar(int id)
    {
        var redir = RedirigirSiNoHaySesion(); if (redir != null) return redir;

        var client = _http.CreateClient("HotelAPI");
        // CORREGIDO: Cambiado a "usuario" a secas
        var res = await client.GetAsync("usuario"); 
        var lista = new List<Usuario>();
        if (res.IsSuccessStatusCode)
        {
            var body = await res.Content.ReadAsStringAsync();
            lista = JsonSerializer.Deserialize<List<Usuario>>(body, _json) ?? new();
        }
        var usuario = lista.FirstOrDefault(u => u.IdUsuario == id);
        if (usuario == null) return NotFound();

        await CargarEmpleadosAsync(); 
        return View(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(int id, Usuario usuario)
    {
        if (!ModelState.IsValid) { await CargarEmpleadosAsync(); return View(usuario); }

        var client = _http.CreateClient("HotelAPI");
        var json = JsonSerializer.Serialize(usuario);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        // CORREGIDO: Cambiado a "usuario/{id}" sin el "api/"
        await client.PutAsync($"usuario/{id}", content); 
        return RedirectToAction("Index");
    }
}

