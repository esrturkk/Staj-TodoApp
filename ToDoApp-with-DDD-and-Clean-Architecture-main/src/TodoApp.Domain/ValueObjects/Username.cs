using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Domain.ValueObjects
{
    public sealed class Username
    {
        public string Value { get; }

        public Username(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Kullanıcı adı boş olamaz.");

            if (value.Trim().Length < 3)
                throw new ArgumentException("Kullanıcı adı en az 3 karakter olmalıdır.");

            Value = value.Trim();
        }

        public override bool Equals(object? obj) =>
            obj is Username other && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;

        public static bool operator ==(Username left, Username right) => Equals(left, right);
        public static bool operator !=(Username left, Username right) => !Equals(left, right);
    }
}
