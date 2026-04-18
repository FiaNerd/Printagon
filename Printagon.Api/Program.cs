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

app.MapGet("/order-rolls/{orderId}", async (IOrderRollRepository repo, Guid orderId) =>
{

    var orderRoll = await repo.GetRollsForOrderAsync(orderId);

    //Console.WriteLine($"Retrieved {rolls.Count()} rolls from the service layer.")

    //var options = new System.Text.Json.JsonSerializerOptions
    //{
    //    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    //};

    return Results.Ok(orderRoll.Select(or => new
    {
        or.Id,
        or.OrderId,
        or.RollId,
        or.IntakeWeight,
        or.OutputWeight,
        or.ConsumedWeight,
        or.MatchesOrderPaper,
        or.DeviationReason,
        or.IsRestRoll,
        or.WebBreak,
        or.CreatedAt,
        Order = new
        {
            or.Order.Id,
            or.Order.OrderNumber,
            or.Order.JobName
        },
        Roll = new
        {
            or.Roll.Id,
            or.Roll.RollNumber,
            or.Roll.PaperType,
            or.Roll.PaperGramWeight,
            or.Roll.PaperWidth,
            or.Roll.RollWeightLeftOver
        }
    }));
})
    .WithTags("OrderRolls");


app.MapGet("/order-rolls/{orderId}/{rollId}", async (Guid orderId,Guid rollId, IOrderRollRepository repo) =>
{
    var roll = await repo.GetByOrderAndRollAsync(orderId, rollId);

    if (roll == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new
    {
        roll.Id,
        roll.OrderId,
        roll.RollId,
        roll.IntakeWeight,
        roll.OutputWeight,
        roll.ConsumedWeight,
        roll.MatchesOrderPaper,
        roll.DeviationReason,
        roll.IsRestRoll,
        roll.WebBreak,
        roll.CreatedAt,
        Order = new
        {
            roll.Order.Id,
            roll.Order.OrderNumber,
            roll.Order.JobName
        },
        Roll = new
        {
            roll.Roll.Id,
            roll.Roll.RollNumber,
            roll.Roll.PaperType,
            roll.Roll.PaperGramWeight,
            roll.Roll.PaperWidth,
            roll.Roll.RollWeightLeftOver
        }
    });

    //var options = new System.Text.Json.JsonSerializerOptions
    //{
    //    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    //};

    //return Results.Json(roll, options);
})
    .WithTags("Rolls");



app.MapPost("/orders/{orderId}/rolls", async (Guid orderId, RollCreateDto dto, IRollService service) =>
{
    var createdRoll = await service.CreateRollAsync(orderId, dto);

    return Results.Created($"/orders/{orderId}/rolls/{createdRoll.Id}", createdRoll);
})
.WithTags("Rolls");


app.MapPut("/orders/{orderId}/rolls/{rollId}", async (Guid orderId, Guid rollId, RollUpdateDto roll, IRollService service) =>
{
    var updateRoll = await service.UpdateRollAsync(orderId, rollId, roll);

    if (updateRoll == null)
    {
        return Results.NotFound();
    }

    var options = new System.Text.Json.JsonSerializerOptions
    {
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    };

    Console.WriteLine($"Updated roll with ID: {updateRoll.Id}");

    return Results.Json(updateRoll, options);
})
    .WithTags("Rolls");



app.MapDelete("/rolls/{rollId}", async (string rollId, IRollService service) =>
{
    if (!Guid.TryParse(rollId, out var parsedId))
    {
        return Results.BadRequest("Invalid GUID format.");
    }

    try
    {
        await service.DeleteRollAsync(parsedId);

        return Results.NoContent();
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
})
    .WithTags("Rolls");

app.Run();

