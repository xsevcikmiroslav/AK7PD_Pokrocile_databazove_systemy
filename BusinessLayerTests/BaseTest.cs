using BusinessLayer.Managers;
using BusinessLayer.Managers.Interfaces;
using DataLayer.Repositories.Interfaces;
using DataLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using BusinessLayer.BusinessObjects;

namespace BusinessLayerTests
{
    public class BaseTest
    {
        protected const string CONNECTION_STRING = "mongodb://localhost:27017";

        protected ServiceProvider _serviceProvider;

        public BaseTest()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper(mc =>
            {
                mc.AddProfile(new BusinessLayer.Automapper.AutomapperConfiguration());
            });

            services.AddScoped<IUserRepository, UserRepository>(s =>
                ActivatorUtilities.CreateInstance<UserRepository>(s, CONNECTION_STRING));
            services.AddScoped<IBookRepository, BookRepository>(s =>
                ActivatorUtilities.CreateInstance<BookRepository>(s, CONNECTION_STRING));
            services.AddScoped<IBorrowingHistoryRepository, BorrowingHistoryRepository>(s =>
                ActivatorUtilities.CreateInstance<BorrowingHistoryRepository>(s, CONNECTION_STRING));
            services.AddScoped<ICurrentBorrowingRepository, CurrentBorrowingRepository>(s =>
                ActivatorUtilities.CreateInstance<CurrentBorrowingRepository>(s, CONNECTION_STRING));

            services.AddScoped<IAdminManager, AdminManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IBookManager, BookManager>();

            _serviceProvider = services.BuildServiceProvider();

            MappingHelper.SetMapper(_serviceProvider.GetRequiredService<IMapper>());
        }
    }
}