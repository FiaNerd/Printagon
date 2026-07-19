using Microsoft.EntityFrameworkCore;
using Printagon.Api.Services.Interfaces;
using Printagon.Api.Data;
using Printagon.Api.Data.Seed;
using Printagon.Api.Repositories;
using Printagon.Api.Repositories.Interfaces;
using Printagon.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Repositories
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRollRepository, RollRepository>();
builder.Services.AddScoped<IOrderRollRepository, OrderRollRepository>();

// Services
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRollService, RollService>();

// OpenAPI (.NET 10)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("PrintagonDb"));

var app = builder.Build();

// Seed
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbSeeder.Seed(context);
}

// OpenAPI UI
app.MapOpenApi();

// API v1 group
var v1 = app.MapGroup("/api/v1").WithOpenApi();

// ---------------------------
// ORDER ROLLS
// ---------------------------

// GET single OrderRoll
v1.MapGet("/order-rolls/{orderRollId}", async (Guid orderRollId, IOrderRollRepository repo) =>
{
    var roll = await repo.GetByIdAsync(orderRollId);
    return roll is null ? Results.NotFound() : Results.Ok(roll);
});

// GET all OrderRolls for an Order
v1.MapGet("/orders/{orderId}/order-rolls", async (Guid orderId, IOrderRollRepository repo) =>
{
    var rolls = await repo.GetAllByOrderIdAsync(orderId);
    return Results.Ok(rolls);
});

// POST new OrderRoll
v1.MapPost("/orders/{orderId}/order-rolls", async (Guid orderId, HttpContext http, IOrderRollRepository repo) =>
{
    var orderRoll = await http.Request.ReadFromJsonAsync<OrderRoll>();

    if (orderRoll is null)
        return Results.BadRequest("Invalid JSON body");

    orderRoll.OrderId = orderId;

    var createdRoll = await repo.AddAsync(orderRoll);

    await repo.SaveChangesAsync();

    return Results.Created($"/api/v1/order-rolls/{createdRoll.Id}", createdRoll);
});


// PATCH OrderRoll
v1.MapPatch("/order-rolls/{orderRollId}", async (Guid orderRollId, OrderRoll updated, IOrderRollRepository repo) =>
{
    var existing = await repo.GetByIdAsync(orderRollId);
    if (existing is null) return Results.NotFound();

    existing.IntakeWeight = updated.IntakeWeight;
    existing.OutputWeight = updated.OutputWeight;
    existing.WebBreakCount = updated.WebBreakCount;
    existing.IsRestRoll = updated.IsRestRoll;
    existing.PaperType = updated.PaperType;
    existing.PaperGramWeight = updated.PaperGramWeight;
    existing.PaperWidth = updated.PaperWidth;
    existing.DeviationReason = updated.DeviationReason;

    repo.Update(existing);
    await repo.SaveChangesAsync();

    return Results.Ok(existing);
});

// DELETE OrderRoll
v1.MapDelete("/order-rolls/{orderRollId}", async (Guid orderRollId, IOrderRollRepository repo) =>
{
    var existing = await repo.GetByIdAsync(orderRollId);
    if (existing is null) return Results.NotFound();

    repo.Remove(existing);
    await repo.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
