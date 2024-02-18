using Application.Features.Countries.Commands;
using Application.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Countries.Validators
{
    public class CreateCountryRequestValidator:AbstractValidator<CreateCountryRequest>
    {
        public CreateCountryRequestValidator(ICountryRepo countryRepo)
        {
            RuleFor(request => request.NewCountry)
                   .SetValidator(new CreateCountryValidator(countryRepo));
        }
    }
}
