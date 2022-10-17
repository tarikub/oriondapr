using Libraries.Interfaces;
using Libraries.Models;
using Libraries;

// Add services to the container.

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient();

builder.Services.AddSingleton(typeof(IStateManager<MediaModel>), typeof(StateManager<MediaModel>));
builder.Services.AddSingleton(typeof(IStateManager<Libraries.Models.User>), typeof(StateManager<Libraries.Models.User >));
builder.Services.AddSingleton(typeof(IStateManager<Libraries.Models.SlideShow>), typeof(StateManager<Libraries.Models.SlideShow>));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddDaprClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
