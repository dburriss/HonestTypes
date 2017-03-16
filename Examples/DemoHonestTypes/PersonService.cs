using HonestTypes.Return;

namespace DemoHonestTypes
{
    using static F;

    public class PersonService
    {
        public Validation<Person> Validate(Person person)
        {
            if (person == null)
                return Error("Person is null");

            //collect all errors
            return Valid(person)
                .Apply(ValidateFirstNames(person))
                .Apply(ValidateLastName(person))
                .Apply(ValidateEmail(person));

            //short circuit on error
            return Valid(person)
                .Bind(ValidateFirstNames)
                .Bind(ValidateLastName)
                .Bind(ValidateEmail);
        }

        private Validation<Person> ValidateFirstNames(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.FirstNames))
                return Invalid(Error($"{nameof(person.FirstNames)} cannot be empty"));

            return person;
        }

        private Validation<Person> ValidateLastName(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.LastName))
                return Invalid(Error($"{nameof(person.LastName)} cannot be empty"));

            return person;
        }

        private Validation<Person> ValidateEmail(Person person)
        {
            if (string.IsNullOrWhiteSpace((string)person.Email))
                return Invalid(Error($"{nameof(person.Email)} cannot be empty"));

            return person;
        }
    }
}