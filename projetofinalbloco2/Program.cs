using FluentValidation;
using Microsoft.EntityFrameworkCore;
using projetofinalbloco2.Data;
using projetofinalbloco2.Model;
using projetofinalbloco2.Service.Implements;
using projetofinalbloco2.Service;
using projetofinalbloco2.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

var connectionString = builder.Configuration.
        GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FarmaciaDbContext>(options =>
        options.UseSqlServer(connectionString)
);

// Validação das Entidades
builder.Services.AddTransient<IValidator<Produto>, ProdutoValidator>();


// Registrar as Classes e Interfaces Service
builder.Services.AddScoped<IProdutoService, ProdutoService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FarmaciaDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();