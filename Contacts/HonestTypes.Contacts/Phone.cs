using System;

namespace HonestTypes.Contacts
{
    public class Phone
    {
        string Value { get; }
        public Phone(string value) { Value = value; }

        public static implicit operator string(Phone c)
            => c.Value;
        public static implicit operator Phone(string s)
            => new Phone(s);

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
