using BusinessLayer.Managers.Interfaces;
using BusinessLayer.Managers;
using DataLayer.Repositories.Interfaces;
using DataLayer.Repositories;
using OnlineLibraryApi.Authentication;
using AutoMapper;
using BusinessLayer.BusinessObjects;
using OnlineLibraryApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(mc =>
{
    mc.AddProfile(new BusinessLayer.Automapper.AutomapperConfiguration());
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();

builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IAdminManager, AdminManager>();
builder.Services.AddScoped<IBookManager, BookManager>();

builder.Services.AddScoped<IInit, Init>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
