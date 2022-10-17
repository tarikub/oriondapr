using Libraries;
using Libraries.Interfaces;
using Libraries.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDaprClient();

builder.Services.AddSingleton(typeof(IStateManager<MediaModel>), typeof(StateManager<MediaModel>));
builder.Services.AddSingleton(typeof(IStateManager<User>), typeof(StateManager<User>));

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCloudEvents();
app.UseAuthorization();
app.MapSubscribeHandler();
app.MapControllers();

app.Run();
