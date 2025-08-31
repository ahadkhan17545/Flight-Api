using System.ComponentModel.DataAnnotations;

namespace FlightApi.Validation
{
    /// <summary>
    /// Validation attribute to ensure a date property is greater than another date property.
    /// </summary>
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateGreaterThanAttribute"/> class.
        /// </summary>
        /// <param name="comparisonProperty">The name of the property to compare against.</param>
        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        /// <summary>
        /// Determines whether the specified value of the object is valid.
        /// </summary>
        /// <param name="value">The value of the property being validated.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// A <see cref="ValidationResult"/> indicating whether validation succeeded.
        /// </returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime currentValue)
                return ValidationResult.Success;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
                return new ValidationResult($"Unknown property: {_comparisonProperty}");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);
            if (comparisonValue is not DateTime comparisonDateTime)
                return ValidationResult.Success;

            if (currentValue <= comparisonDateTime)
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be greater than {_comparisonProperty}.");

            return ValidationResult.Success;
        }
    }
}
