using System;

namespace HonestTypes.Contacts
{
    public class FirstNames
    {
        string Value { get; }
        public FirstNames(string value) { Value = value ?? string.Empty; }

        public static implicit operator string(FirstNames c)
            => c?.Value ?? string.Empty;
        public static implicit operator FirstNames(string s)
            => new FirstNames(s);

        public override string ToString() => Value;
        public override int GetHashCode() => Value.GetHashCode();
        public override bool Equals(object obj)
        {
            if (Value == null || obj == null)
                return false;

            if (obj.GetType() == typeof(string))
            {
                var otherString = obj as string;
                return string.Equals(Value, otherString, StringComparison.Ordinal);
            }

            if (obj.GetType() == this.GetType())
            {
                string otherString = $"{obj}";
                return string.Equals(Value, otherString, StringComparison.Ordinal);
            }

            return false;
        }
    }
}
