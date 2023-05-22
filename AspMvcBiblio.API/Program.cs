using AspMvcBiblio.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
        .AddJsonOptions(options=>options.JsonSerializerOptions.ReferenceHandler= ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddDefaultPolicy(builder=> { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }));

//http://localhost:7195//


if (builder.Environment.IsDevelopment())
{
	builder.Services.AddDbContext<BiblioContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("localConnection")));
}
else if (builder.Environment.IsStaging())
{
	builder.Services.AddDbContext<BiblioContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("devOpsConnection")));
}
else
{
	builder.Services.AddDbContext<BiblioContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("azureConnection")));

}

var app = builder.Build();
app.UseCors();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }