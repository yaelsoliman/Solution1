using Application.Models;
using Application.PipelineBehaviours.Contracts;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Properties.Queries
{
    public class GetPropertiesRequest : IRequest<List<PropertyDto>>, ICacheable
    {
        public string CacheKey { get; set; }
        public bool BypassCache { get; set; }
        public TimeSpan SlidingExpriation { get; set; }
        public GetPropertiesRequest()
        {
            CacheKey = "GetProperties";
        }
    }
    public class GetPropertiesRequestHandler : IRequestHandler<GetPropertiesRequest, List<PropertyDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPropertyRepo _propertyRepo;

        public GetPropertiesRequestHandler(IMapper mapper, IPropertyRepo propertyRepo)
        {
            _mapper = mapper;
            _propertyRepo = propertyRepo;
        }
        public async Task<List<PropertyDto>> Handle(GetPropertiesRequest request, CancellationToken cancellationToken)
        {
            List<Property> properties = await _propertyRepo.GetAllAsync();
            if (properties != null)
            {
               List<PropertyDto> propertiesDto=_mapper.Map<List<PropertyDto>>(properties);
                return propertiesDto;
            }
            return null;
        }
    }
}
