using System.Reflection;
using Ewone.Api.Extensions;
using Ewone.Data.Core;
using Ewone.Domain.Requests.User;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EwoneDbContext>();


builder.Services.AddControllers();
builder.Services.AddMediatR(Assembly.GetAssembly(typeof(LoginRequestHandler)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Resolve();

var app = builder.Build();

await app.RunMigration();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();