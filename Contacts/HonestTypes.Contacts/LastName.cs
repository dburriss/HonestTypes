using System;

namespace HonestTypes.Contacts
{

    public class LastName
    {
        string Value { get; }
        public LastName(string value) { Value = value ?? string.Empty; }

        public static implicit operator string(LastName c)
            => c?.Value ?? string.Empty;
        public static implicit operator LastName(string s)
            => new LastName(s);

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
                string otherString = string.Format("{0}", obj);
                return string.Equals(Value, otherString, StringComparison.Ordinal);
            }

            return false;
        }
    }
}
