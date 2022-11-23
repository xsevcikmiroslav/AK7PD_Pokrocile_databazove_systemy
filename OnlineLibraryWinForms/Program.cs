using BusinessLayer.Managers.Interfaces;
using BusinessLayer.Managers;
using DataLayer.Repositories.Interfaces;
using DataLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.BusinessObjects;

namespace OnlineLibraryWinForms
{
    internal static class Program
    {
        public static User ActiveUser { get; set; } = new User();

        public static ServiceProvider MyServiceProvider;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            var services = new ServiceCollection();

            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                MyServiceProvider = serviceProvider;
                var mainWindow = serviceProvider.GetRequiredService<LoginForm>();

                SeedDb();

                Application.Run(mainWindow);
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBorrowingRepository, BorrowingRepository>();
            
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IBookManager, BookManager>();

            services.AddAutoMapper(mc =>
            {
                mc.AddProfile(new BusinessLayer.Automapper.AutomapperConfiguration());
            });

            services.AddScoped<LoginForm>();
            services.AddScoped<MainForm>();
        }

        private static void SeedDb()
        {
            var userManager = MyServiceProvider.GetRequiredService<IUserManager>();
            var bookManager = MyServiceProvider.GetRequiredService<IBookManager>();

            if (!userManager.Find(FindType.OR, "MirSev", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty).Any())
            {
                userManager.CreateUser(new User
                {
                    Firstname = "Miroslav",
                    Surname = "Sevcik",
                    Username = "MirSev",
                    Password = "123",
                    Address = new Address
                    {
                        Zip = "67801",
                        City = "Blansko",
                        Street = "Krizkovskeho",
                        DescriptiveNumber = "1124",
                        OrientationNumber = "29"
                    },
                    Pin = "0101010008"
                });
            }

            if (!bookManager.Find(FindType.OR, "Test Book Title", string.Empty, 0, string.Empty).Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    bookManager.CreateBook(new Book
                    {
                        Author = $"Test Author{i}",
                        Title = $"Test Book Title {i}",
                        NumberOfLicences = new Random().Next(1, 5),
                        YearOfPublication = new Random().Next(2000, 2022),
                        NumberOfPages = new Random().Next(100, 500)
                    });
                }
            }
        }
    }
}