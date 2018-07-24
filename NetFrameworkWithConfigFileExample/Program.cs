using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Model;
using Microsoft.Extensions.Configuration;
using Autofac.Configuration;

namespace NetFrameworkWithConfigFileExample // Configuracion con xml
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            RegisterComponents();

            AddNote("He is here", "Luke! Luke! Luke! Hey! Hey! I knew you'd come back! I just knew it! ");
            AddNote("I don't like fights", "Fighters coming in. There's too many of them! Accelerate to attack speed! Draw their fire away from the cruisers. Copy, Gold Leader.");

            DisplayNotes();
            ExitForm();
        }

        private static void RegisterComponents()  // configuracion con xml
        {
            var config = new ConfigurationBuilder();  // del net framework, para coger archivos xml o json y generar configuracion
            config.AddXmlFile("autofac.xml"); // añadir fichero xml. Sin ruta es que se copie en el compilado

            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();
            builder.RegisterModule(module); // registra modulo y lo pasa al onfigmodule
            
            Container = builder.Build();
        }

        public static void AddNote(string title, string content)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                scope.Resolve<Journal>().AddJournalPage(title, content);
            }
        }

        public static void DisplayNotes()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                scope.Resolve<Journal>().PrintAllJournalPages();
            }
        }

        public static void ExitForm()
        {
            Console.Write("\nPress any key to exit");
            Console.ReadKey();
        }
    }
}
