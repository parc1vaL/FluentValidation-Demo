using FluentValidation;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace FluentValidationDemo.Components;

public class FluentValidationValidator : ComponentBase
{
    private static readonly char[] Separators = { '.', '[' };
    
    [Inject] private IServiceProvider ServiceProvider { get; set; }

    [CascadingParameter] private EditContext CurrentEditContext { get; set; }

    protected override void OnInitialized()
    {
        if (CurrentEditContext == null)
        {
            throw new InvalidOperationException($"{nameof(FluentValidationValidator)} requires a cascading " +
                $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(FluentValidationValidator)} " +
                $"inside an {nameof(EditForm)}.");
        }

        var messages = new ValidationMessageStore(CurrentEditContext);

        CurrentEditContext.OnValidationRequested +=
            (sender, eventArgs) => ValidateModel((EditContext)sender, messages, ServiceProvider, this);

        CurrentEditContext.OnFieldChanged +=
            (sender, eventArgs) => ValidateModel(CurrentEditContext, messages, ServiceProvider, this);
    }

    private static async void ValidateModel(
        EditContext editContext,
        ValidationMessageStore messages,
        IServiceProvider serviceProvider,
        FluentValidationValidator fluentValidationValidator)
    {
        if (serviceProvider.GetRequiredService(typeof(IValidator<>).MakeGenericType(editContext.Model.GetType())) is IValidator validator)
        {
            var context = ValidationContext<object>.CreateWithOptions(editContext.Model, opt => opt.IncludeAllRuleSets());

            var validationResults = await validator.ValidateAsync(context);

            messages.Clear();
            foreach (var validationResult in validationResults.Errors)
            {
                var fieldIdentifier = ToFieldIdentifier(editContext, validationResult.PropertyName);
                messages.Add(fieldIdentifier, validationResult.ErrorMessage);
            }

            editContext.NotifyValidationStateChanged();
        }
    }

    private static FieldIdentifier ToFieldIdentifier(in EditContext editContext, in string propertyPath)
    {
        // This code is taken from an article by Steve Sanderson (https://blog.stevensanderson.com/2019/09/04/blazor-fluentvalidation/)
        // all credit goes to him for this code.

        // This method parses property paths like 'SomeProp.MyCollection[123].ChildProp'
        // and returns a FieldIdentifier which is an (instance, propName) pair. For example,
        // it would return the pair (SomeProp.MyCollection[123], "ChildProp"). It traverses
        // as far into the propertyPath as it can go until it finds any null instance.

        var obj = editContext.Model;
        var nextTokenEnd = propertyPath.IndexOfAny(Separators);

        // Optimize for a scenario when parsing isn't needed.
        if (nextTokenEnd < 0)
        {
            return new FieldIdentifier(obj, propertyPath);
        }

        ReadOnlySpan<char> propertyPathAsSpan = propertyPath;

        while (true)
        {
            var nextToken = propertyPathAsSpan.Slice(0, nextTokenEnd);
            propertyPathAsSpan = propertyPathAsSpan.Slice(nextTokenEnd + 1);

            object newObj;
            if (nextToken.EndsWith("]"))
            {
                // It's an indexer
                // This code assumes C# conventions (one indexer named Item with one param)
                nextToken = nextToken.Slice(0, nextToken.Length - 1);
                var prop = obj.GetType().GetProperty("Item");

                if (prop is object)
                {
                    // we've got an Item property
                    var indexerType = prop.GetIndexParameters()[0].ParameterType;
                    var indexerValue = Convert.ChangeType(nextToken.ToString(), indexerType);
                    newObj = prop.GetValue(obj, new object[] { indexerValue });
                }
                else
                {
                    // If there is no Item property
                    // Try to cast the object to array
                    if (obj is object[] array)
                    {
                        var indexerValue = int.Parse(nextToken);
                        newObj = array[indexerValue];
                    }
                    else
                    {
                        throw new InvalidOperationException($"Could not find indexer on object of type {obj.GetType().FullName}.");
                    }
                }
            }
            else
            {
                // It's a regular property
                var prop = obj.GetType().GetProperty(nextToken.ToString());
                if (prop == null)
                {
                    throw new InvalidOperationException($"Could not find property named {nextToken.ToString()} on object of type {obj.GetType().FullName}.");
                }
                newObj = prop.GetValue(obj);
            }

            if (newObj == null)
            {
                // This is as far as we can go
                return new FieldIdentifier(obj, nextToken.ToString());
            }

            obj = newObj;

            nextTokenEnd = propertyPathAsSpan.IndexOfAny(Separators);
            if (nextTokenEnd < 0)
            {
                return new FieldIdentifier(obj, propertyPathAsSpan.ToString());
            }
        }
    }
}