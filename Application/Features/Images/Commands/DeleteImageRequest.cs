using Application.PipelineBehaviours.Contracts;
using Application.Repositories;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Images.Commands
{
    public class DeleteImageRequest : IRequest<bool>,ICacheRemoval
    {
        public int ImageId { get; set; }
        public List<string> CacheKeys { get;set; }
        public DeleteImageRequest(int imageId)
        {
            ImageId = imageId;
            CacheKeys = new() { $"GetImageByIdRequest:{ImageId}", "GetImagesRequest" };
        }
    }
    public class DeleteImageRequestHandler : IRequestHandler<DeleteImageRequest, bool>
    {
        private readonly IImageRepo _imageRepo;

        public DeleteImageRequestHandler(IImageRepo imageRepo)
        {
            _imageRepo = imageRepo;
        }
        public async Task<bool> Handle(DeleteImageRequest request, CancellationToken cancellationToken)
        {
            Image imageInDb = await _imageRepo.GetByIdAsync(request.ImageId);
            if (imageInDb != null)
            {
                await _imageRepo.DeleteAsync(imageInDb);
                return true;
            }
            return false;

        }
    }
}
