using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class MainViewModel : Base
    {
        private readonly TestDataContext dataContext;

        private readonly IServiceProvider serviceProvider;

        public MainViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.dataContext = serviceProvider.GetService<TestDataContext>();
            DoThing();
            ITitle mainTitle = serviceProvider.GetService<ITitle>();
            Title = mainTitle.Title;
        }

        private async void DoThing()
        {
            await dataContext.Database.EnsureCreatedAsync();
            dataContext.data.Add(new Model.testModel { Id =1,Name="hej"});
            await dataContext.SaveChangesAsync();
        }

        private string title = "Dependency Injection Application";

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChange(nameof(Title));
            }
        }
    }
}
