using Printagon.Api.Services.Interfaces;
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


app.MapGet("/order-rolls/{orderRollId}", async (IOrderRollRepository repo, Guid orderRollId) => {

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



app.MapGet("/orders/{orderId}/order-rolls", async (IOrderRollRepository repo, Guid orderId) =>
{
    var result = await repo.GetAllByOrderIdAsync(orderId);

    return Results.Ok(result);
})
.WithTags("OrderRolls");


app.MapPost("/order-rolls/{orderId}/rolls", async (Guid orderId, OrderRoll orderRoll, IOrderRollRepository repo) =>
{
    orderRoll.OrderId = orderId;

    var createdRoll = await repo.AddAsync(orderRoll);

    return Results.Created($"/order-rolls/{orderId}/{createdRoll.RollId}", createdRoll);
})
.WithTags("OrderRolls");


app.MapPatch("/order-rolls/{orderRollId}", async (Guid orderRollId, OrderRoll updatedOrderRoll, IOrderRollRepository repo) =>
{
    var existingOrderRoll = await repo.GetByIdAsync(orderRollId);

    if (existingOrderRoll == null)
    {
        return Results.NotFound();
    }

    existingOrderRoll.IntakeWeight = updatedOrderRoll.IntakeWeight;
    existingOrderRoll.OutputWeight = updatedOrderRoll.OutputWeight;
    existingOrderRoll.WebBreakCount = updatedOrderRoll.WebBreakCount;
    existingOrderRoll.IsRestRoll = updatedOrderRoll.IsRestRoll;
    existingOrderRoll.PaperType = updatedOrderRoll.PaperType;
    existingOrderRoll.PaperGramWeight = updatedOrderRoll.PaperGramWeight;
    existingOrderRoll.PaperWidth = updatedOrderRoll.PaperWidth;
    existingOrderRoll.DeviationReason = updatedOrderRoll.DeviationReason;

    repo.Update(existingOrderRoll);

    //await repo.SaveChangesAsync();

    return Results.Ok(existingOrderRoll);
})
.WithTags("OrderRolls");


app.Run();

