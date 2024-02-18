using Application.Features.Properties.Commands;
using Application.Models;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles
{
    public class Mappings:Profile
    {
        public Mappings()
        {
            CreateMap<NewProperty, Property>();
            CreateMap<Property,PropertyDto>();
            CreateMap<NewImage, Image>();
            CreateMap<Image, ImageDto>();
            CreateMap<NewCountry,Country>();
            CreateMap<CountryDto,Country>().ReverseMap();
        }
    }
}
