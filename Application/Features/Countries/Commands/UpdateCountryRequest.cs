using Application.Models;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Countries.Commands
{
    public class UpdateCountryRequest : IRequest<bool>
    {
        public UpdateCountry UpdateCountry { get; set; }
        public UpdateCountryRequest(UpdateCountry updateCountry)
        {
            UpdateCountry = updateCountry;
        }
    }
    public class UpdateCountryRequestHandler : IRequestHandler<UpdateCountryRequest, bool>
    {

        private readonly ICountryRepo _countryRepo;

        public UpdateCountryRequestHandler(ICountryRepo countryRepo)
        {

            _countryRepo = countryRepo;
        }
        public async Task<bool> Handle(UpdateCountryRequest request, CancellationToken cancellationToken)
        {
            Country countryInDb = await _countryRepo.GetByIdAsync(request.UpdateCountry.Id);
            if (countryInDb != null)
            {
                countryInDb.Name = request.UpdateCountry.Name;
                await _countryRepo.UpdateAsync(countryInDb);
                return true;
            }
            return false;
        }
    }
}
