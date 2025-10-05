using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TodoApp.Domain.ValueObjects
{
    public sealed class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("E-posta boş olamaz.");

            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Geçerli bir e-posta adresi girilmelidir.");

            Value = value.Trim();
        }

        public override bool Equals(object? obj) =>
            obj is Email other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;

        public static bool operator ==(Email left, Email right) => Equals(left, right);
        public static bool operator !=(Email left, Email right) => !Equals(left, right);



    }
}

