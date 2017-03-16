using System;
using System.Text.RegularExpressions;

namespace HonestTypes.Contacts
{
    public class Email
    {
        const string regexPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        string Value { get; }
        public bool IsValid { get { return IsEmailValid(Value); } }
        public Email(string value)
        {
            if (!IsEmailValid(value))
            {
                throw new ArgumentException($"{value} is not a valid email address.", nameof(value));
            }
            Value = value;
        }

        private static bool IsEmailValid(string value)
        {
            return Regex.IsMatch(value, regexPattern, RegexOptions.IgnoreCase);
        }

        public static explicit operator string(Email c)
            => c?.Value ?? string.Empty;
        public static explicit operator Email(string s)
            => new Email(s);

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
