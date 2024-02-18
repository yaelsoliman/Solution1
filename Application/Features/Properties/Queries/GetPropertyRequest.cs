using Application.Models;
using Application.PipelineBehaviours.Contracts;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Properties.Queries
{
    public class GetPropertyRequest : IRequest<PropertyDto>,ICacheable
    {
        public int PropertyId { get; set; }
        public string CacheKey { get; set; }
        public bool BypassCache { get; set; }
        public TimeSpan SlidingExpriation { get; set; }
        public GetPropertyRequest(int propertyId)
        {
            PropertyId = propertyId;
            CacheKey = $"GetProperty:{PropertyId}";
        }
        public class GetPropertyRequestHandler : IRequestHandler<GetPropertyRequest, PropertyDto>
        {
            private readonly IMapper _mapper;
            private readonly IPropertyRepo _propertyRepo;

            public GetPropertyRequestHandler(IMapper mapper, IPropertyRepo propertyRepo)
            {
                _mapper = mapper;
                _propertyRepo = propertyRepo;
            }
            public async Task<PropertyDto> Handle(GetPropertyRequest request, CancellationToken cancellationToken)
            {
                Property propertyInDb = await _propertyRepo.GetByIdAsync(request.PropertyId);
                if (propertyInDb is not null)
                {
                    PropertyDto propertyDto = _mapper.Map<PropertyDto>(propertyInDb);
                    return propertyDto;
                }
                return null;
            }
        }
    }
}
