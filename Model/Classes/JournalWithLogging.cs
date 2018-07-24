using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class JournalWithLogging : IJournal  // este journal tiene logger en serlilog. Acepta la interfaz ILogger
    {
        List<JournalPage> _pages; // dependencia

        IJournalPagePrinter _printer; // dependencia

        ILogger _logger; // dependencia

        public JournalWithLogging(IJournalPagePrinter printer, ILogger logger) // lo que son asociaciones se injectan
        {
            _pages = new List<JournalPage>();  // model data se nstancian a mano
            _printer = printer ?? throw new ArgumentNullException(nameof(printer));  //se pasa por constructor, que guarde el printer. La variable que va a retener las printers. 
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public JournalWithLogging(List<JournalPage> pages, IJournalPagePrinter printer, ILogger logger)
        {
            _pages = pages ?? throw new ArgumentNullException(nameof(pages));
            _printer = printer ?? throw new ArgumentNullException(nameof(printer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Guid AddJournalPage(string title, string content)
        {
            JournalPage journalPage = new JournalPage(title, content);

            _pages.Add(journalPage);

            _logger.Information($"Added new page with Id: {journalPage.Id.ToString()} to Jornal: '{journalPage.Title}'");

            return journalPage.Id;
        }

        public void DeleteJournalPage(JournalPage journalPage)
        {
            _pages.Remove(journalPage);

            _logger.Information($"Removed page with Id: {journalPage.Id.ToString()} from Jornal: '{journalPage.Title}'");
        }

        public void DeleteJournalPageWithId(Guid id)
        {
            JournalPage journalPage = _pages.Find(nt => nt.Id == id);

            DeleteJournalPage(journalPage);
        }

        public void PrintAllJournalPages()
        {
            foreach (var journalPage in _pages)
            {
                PrintJournalPage(journalPage);
            }
        }

        public void PrintJournalPageWithId(Guid id)
        {
            JournalPage journalPage = _pages.Find(nt => nt.Id == id);

            PrintJournalPage(journalPage);
        }

        public void PrintJournalPage(JournalPage journalPage)
        {
            _printer.Print(journalPage);

            _logger.Information($"Printed page with Id: {journalPage.Id.ToString()} from Jornal: '{journalPage.Title}'");
        }
    }
}
