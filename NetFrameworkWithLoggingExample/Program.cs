using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Autofac;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Events;

namespace NetFrameworkWithLoggingExample
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

        private static void RegisterComponents()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<JournalWithLogging>().SingleInstance();
            builder.RegisterType<JournalPagePrinter>().As<IJournalPagePrinter>(); // registramos tipo pageprinter
            builder.Register<ILogger>((c, p) => {    // registrar Ilogger. 
                return new LoggerConfiguration().MinimumLevel.Debug()  //utiliza lambda, que retorna la configuración del logger
                    .WriteTo.Logger(l => l.Filter   //.Algo para ir añadiendo codad. l de logger
                        .ByIncludingOnly(e => e.Level == LogEventLevel.Information) // filtro
                        .WriteTo.File(new CompactJsonFormatter(), "jounalInfoLog.json")) // guarda en fichero  json
                    .WriteTo.Logger(l => l.Filter
                        .ByIncludingOnly(e => e.Level == LogEventLevel.Error)  // los que no quiero
                        .WriteTo.File(new CompactJsonFormatter(), "jounalErrorLog.json"))
                    .WriteTo.Logger(l => l.Filter
                        .ByIncludingOnly(e => e.Level != LogEventLevel.Information && e.Level != LogEventLevel.Error) // los que no quiero
                        .WriteTo.File(new CompactJsonFormatter(), "journalOtherErrorsLog.json"))
                    .CreateLogger();
            }).SingleInstance(); // decimos que es single instance, que nos devuelva la misma.

            Container = builder.Build();
        }

        public static void AddNote(string title, string content)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                scope.Resolve<JournalWithLogging>().AddJournalPage(title, content);
            }
        }

        public static void DisplayNotes()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                scope.Resolve<JournalWithLogging>().PrintAllJournalPages();
            }
        }

        public static void ExitForm()
        {
            Console.Write("\nPress any key to exit");
            Console.ReadKey();
        }
    }
}
