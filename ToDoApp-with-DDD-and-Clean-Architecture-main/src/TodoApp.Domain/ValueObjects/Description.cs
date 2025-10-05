using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Domain.ValueObjects
{
    public sealed class Description
    {
        public string Value { get; }

        public Description(string? value)
        {
            value = value?.Trim() ?? string.Empty;

            if (value.Length > 500)
                throw new ArgumentException("Açıklama en fazla 500 karakter olabilir.");

            Value = value;
        }

        public override bool Equals(object? obj) =>
            obj is Description other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;

        public static bool operator ==(Description left, Description right) => Equals(left, right);
        public static bool operator !=(Description left, Description right) => !Equals(left, right);
    }
}
