using BusinessLayer.Managers.Interfaces;
using BusinessLayer.Managers;
using DataLayer.Repositories.Interfaces;
using DataLayer.Repositories;
using OnlineLibraryApi.Authentication;
using AutoMapper;
using BusinessLayer.BusinessObjects;
using OnlineLibraryApi;
using System.Text.Json.Serialization;
using OnlineLibraryApi.ExceptionHandling;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(mc =>
{
    mc.AddProfile(new BusinessLayer.Automapper.AutomapperConfiguration());
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBorrowingHistoryRepository, BorrowingHistoryRepository>();
builder.Services.AddScoped<ICurrentBorrowingRepository, CurrentBorrowingRepository>();

builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IAdminManager, AdminManager>();
builder.Services.AddScoped<IBookManager, BookManager>();

builder.Services.AddScoped<IInit, Init>();

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<HttpResponseExceptionFilter>();
    })
    .AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.IgnoreNullValues = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

MappingHelper.SetMapper(app.Services.GetRequiredService<IMapper>());

var provider = builder.Services.BuildServiceProvider();
provider.GetService<IInit>().InitEntres();

app.UseMiddleware<BasicAuthMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler("/error");

app.UseAuthorization();

app.MapControllers();

app.Run();
