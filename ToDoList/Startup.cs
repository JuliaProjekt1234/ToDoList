﻿using Microsoft.EntityFrameworkCore;
using ToDoList.Db;
using ToDoList.Repositories;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Mappers;

namespace ToDoList;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();

        services.AddDbContext<ToDoListDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IToDoListDbContext, ToDoListDbContext>();
        services.AddScoped<ITablesRepository, TablesRepository>();
        services.AddSingleton(mapper);

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("AllowSpecificOrigin");
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }
}