using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class JournalPagePrinter : IJournalPagePrinter
    {
        public void Print(JournalPage page)
        {
            StringBuilder stringBuilder = new StringBuilder(); //hace el contenido

            stringBuilder.Append($"Journal Page Title: {page.Title}\n");
            stringBuilder.Append($"Journal Page Date: {page.Date.ToLongDateString()}\n");
            stringBuilder.Append($"Journal Page Content: {page.Content}\n");

            Console.WriteLine(stringBuilder.ToString());

        }
    }
}
