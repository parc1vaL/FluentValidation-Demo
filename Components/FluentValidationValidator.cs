using FluentValidation;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace FluentValidationDemo.Components;

public class FluentValidationValidator : ComponentBase
{
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

        CurrentEditContext.AddFluentValidation(ServiceProvider, this);
    }
}