using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Printagon.Api.Data;
using Printagon.Api.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();
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



app.Run();

