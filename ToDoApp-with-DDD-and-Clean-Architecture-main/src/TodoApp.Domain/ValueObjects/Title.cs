using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Domain.ValueObjects
{
    public sealed class Title
    {
        public string Value { get; }

        public Title(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Başlık boş olamaz.");

            if (value.Length > 100)
                throw new ArgumentException("Başlık en fazla 100 karakter olabilir.");

            Value = value.Trim();
        }

        public override bool Equals(object? obj) =>
            obj is Title other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;

        // == ve != operatörlerini desteklemek 
        public static bool operator ==(Title left, Title right) => Equals(left, right);
        public static bool operator !=(Title left, Title right) => !Equals(left, right);
    }
}
