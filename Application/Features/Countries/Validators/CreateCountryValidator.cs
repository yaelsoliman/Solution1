using Application.Models;
using Application.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Countries.Validators
{
    public class CreateCountryValidator:AbstractValidator<NewCountry>
    {
        public CreateCountryValidator(ICountryRepo countryRepo)
        {
            RuleFor(x=>x.Name).NotEmpty().NotNull().WithMessage("Name is required")
                .MaximumLength(25).WithMessage("Name shoulde be less than or equal 25 char");
        }
    }
}
