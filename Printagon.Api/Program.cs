using Printagon.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Printagon.Api.Data;
using Printagon.Api.Data.Seed;
using Printagon.Api.DTOs.Roll;
using Printagon.Api.Repositories;
using Printagon.Api.Repositories.Interfaces;
using Printagon.Api.Services;

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

app.MapGet("/rolls", async (IRollService service) =>
{
    var rolls = await service.GetAllRollsAsync();

    Console.WriteLine($"Retrieved {rolls.Count()} rolls from the service layer.");

    var options = new System.Text.Json.JsonSerializerOptions
    {
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    };

    return Results.Json(rolls, options);
})
    .WithTags("Rolls");


app.MapGet("/rolls/{rollId}", async (Guid rollId, IRollService service) =>
{
    var roll = await service.GetRollByIdAsync(rollId);

    if (roll == null)
    {
        return Results.NotFound();
    }

    var options = new System.Text.Json.JsonSerializerOptions
    {
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    };

    return Results.Json(roll, options);
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
});

app.Run();

