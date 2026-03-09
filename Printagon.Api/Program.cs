using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Printagon.Api.Data;
using Printagon.Api.Data.Seed;
using Printagon.Api.Models;
using Printagon.Api.Repositories;
using Printagon.Api.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Repositories
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo

    {
        Title = "Printagon API",
        Version = "v1",
        Description = "Internal API for managing orders, rolls and production data."
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("PrintagonDb"));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbSeeder.Seed(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}


app.MapControllers();

app.UseHttpsRedirection();

app.MapGet("/order/{orderId}", async (Guid orderId, IOrderRepository repo) =>
{
    var order = await repo.GetOrderByIdAsync(orderId);
    if (order == null) return Results.NotFound();

    // Undvik cykliska referenser
    var options = new System.Text.Json.JsonSerializerOptions
    {
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    };

    return Results.Json(order, options);
});

app.MapGet("/order", async (IOrderRepository repo) =>
{
    var orders = await repo.GetOrdersAsync();

    Console.WriteLine($"Retrieved {orders.Count()} orders from the repository.");

    var options = new System.Text.Json.JsonSerializerOptions
    {
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    };

    return Results.Json(orders, options);
});

app.MapPost("/order", async (Order order, IOrderRepository repo) =>
{
    var createdOrder = await repo.CreateOrderAsync(order);

    var options = new System.Text.Json.JsonSerializerOptions
    {
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    };
    return Results.Created($"/order/{createdOrder.Id}", createdOrder);
});

app.MapPut("/order/{orderId}", async (Guid orderId, Order order, IOrderRepository repo) => {
    var updatedOrder = await repo.UpdateOrderAsync(orderId, order);

    var options = new System.Text.Json.JsonSerializerOptions
    {
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    };

    return Results.Json(updatedOrder, options);
});

app.MapDelete("/order/{orderId}", async (Guid orderId, IOrderRepository repo) =>
{
    var success = await repo.DeleteOrderAsync(orderId);

    if (!success) return Results.NotFound();

    return Results.NoContent();
});

app.Run();

