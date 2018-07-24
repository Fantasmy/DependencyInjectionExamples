using System;
using Microsoft.Extensions.DependencyInjection;
using Model;

namespace NetCoreExample
{
    class Program
    {
        private static IServiceProvider ServiceContainer { get; set; }  // en vez de container tiene un IserviceProvider (Algunos lo llaman iservicecontainer

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
            var serviceCollection = new ServiceCollection(); 

            serviceCollection.AddSingleton<Journal>(); // en vez de registrar un tipo se hace un add. añadir como singleton el journal
            serviceCollection.AddScoped<IJournalPagePrinter, JournalPagePrinter>(); // registramos addScoped, interfaz como JournalPagePrinter

            ServiceContainer = serviceCollection.BuildServiceProvider();
        }

        public static void AddNote(string title, string content)
        {
            ServiceContainer.GetRequiredService<Journal>().AddJournalPage(title, content); // en vez de resolve se llama GetRequiredService
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
