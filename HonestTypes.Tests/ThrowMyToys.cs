using HonestTypes.Return;
using LanguageExt;
using System;

namespace HonestTypes.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using static LanguageExt.Prelude;
    using static F;

    class ThrowMyToys
    {
        public Exceptional<string> ReturnAResult()
        {
            try
            {
                string x = "Xyz";
                return x;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Exceptional<string> ReturnAResult2()
        {
            Exceptional<string> result = new Exceptional<string>();
            var assign = fun<string>(() => "Xyz");
            Try<string>(assign).Match(
                Succ: x => result = x,
                Fail: ex => result = ex
            );
            return result;
        }

        public Exceptional<string> ThrowAnException()
        {
            try
            {
                string x = null;
                var l = x.Length;
                return x;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Validation<string> ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Error($"{nameof(name)} cannot be empty");// demo returning an error

            List<Error> errors = Validate(name);

            return errors.Any() ? Invalid<string>(errors) : name;
        }

        private List<Error> Validate(string name)
        {
            var errors = new List<Error>();
            if (!IsCapitalized(name))
                errors.Add(Error("Name is not capitalized."));

            if (name != "BooBoo")
                errors.Add(Error("Name is not BooBoo."));
            return errors;
        }

        private bool IsCapitalized(string name)
        {
            var firstLetter = name.Substring(0, 1);
            return firstLetter == firstLetter.ToUpperInvariant();
        }

        public Either<Error, string> CallName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Error($"{nameof(name)} cannot be empty");

            if (name == "BooBoo")
                return "Haha";

            return Error($"{name} is not the name I was looking for.");
        }

    }
}
