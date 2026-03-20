using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DevTest_TaskApi.Data;
using DevTest_TaskApi.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DevTest_TaskApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevTest_TaskApiContext") ?? throw new InvalidOperationException("Connection string 'DevTest_TaskApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITasksService, TasksService>();
var app = builder.Build();

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
