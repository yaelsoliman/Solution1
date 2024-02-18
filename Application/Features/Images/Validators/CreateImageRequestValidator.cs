using Application.Features.Images.Commands;
using Application.Models;
using Application.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Images.Validators
{
    public class CreateImageRequestValidator:AbstractValidator<CreateImageRequest>
    {
        public CreateImageRequestValidator(IPropertyRepo propertyRepo)
        {
            RuleFor(request => request.NewImage)
                .SetValidator(new CreateImageValidator(propertyRepo));
        }
    }
}
