using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Journal : IJournal
    {
        List<JournalPage> _pages;

        IJournalPagePrinter _printer;

        public Journal(IJournalPagePrinter printer)  // cuando generas un nuevo journal necesitas siempre un printer
        {
            _pages = new List<JournalPage>();
            _printer = printer ?? throw new ArgumentNullException(nameof(printer));  //Si envías un objeto por parámetro y es nullo, envias un throw new ArgumentNullException.
        }

        public Journal(List<JournalPage> pages, IJournalPagePrinter printer)
        {
            _pages = pages ?? throw new ArgumentNullException(nameof(pages));
            _printer = printer ?? throw new ArgumentNullException(nameof(printer));
        }

        public Guid AddJournalPage(string title, string content)
        {
            JournalPage journalPage = new JournalPage(title, content);  

            _pages.Add(journalPage); // agrega página nueva

            return journalPage.Id;
        }

        public void DeleteJournalPage(JournalPage journalPage)
        {
            _pages.Remove(journalPage);
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
        }
    }
}
