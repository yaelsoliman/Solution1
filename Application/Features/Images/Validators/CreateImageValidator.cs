using Application.Models;
using Application.Repositories;
using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Images.Validators
{
    public class CreateImageValidator : AbstractValidator<NewImage>
    {
        public CreateImageValidator(IPropertyRepo propertyRepo)
        {
            RuleFor(newImage => newImage.PropertyId)
                .MustAsync(async (propertyId, ct) => await propertyRepo.GetByIdAsync(propertyId) is Property existingProperty &&
                existingProperty.Id == propertyId).WithMessage("Property does not exist");
            RuleFor(n => n.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(15).WithMessage("Maximum length for Name is 15 char"); ;
            RuleFor(n => n.Path)
                .NotEmpty().NotNull()
                .WithMessage("Path is required");
        }
    }
}
