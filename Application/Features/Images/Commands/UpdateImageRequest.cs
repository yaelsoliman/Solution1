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
using static System.Net.Mime.MediaTypeNames;

namespace Application.Features.Images.Commands
{
    public class UpdateImageRequest : IRequest<bool>, ICacheRemoval
    {
        public UpdateImage UpdateImage { get; set; }
        public List<string> CacheKeys { get; set; }
        public UpdateImageRequest(UpdateImage updateImage)
        {
            UpdateImage = updateImage;
            CacheKeys = new() { $"GetImageByIdRequest:{UpdateImage.Id}", "GetImagesRequest" };
        }
    }
    public class UpdateImageRequestHandler : IRequestHandler<UpdateImageRequest, bool>
    {
        private readonly IImageRepo _imageRepo;

        public UpdateImageRequestHandler(IImageRepo imageRepo)
        {
            _imageRepo = imageRepo;
        }
        public async Task<bool> Handle(UpdateImageRequest request, CancellationToken cancellationToken)
        {
            Domain.Image imageInDb = await _imageRepo.GetByIdAsync(request.UpdateImage.Id);
            if (imageInDb != null)
            {
                imageInDb.Name = request.UpdateImage.Name;
                imageInDb.Path = request.UpdateImage.Path;

                await _imageRepo.UpdateAsync(imageInDb);
                return true;
            }
            return false;

        }
    }
}
