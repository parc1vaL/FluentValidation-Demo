using System.ComponentModel.DataAnnotations;
using FluentValidation;

public static class DataAnnotationsExtensions
{
    public static IRuleBuilderOptionsConditions<T, T> DataAnnotations<T>(this IRuleBuilder<T, T> ruleBuilder) {

    return ruleBuilder.Custom((rootObject, context) =>
    {
        var daContext = new ValidationContext(rootObject, serviceProvider: null, items: null);
        var errorResults = new List<ValidationResult>();

        if (Validator.TryValidateObject(rootObject, daContext, errorResults, true))
        {
            return;
        }
        else
        {
            foreach (var error in errorResults)
            {
                context.AddFailure(error.MemberNames.First(), error.ErrorMessage);
            }
        }
    });
}
}