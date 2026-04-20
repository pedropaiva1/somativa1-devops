using FluentValidation.API.Constants.CorsConstants;
using FluentValidation.API.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependencyInjectionHandler();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(PoliciesConstants.DefaultCorsPolicy);
app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("/health");

app.Run();

public partial class Program{ }
