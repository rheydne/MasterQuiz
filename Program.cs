using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QuizApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container. aaa

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("QuizDB"));*/

builder.Services.AddTransient<IQuestoesServices,QuestoesServices>();
builder.Services.AddTransient<IRespostasServices,RespostasServices>();

builder.Services.AddDbContext<AppDbContext> 
(options => options.UseNpgsql(@"Host=ec2-3-231-82-226.compute-1.amazonaws.com;Username=tcnmcljbpzdoow;Password=a4481a0eb91dd6d13891a89f7f5a3482afa3c9b8530f84e7ad91d337f0849423;Database=dfjeva02dtv2vr"));

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
