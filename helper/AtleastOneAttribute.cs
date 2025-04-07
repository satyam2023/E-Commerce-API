using System.ComponentModel.DataAnnotations;
using System.Reflection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class AtLeastOneRequiredAttribute : ValidationAttribute
{
    private readonly string[] _propertyNames;

    public AtLeastOneRequiredAttribute(params string[] propertyNames)
    {
        _propertyNames = propertyNames;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return new ValidationResult("Object cannot be null.");

        var type = value.GetType();

        foreach (var propName in _propertyNames)
        {
            PropertyInfo? property = type.GetProperty(propName);

            if (property == null)
                return new ValidationResult($"Property '{propName}' does not exist.");

            var propValue = property.GetValue(value);

            if (propValue is string str && !string.IsNullOrWhiteSpace(str))
                return ValidationResult.Success;

            if (propValue != null)
                return ValidationResult.Success;
        }

        var formattedProps = string.Join(" or ", _propertyNames);
        return new ValidationResult($"At least one field ({formattedProps}) must be provided.");
    }
}
