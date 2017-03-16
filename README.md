# Honest Types

This project is an effort to create a repository of commonly used simple expressive types used regularly in all manner of applications. Contact details, data element, and product details are examples of these. I will also focus on helpers for these types like serialization and [microformats](http://microformats.org/wiki/microformats).

## Serialization Package

You can install the Newtonsoft JsonConverter for these and your own types:

> `Install-Package HonestTypes.Serialization`

### Usage

The following snippet allows easy serialization of types with implicit or explicit conversion to `string`.

```csharp
List<JsonConverter> _converters = new List<JsonConverter>
    {
        new SimpleJsonConverter<Email, string>(),
        new SimpleJsonConverter<FirstNames, string>(),
        new SimpleJsonConverter<LastName, string>()
    };

JsonSerializerSettings _settings = new JsonSerializerSettings
    {
        Converters = _converters
    };

private string Serialize(object obj)
{
    return JsonConvert.SerializeObject(obj, _settings);
}

private T Deserialize<T>(string json) where T : class
{
    var obj = JsonConvert.DeserializeObject<T>(json, _settings);
    return obj;
}
```

Where the type being serialized looks like this:

```csharp
public class LastName
{
    string Value { get; }
    public LastName(string value) { Value = value; }

    public static implicit operator string(LastName c)
        => c.Value;
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
```

For more details check out [this blog post](http://devonburriss.me/honest-arguments).

## Return Types

> Install-Package HonestTypes.Return

Contains types for:

* Error - representing errors
* Validation<T> - representing multiple logic failures
* Exceptional<T> - represent a result that might be an exception

See [this blog post](http://devonburriss.me/honest-return-types) for usage details.