using System;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Autofac;
using Microsoft.Extensions.Configuration;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;

namespace NetCoreWithAutofacAndConfigFileExample
{
    class Program
    {
        private static IServiceProvider ServiceContainer { get; set; }  //donde se contiene

        static void Main(string[] args)
        {
            RegisterComponents();

            AddNote("He is here", "Luke! Luke! Luke! Hey! Hey! I knew you'd come back! I just knew it! ");
            AddNote("I don't like fights", "Fighters coming in. There's too many of them! Accelerate to attack speed! Draw their fire away from the cruisers. Copy, Gold Leader.");

            DisplayNotes();
            ExitForm();
        }

        private static void RegisterComponents() 
        {
            var serviceCollection = new ServiceCollection(); // trabaja con las dependencias
            var containerBuilder = new ContainerBuilder(); // hay que populate, se informa que en vez de trabajar con framework autofac trabaje con framework de microsoft
            containerBuilder.Populate(serviceCollection); // se pasa por el constructor

            var config = new ConfigurationBuilder();
            config.AddXmlFile("autofac.xml");

            var module = new ConfigurationModule(config.Build()); // genera modulo
            containerBuilder.RegisterModule(module); // registra módulo

            var container = containerBuilder.Build();


            ServiceContainer = new AutofacServiceProvider(container); // al service container se le pasa un new.... y se la psa al serviceCOntainer
        }

        public static void AddNote(string title, string content)
        {
            ServiceContainer.GetRequiredService<Journal>().AddJournalPage(title, content); // se llama con GetRequiredService y se usa
        }

        public static void DisplayNotes()
        {
            ServiceContainer.GetRequiredService<Journal>().PrintAllJournalPages();
        }

        public static void ExitForm()
        {
            Console.Write("\nPress any key to exit");
            Console.ReadKey();
        }
    }
}
