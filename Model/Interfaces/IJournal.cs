using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IJournal
    {
        Guid AddJournalPage(string title, string content);

        void DeleteJournalPageWithId(Guid id);

        void DeleteJournalPage(JournalPage journalPage);

        void PrintJournalPageWithId(Guid id);

        void PrintJournalPage(JournalPage journalPage);

        void PrintAllJournalPages();
    }
}
