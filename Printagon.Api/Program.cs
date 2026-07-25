using Microsoft.EntityFrameworkCore;
using Printagon.Api.Services.Interfaces;
using Printagon.Api.Data;
using Printagon.Api.Data.Seed;
using Printagon.Api.Repositories;
using Printagon.Api.Repositories.Interfaces;
using Printagon.Api.Services;
using Printagon.Api.DTOs.OrderRoll;

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
builder.Services.AddScoped<IOrderRollService, OrderRollService>();

// OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// API v1 group
var v1 = app.MapGroup("/api/v1").WithOpenApi();

// ---------------------------
// ORDER ROLLS
// ---------------------------

// GET all OrderRolls for an Order
v1.MapGet("/orders/{orderId}/order-rolls", async (Guid orderId, IOrderRollService service) =>
{
    var rolls = await service.GetAllByOrderIdAsync(orderId);

    return rolls.Any()
        ? Results.Ok(rolls)
        : Results.NotFound($"No rolls found for order {orderId}");
})
    .WithTags("OrderRolls");

// GET single OrderRoll
v1.MapGet("/order-rolls/{orderRollId}", async (Guid orderRollId, IOrderRollService service) =>
{
    var roll = await service.GetByIdAsync(orderRollId);
    return roll is null ? Results.NotFound() : Results.Ok(roll);
})
    .WithTags("OrderRolls");


// GET order and rolls
v1.MapGet("/orders/{orderId}/rolls/{rollId}", async (
    Guid orderId, 
    Guid rollId, 
    IOrderRollService service) =>
{
    var roll = await service.GetByOrderAndRollAsync(orderId, rollId);
    return roll is null ? Results.NotFound() : Results.Ok(roll);
})
    .WithTags("OrderRolls");



// POST new OrderRoll
v1.MapPost("/orders/{orderId}/order-rolls", async (
    Guid orderId,
    OrderRollCreateDto dto,
    IOrderRollService service
) =>
{
    dto.OrderId = orderId;

    var createdRoll = await service.AddAsync(dto);

    return Results.Created(
        $"/api/v1/orders/{createdRoll.Id}/order-rolls",
        createdRoll
    );
})
.WithTags("OrderRolls");


// PATCH OrderRoll
v1.MapPatch("/order-rolls/{orderRollId}", async (Guid orderRollId, OrderRoll updated, IOrderRollRepository repo) =>
{
    var existing = await repo.GetByIdAsync(orderRollId);
   
    if (existing is null) 
    { 
        return Results.NotFound();
    } 

    existing.IntakeWeight = updated.IntakeWeight;
    existing.OutputWeight = updated.OutputWeight;
    existing.WebBreakCount = updated.WebBreakCount;
    existing.IsRestRoll = updated.IsRestRoll;
    existing.PaperType = updated.PaperType;
    existing.PaperGramWeight = updated.PaperGramWeight;
    existing.PaperWidth = updated.PaperWidth;
    existing.DeviationReason = updated.DeviationReason;

    repo.UpdateAsync(existing);

    await repo.SaveChangesAsync();

    return Results.Ok(existing);
})
    .WithTags("OrderRolls");


// DELETE OrderRoll
v1.MapDelete("/order-rolls/{orderRollId}", async (Guid orderRollId, IOrderRollRepository repo) =>
{
    var existing = await repo.GetByIdAsync(orderRollId);
    if (existing is null) return Results.NotFound();

    repo.DeleteAsync(existing);
    await repo.SaveChangesAsync();

    return Results.NoContent();
})
    .WithTags("OrderRolls");

app.Run();
