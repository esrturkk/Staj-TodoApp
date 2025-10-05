using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Domain.ValueObjects
{
    public sealed class DueDate
    {
        public DateTime Value { get; }

        // EF Core için parametresiz ctor
        private DueDate() { }


        public DueDate(DateTime value)
        {
            // Gelen tarih UTC değilse, zorla UTC olarak ayarla
            if (value.Kind == DateTimeKind.Unspecified)
                value = DateTime.SpecifyKind(value, DateTimeKind.Utc);

            // Geçmiş tarih kontrolü
            if (value < DateTime.UtcNow.Date)
                throw new ArgumentException("Bitiş tarihi geçmiş olamaz.");

            Value = value;
        }

        public override bool Equals(object? obj) =>
            obj is DueDate other && Value.Equals(other.Value);

        public override int GetHashCode() =>
            Value.GetHashCode();

        public override string ToString() =>
            Value.ToString("yyyy-MM-dd");

    }
}
