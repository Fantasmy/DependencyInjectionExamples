using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class JournalPage : Page, IEquatable<JournalPage>
    {
        public JournalPage(string title, string content) : this(Guid.NewGuid(), DateTime.Now, title, content) // va llamando varios... Es el que se ejecuta
        {
        }

        protected JournalPage(Guid id, DateTime date, string title, string content) : base(id, date, content) // protegido, que llama guid
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        public string Title { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as JournalPage);
        }

        public bool Equals(JournalPage other)
        {
            return other != null &&
                   base.Equals(other) &&
                   Title == other.Title;
        }

        public override int GetHashCode()
        {
            var hashCode = 1241924671;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            return hashCode;
        }

        public static bool operator ==(JournalPage page1, JournalPage page2)
        {
            return EqualityComparer<JournalPage>.Default.Equals(page1, page2);
        }

        public static bool operator !=(JournalPage page1, JournalPage page2)
        {
            return !(page1 == page2);
        }
    }
}
