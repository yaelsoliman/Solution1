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

namespace Application.Features.Images.Queries
{
    public class GetImageByIdRequest:IRequest<ImageDto>,ICacheable
    {
        public int ImageId { get; set; }
        public string CacheKey { get;set; }
        public bool BypassCache { get; set; }
        public TimeSpan SlidingExpriation { get; set; }

        public GetImageByIdRequest(int imageId)
        {
            ImageId = imageId;
            CacheKey = $"GetImageByIdRequest:{ImageId}";
        }
    }
    public class GetImageByIdRequestHandler : IRequestHandler<GetImageByIdRequest, ImageDto>
    {
        private readonly IImageRepo _imageRepo;
        private readonly IMapper _mapper;

        public GetImageByIdRequestHandler(IImageRepo imageRepo,IMapper mapper)
        {
            _imageRepo = imageRepo;
            _mapper = mapper;
        }
        public async Task<ImageDto> Handle(GetImageByIdRequest request, CancellationToken cancellationToken)
        {
           Image imageInDb=await _imageRepo.GetByIdAsync(request.ImageId);
            if(imageInDb != null)
            {
                ImageDto imageDto=_mapper.Map<ImageDto>(imageInDb);
                return imageDto;
            }
            return null;
        }
    }
}
