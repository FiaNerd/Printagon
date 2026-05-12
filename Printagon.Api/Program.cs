using Printagon.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Printagon.Api.Data;
using Printagon.Api.Data.Seed;
using Printagon.Api.DTOs.Roll;
using Printagon.Api.Repositories;
using Printagon.Api.Repositories.Interfaces;
using Printagon.Api.Services;
using Printagon.Api.Models;
using Printagon.Api.DTOs.OrderRoll;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

/* --------------
   Repositories
   -------------- */
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRollRepository, RollRepository>();
builder.Services.AddScoped<IOrderRollRepository, OrderRollRepository>();

/* --------------
   Services
   -------------- */
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRollService, RollService>();
//builder.Services.AddScoped<IOrderRollService, OrderRollService>();

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
    //app.MapOpenApi();
}


app.MapControllers();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}


app.MapGet("/orders/{orderId}/order-rolls", async (IOrderRollRepository repo, Guid orderId) =>
{
    var result = await repo.GetAllByOrderIdAsync(orderId);

    return Results.Ok(result);
})
.WithTags("OrderRolls");


app.Map("/order-rolls/{orderRollId}", async (IOrderRollRepository repo, Guid orderRollId) => { 
    
    var roll = await repo.GetByIdAsync(orderRollId);

    if (roll == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(roll);
})
    .WithTags("OrderRolls");


app.MapGet("/order-rolls/{orderId}/{rollId}", async (IOrderRollRepository repo, Guid orderId, Guid rollId) =>
{
    var roll = await repo.GetByOrderAndRollAsync(orderId, rollId);

    if (roll == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(roll);
})
.WithTags("OrderRolls");




app.MapPost("/order-rolls/{orderId}/rolls", async (Guid orderId, OrderRoll orderRoll, IOrderRollRepository repo) =>
{
    orderRoll.OrderId = orderId;

    var createdRoll = await repo.AddAsync(orderRoll);

    return Results.Created($"/order-rolls/{orderId}/{createdRoll.RollId}", createdRoll);
})
.WithTags("OrderRolls");


//app.MapPatch("/order-rolls/{orderRollId}", async (Guid orderRollId, OrderRollPatch patch, IOrderRollRepository repo) =>
//{
//    var existingOrderRoll = await repo.GetOrderRollByIdAsync(orderRollId);

//    if (existingOrderRoll == null)
//    {
//        return Results.NotFound();
//    }

//    if (patch.OutputWeight.HasValue)
//    {
//        existingOrderRoll.OutputWeight = patch.OutputWeight.Value;
//    }

//    if (patch.MatchesOrderPaper.HasValue)
//    {
//        existingOrderRoll.MatchesOrderPaper = patch.MatchesOrderPaper.Value;
//    }

//    if (patch.DeviationReason != null)
//    {
//        existingOrderRoll.DeviationReason = patch.DeviationReason;
//    }

//    if (patch.IsRestRoll.HasValue)
//    {
//        existingOrderRoll.IsRestRoll = patch.IsRestRoll.Value;
//    }

//    if (patch.WebBreak.HasValue)
//    {
//        existingOrderRoll.WebBreak = patch.WebBreak.Value;
//    }

//    var updateRoll = await repo.UpdateOrderRollAsync(existingOrderRoll);

//    if (updateRoll == null)
//    {
//        return Results.NotFound();
//    }

//    var options = new System.Text.Json.JsonSerializerOptions
//    {
//        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
//    };

//    Console.WriteLine($"Updated roll with ID: {updateRoll.Id}");

//    return Results.Json(updateRoll, options);
//})
//    .WithTags("OrderRolls");



//app.MapDelete("/order-rolls/{orderRollId}", async (Guid orderRollId, IOrderRollRepository repo) =>
//{

//    try
//    {
//        await repo.DeleteOrderRollByIdAsync(orderRollId);

//        return Results.NoContent();
//    }
//    catch (KeyNotFoundException)
//    {
//        return Results.NotFound();
//    }
//})
//    .WithTags("OrderRolls");
//    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
//};

//    //return Results.Json(roll, options);
//})
//    .WithTags("OrderRolls");



app.Run();

