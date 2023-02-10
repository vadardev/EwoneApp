using System.Reflection;
using Ewone.Data.Core;
using Ewone.Data.Repositories.Repository;
using Ewone.Data.Repositories.UnitToWork;
using Ewone.Data.Repositories.User;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EwoneDbContext>();


builder.Services.AddControllers();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IUnitToWork, UnitToWork>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

app.RunMigration();

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