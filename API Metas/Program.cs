using API_Metas;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de DbContext
builder.Services.AddDbContext<AplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

// Configuración de CORS
builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
    builder => builder.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod()));

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Asegura que los archivos estáticos se sirvan correctamente
app.UseRouting(); // Asegura que las rutas se configuren correctamente

// Habilitar CORS
app.UseCors("AllowWebApp");

app.UseAuthorization();
app.MapControllers();

app.Run();
