using Microsoft.Extensions.DependencyInjection;
using Practise_Tasks.Interfaces;
using Practise_Tasks.Services;
using Practise_Tasks.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<RandomNumberSettings>(builder.Configuration.GetSection("RandomNumberSettings"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IInputValidate, ReverseInputValidate>();
builder.Services.AddScoped<IItemsCounter<char, string>, CharsCounter>();
builder.Services.AddScoped<ISubsetFinder<string>, VowelSpan>();
builder.Services.AddScoped<IRandomNumber, RandomNumber>();
builder.Services.AddHttpClient("GetRandomNumber", client =>
{
    client.BaseAddress = new Uri("https://www.randomnumberapi.com/api/v1.0/random");
});


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
