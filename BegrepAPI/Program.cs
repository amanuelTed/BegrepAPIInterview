using BegrepAPI.Contracts;
using BegrepAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddHttpClient<IConceptService, ConceptService>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
});
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

app.UseAuthorization();

app.MapControllers();

app.Run();
