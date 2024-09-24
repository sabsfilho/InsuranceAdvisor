using System.Text.Json;
using BankRisk;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/", () => "Hello Bank User!");

app.MapPost("/calc-insurance", (Input? input) => Calculate(input));

app.Run();

static IResult Calculate(Input? input)
{
  if (input == null) 
    return Results.BadRequest("undefined input");

    var state = RiskControl.Calculate(input!);

    return Results.Ok(state);
}