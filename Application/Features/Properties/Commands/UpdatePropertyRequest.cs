using Application.Models;
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
    public class UpdatePropertyRequest : IRequest<bool>,ICacheRemoval
    {
        public UpdateProperty UpdateProperty { get; set; }
        public List<string> CacheKeys { get; set; }
        public UpdatePropertyRequest(UpdateProperty updateProperty)
        {
            UpdateProperty = updateProperty;
            CacheKeys = new() { $"GetPropertyByIdRequest:{UpdateProperty.Id}", "GetProperties" };
        }

    }
    public class UpdatePropertyRequestHandler : IRequestHandler<UpdatePropertyRequest, bool>
    {
        private readonly IPropertyRepo _propertyRepo;

        public UpdatePropertyRequestHandler(IPropertyRepo propertyRepo)
        {
            _propertyRepo = propertyRepo;
        }

        public async Task<bool> Handle(UpdatePropertyRequest request, CancellationToken cancellationToken)
        {
            Property propertyInDb = await _propertyRepo.GetByIdAsync(request.UpdateProperty.Id);
            if (propertyInDb != null)
            {
                propertyInDb.Name=request.UpdateProperty.Name;
                propertyInDb.Description=request.UpdateProperty.Description;
                propertyInDb.Type=request.UpdateProperty.Type;
                propertyInDb.Louge=request.UpdateProperty.Louge;
                propertyInDb.Address=request.UpdateProperty.Address;
                propertyInDb.Bathrooms=request.UpdateProperty.Bathrooms;
                propertyInDb.Bedrooms=request.UpdateProperty.Bedrooms;
                propertyInDb.Dining=request.UpdateProperty.Dining;
                propertyInDb.Rates=request.UpdateProperty.Rates;
                propertyInDb.ErfSize=request.UpdateProperty.ErfSize;
                propertyInDb.FloorSize=request.UpdateProperty.FloorSize;
                propertyInDb.Price=request.UpdateProperty.Price;
                propertyInDb.Kitchen=request.UpdateProperty.Kitchen;
                propertyInDb.Levies=request.UpdateProperty.Levies;
                propertyInDb.PetsAllowed=request.UpdateProperty.PetsAllowed;
                await _propertyRepo.UpdateAsync(propertyInDb);
                return true;
            }
            return false;
        }
    }
}
