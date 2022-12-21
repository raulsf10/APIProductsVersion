using APIProductsVersion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// 1.- Agregar servicio de HttpClient para enviar peticiones en Controladores
builder.Services.AddHttpClient();

// 2.- Agregar el control de versiones
builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
});

// 3.- Agregar configuración para documentar las versiones
builder.Services.AddVersionedApiExplorer( setup =>
{
    setup.GroupNameFormat = "'v'VVV"; // Para que se mire v1.0.0, v.1.0.1, etc.
    setup.SubstituteApiVersionInUrl = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4.- Configurar opciones
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

// 5.- Configurar Endpoints para la documentación de Swagger para cada una de las versiones de la API
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // 6.- Configurar documentación de Swagger
    app.UseSwaggerUI(options =>
    {
        foreach(var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant()
                ); // Esto es para que el Endpoint sea: /swagger/v1,v2,etc/swagger.json
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
