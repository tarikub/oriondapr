using Libraries.Interfaces;
using Libraries.Models;
using Libraries;
using Help.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();
builder.Services.AddDaprClient();

builder.Services.AddSingleton(typeof(IStateManager<MediaModel>), typeof(StateManager<MediaModel>));
builder.Services.AddSingleton(typeof(IStateManager<Libraries.Models.User>), typeof(StateManager<Libraries.Models.User>));
builder.Services.AddSingleton(typeof(IStateManager<Libraries.Models.SlideShow>), typeof(StateManager<Libraries.Models.SlideShow>));
builder.Services.AddSingleton(typeof(IHelpService), typeof(HelpService));

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
