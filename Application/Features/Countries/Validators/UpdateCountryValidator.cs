using Application.Models;
using Application.Repositories;
using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Countries.Validators
{
    public class UpdateCountryValidator:AbstractValidator<UpdateCountry>
    {
        public UpdateCountryValidator(ICountryRepo countryRepo)
        {
            RuleFor(request => request.Name).NotEmpty().NotNull().WithMessage("Name is required")
                .MaximumLength(15).WithMessage("Name should be less than or equal 15 char");

            RuleFor(updateImage => updateImage.Id)
                .MustAsync(async (id, ct) => await countryRepo.GetByIdAsync(id) is Country existingCountry &&
                 existingCountry.Id == id).WithMessage("Country does not exist");
        }
    }
}
