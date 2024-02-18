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
    public class UpdateCountryRequestValidator:AbstractValidator<UpdateCountryRequest>
    {
        public UpdateCountryRequestValidator(ICountryRepo countryRepo)
        {
            RuleFor(request => request.UpdateCountry)
                .SetValidator(new UpdateCountryValidator(countryRepo));
        }
    }
}
