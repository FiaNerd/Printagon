using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Printagon.Api.Data;
using Printagon.Api.Data.Seed;
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

app.MapGet("/rolls", async (IRollRepository repo) =>
{
    var rolls = await repo.GetAllRollsAsync();

    Console.WriteLine($"Retrieved {rolls.Count()} rolls from the repository.");

    var options = new System.Text.Json.JsonSerializerOptions
    {
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    };

    return Results.Json(rolls, options);
})
    .WithTags("Rolls");


app.MapGet("/rolls/{rollId}", async (Guid rollId, IRollRepository repo) =>
{
    var roll = await repo.GetRollByIdAsync(rollId);

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


//app.MapPost("/order", async (OrderCreateDto order, IOrderService service) =>
//{
//    var createdOrder = await service.CreateOrderAsync(order);

//    var options = new System.Text.Json.JsonSerializerOptions
//    {
//        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
//    };
//    return Results.Created($"/order/{createdOrder.Id}", createdOrder);
//})
//    .WithTags("Orders");

//app.MapPut("/order/{orderId}", async (Guid orderId, OrderUpdateDto order, IOrderService service) => {
//    var updatedOrder = await service.UpdateOrderAsync(orderId, order);

//    if(updatedOrder == null)
//    {
//        return Results.NotFound();
//    }

//    var options = new System.Text.Json.JsonSerializerOptions
//    {
//        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
//    };

//    return Results.Json(updatedOrder, options);
//})
//    .WithTags("Orders");

//app.MapDelete("/order/{orderId}", async (Guid orderId, IOrderService service) =>
//{
//    var delete = await service.DeleteOrderAsync(orderId);

//    return delete ? Results.NoContent() : Results.NotFound();
//})
//    .WithTags("Orders");

app.Run();

