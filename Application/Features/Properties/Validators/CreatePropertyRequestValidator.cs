using Application.Features.Properties.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Properties.Validators
{
    public class CreatePropertyRequestValidator : AbstractValidator<CreatePropertyRequest>
    {
        public CreatePropertyRequestValidator()
        {
            RuleFor(request => request.PropertyRequest)
                .SetValidator(new NewPropertyValidator());
        }
    }
}
