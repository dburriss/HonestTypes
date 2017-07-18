using System;

namespace HonestTypes.Return
{
    public static partial class Fun
    {
        public static Error Error(string message) => new Error(message);
    }

    public class Error: IEquatable<Error>
    {
        public virtual string Message { get; }
        public override string ToString() => Message;
        protected Error() { }
        internal Error(string Message) { this.Message = Message; }

        public static implicit operator Error(string m) => new Error(m);

        public bool Equals(Error other)
        {
            if(ReferenceEquals(null, other))
                return false;
            if(ReferenceEquals(this, other))
                return true;
            return string.Equals(Message, other.Message);
        }

        public override bool Equals(object obj)
        {
            if(obj is Error other)
                return Equals(other);
            return false;
        }

        public override int GetHashCode()
        {
            return Message != null ? Message.GetHashCode() : 0;
        }

        public static bool operator ==(Error left, Error right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Error left, Error right)
        {
            return !Equals(left, right);
        }
    }
}
