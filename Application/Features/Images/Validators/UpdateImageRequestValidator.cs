using Application.Features.Images.Commands;
using Application.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Images.Validators
{
    public class UpdateImageRequestValidator:AbstractValidator<UpdateImageRequest>
    {
        public UpdateImageRequestValidator(IImageRepo imageRepo,IPropertyRepo propertyRepo)
        {
            RuleFor(n => n.UpdateImage)
                .SetValidator(new UpdateImageValidator(imageRepo,propertyRepo));
        }
    }
}
