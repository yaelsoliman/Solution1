using Application.Models;
using Application.PipelineBehaviours.Contracts;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Countries.Queries
{
    public class GetCountriesRequest:IRequest<List<CountryDto>>,ICacheable
    {
      
        public string CacheKey { get;set; }
        public bool BypassCache { get; set; }
        public TimeSpan SlidingExpriation { get; set; }
        public GetCountriesRequest()
        {
            CacheKey = "GetCountries";
        }
    }
    public class GetCountriesRequestHandler : IRequestHandler<GetCountriesRequest, List<CountryDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepo _countryRepo;

        public GetCountriesRequestHandler(IMapper mapper,ICountryRepo countryRepo)
        {
            _mapper = mapper;
            _countryRepo = countryRepo;
        }
        public async Task<List<CountryDto>> Handle(GetCountriesRequest request, CancellationToken cancellationToken)
        {
            List<Country> countries = await _countryRepo.GetAllAsync();
            if(countries != null)
            {
                List<CountryDto> countryDtos=_mapper.Map<List<CountryDto>>(countries);
                return countryDtos;
            }
            return null;
        }
    }
}
