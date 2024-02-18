using Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Properties.Validators
{
    public class NewPropertyValidator : AbstractValidator<NewProperty>
    {
        public NewPropertyValidator()
        {
           RuleFor(np => np.Name)
            .NotEmpty().WithMessage("Property Name is required")
            .MaximumLength(15).WithMessage("Name should not be more than 15 char");

            RuleFor(np => np.Bedrooms)
                .LessThanOrEqualTo(4).WithMessage("Bedrooms should be less than 5 "); ;

            RuleFor(np => np.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50).WithMessage("Description should not more than 50 char");
        }
    }
}
