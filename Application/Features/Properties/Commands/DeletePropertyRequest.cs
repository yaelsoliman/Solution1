using Application.PipelineBehaviours.Contracts;
using Application.Repositories;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Properties.Commands
{
    public class DeletePropertyRequest : IRequest<bool>, ICacheRemoval
    {
        public int PropertyId { get; set; }
        public List<string> CacheKeys { get; set; }

        public DeletePropertyRequest(int propertyId)
        {
            PropertyId = propertyId;
            CacheKeys = new() { $"GetPropertyByIdRequest:{PropertyId}", "GetProperties" };
        }
    }
    public class DeletePropertyRequestHandler : IRequestHandler<DeletePropertyRequest, bool>
    {
        private readonly IPropertyRepo _propertyRepo;

        public DeletePropertyRequestHandler(IPropertyRepo propertyRepo)
        {
            _propertyRepo = propertyRepo;
        }
        public async Task<bool> Handle(DeletePropertyRequest request, CancellationToken cancellationToken)
        {
            Property propertyInDb = await _propertyRepo.GetByIdAsync(request.PropertyId);
            if (propertyInDb != null)
            {
                await _propertyRepo.DeleteAsync(propertyInDb);
                return true;
            }
            return false;
        }
    }
}
