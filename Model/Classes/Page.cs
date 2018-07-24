using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class Page : IEquatable<Page>
    {
        protected Page(Guid id, DateTime date, string content)
        {
            Id = id;
            Date = date;
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string Content { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Page);
        }

        public bool Equals(Page other)
        {
            return other != null &&
                   Id.Equals(other.Id) &&
                   Date == other.Date &&
                   Content == other.Content;
        }

        public override int GetHashCode()
        {
            var hashCode = -600763453;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Content);
            return hashCode;
        }

        public static bool operator ==(Page page1, Page page2)
        {
            return EqualityComparer<Page>.Default.Equals(page1, page2);
        }

        public static bool operator !=(Page page1, Page page2)
        {
            return !(page1 == page2);
        }
    }
}
