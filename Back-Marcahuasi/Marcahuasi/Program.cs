using Marcahuasi.Modelos;
using Marcahuasi.Negocio;
using Marcahuasi.Negocio.IServices;
using Marcahuasi.Repositorio;
using Marcahuasi.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container.
builder.Services.AddDbContext<MarcahuasiContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));
});

builder.Services.AddScoped<IDashBoardIngresosServices, DashBoardIngresosServices>();
builder.Services.AddScoped<IAdministradorRepositorio, AdministradorRepositorio>();
builder.Services.AddScoped<IIngresoRepositorio, IngresoRepositorio>();
builder.Services.AddScoped<INacionalidadRepositorio, NacionalidadRepositorio>();
builder.Services.AddScoped<ITarifaPagoRepositorio, TarifaPagoRepositorio>();
builder.Services.AddAutoMapper(typeof(Marcahuasi.Utilidades.AutoMapper));


//Desactivacion de CORDS
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
