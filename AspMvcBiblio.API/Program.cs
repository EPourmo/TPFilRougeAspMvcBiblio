using AspMvcBiblio.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
        .AddJsonOptions(options=>options.JsonSerializerOptions.ReferenceHandler= ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<BiblioContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("localConnection")));

builder.Services.AddCors(options => options.AddPolicy(name: "AuteurOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:7195").AllowAnyMethod().AllowAnyHeader();
    }));
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateAsyncScope())
    {
        scope.ServiceProvider.GetRequiredService<BiblioContext>()
            .Database.EnsureCreated();
    }
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AuteurOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();