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
    public class CreateCountryRequest:IRequest<bool>
    {
        public NewCountry NewCountry { get; set; }
        public CreateCountryRequest(NewCountry newCountry)
        {
            NewCountry=newCountry;
        }
    }
    public class CreateCountryRequestHandler : IRequestHandler<CreateCountryRequest, bool>
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepo _countryRepo;

        public CreateCountryRequestHandler(IMapper mapper,ICountryRepo countryRepo)
        {
            _mapper = mapper;
            _countryRepo = countryRepo;
        }
        public async Task<bool> Handle(CreateCountryRequest request, CancellationToken cancellationToken)
        {
            Country country=_mapper.Map<Country>(request.NewCountry);
            await _countryRepo.AddNewasync(country);
            return true;
        }
    }
}
