﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider = null;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IServiceCollection serverCollection = new ServiceCollection();
            ConfigureServices(serverCollection);

            if (serviceProvider == null)
                throw new NullReferenceException("serviceProvider is null");

            MainWindow window = new MainWindow();
            window.DataContext = serviceProvider.GetService<MainViewModel>();
            window.ShowDialog();
        }

        private void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContextPool<TestDataContext>(options => options.UseCosmos(
                "http://localhost:8081",
                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                "testdb"
                ));
            serviceCollection.AddSingleton<ITitle, ApplicationTitle>();
            serviceCollection.AddTransient<MainViewModel>();
            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
