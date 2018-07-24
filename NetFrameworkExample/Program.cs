using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Model;

namespace NetFrameworkExample
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            RegisterComponents();

            AddNote("He is here", "Luke! Luke! Luke! Hey! Hey! I knew you'd come back! I just knew it! ");  // añade una nota al journal
            AddNote("I don't like fights", "Fighters coming in. There's too many of them! Accelerate to attack speed! Draw their fire away from the cruisers. Copy, Gold Leader.");

            DisplayNotes();  // VA A NECESITAR DEL JOURNAL, SE HACE UN SCOPE.
            ExitForm();
        }

        private static void RegisterComponents() // registrar es decirle al contenedor todas las clases puede inyectar en otras ¿?¿?¿? 
        {
            var builder = new ContainerBuilder();  // es una clase para generar el IContainer. Dice cómo se va a instanciar. Como un placeholder. Se instancia el contenedor

            builder.RegisterType<Journal>().SingleInstance(); // le decimos que queremos registrar la clase journal para que cualquiera lo pueda coger cuando lo necesite. SingleInstancia = cuando alguien pida el single journal que siempre devuelva el mismo (SINGLETON, que ya lo hace autofac). SingleInstancia = ámbito del objeto, que podría decir tb por request. No abusar, a veces no se necesita.
            builder.RegisterType<JournalPagePrinter>().As<IJournalPagePrinter>();  // lo registras para que cada que pidan journal que generen un nuevo printer. (Ccuando pidas por esta interfaz, aplicame esta clase (JournalPagePrinter). Si no pone interfaz, se instancia por request). Aqui el Journal se cas acon PagePrinter.

            Container = builder.Build(); // Al builder se le dice que construya uno, que lo guardará en el container. Crea la CONFIGURACION en memoria. (SE INSTANCIA SOLO CUANDO SE LLAMA, aqui no se instancia nada)
        }

        public static void AddNote(string title, string content)
        {
            using (var scope = Container.BeginLifetimeScope())  // lo que hagas aqui se genera aqui y después se hace dispose (para no tener problemas de memoria). No se hace new, puedes cambiar en el container.
            {
                //scope.Resolve<Journal>().AddJournalPage(title, content); //más corto
                var journal = scope.Resolve<Journal>();
                    journal.AddJournalPage(title, content); // que resuelva la dependencia. Decimos que queremos un Journal que es el mismo (si es la 1a vez lo genera, sino reutiliza). El resolve lo que hace es instanciar, ha mirado el container (configuración).
            }
        }

        public static void DisplayNotes()
        {
            using (var scope = Container.BeginLifetimeScope())  
            {
                scope.Resolve<Journal>().PrintAllJournalPages(); // Gracias al single instancia agrega notas. Llama al print.
            }
        }

        public static void ExitForm()
        {
            Console.Write("\nPress any key to exit");
            Console.ReadKey();
        }
    }
}
