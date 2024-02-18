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
    public class UpdateImageValidator : AbstractValidator<UpdateImage>
    {
        public UpdateImageValidator(IImageRepo imageRepo, IPropertyRepo propertyRepo)
        {
            RuleFor(updateImage => updateImage.Id)
                .MustAsync(async (id, ct) => await imageRepo.GetByIdAsync(id) is Image existingImage &&
                  existingImage.Id == id)
                .WithMessage("Image does not exist");

            RuleFor(updateImage => updateImage.PropertyId)
                .MustAsync(async (propertyId, ct) => await propertyRepo.GetByIdAsync(propertyId) is Property existingProperty &&
                existingProperty.Id == propertyId)
                .WithMessage("property does not exist ");

            RuleFor(n => n.Name)
               .NotEmpty().WithMessage("Name is required")
               .MaximumLength(15).WithMessage("Maximum length for Name is 15 char");
            RuleFor(n => n.Path)
                .NotEmpty().NotNull()
                .WithMessage("Path is required")
                .MaximumLength(100).WithMessage("Path shouldn't more than 100 char");

        }
    }
}
